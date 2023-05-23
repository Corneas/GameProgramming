using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class MummyRayAgent : Agent
{
    private new Transform transform;
    private new Rigidbody rigidbody;

    [SerializeField]
    private float moveSpeed = 1.5f;
    [SerializeField]
    private float turnSpeed = 200.0f;

    [SerializeField]
    private Renderer floorRenderer = null;
    [SerializeField]
    private Material goodMaterial = null;
    [SerializeField]
    private Material badMaterial = null;
    private Material originMaterial = null;

    private StageManager stageManager = null;

    public override void Initialize()
    {
        MaxStep = 5000;

        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody>();
        stageManager = transform.parent.GetComponent<StageManager>();

        floorRenderer = transform.parent.Find("Floor").GetComponent<Renderer>();
        originMaterial = floorRenderer.material;
    }

    public override void OnEpisodeBegin()
    {
        stageManager.SetStageObject();

        rigidbody.velocity = rigidbody.angularVelocity = Vector3.zero;
        transform.localPosition = new Vector3(Random.Range(-22.0f, 22.0f), 0.05f, Random.Range(-22.0f, 22.0f));
        transform.localRotation = Quaternion.Euler(Vector3.up * Random.Range(0, 360));
    }

    public override void CollectObservations(VectorSensor sensor)
    {

    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var action = actions.DiscreteActions;

        Vector3 dir = Vector3.zero;
        Vector3 rot = Vector3.zero;

        switch (action[0])
        {
            case 1: 
                dir = transform.forward;
                break;
            case 2: 
                dir = -transform.forward;
                break;
        }

        switch (action[1])
        {
            case 1: 
                rot = -transform.up;
                break;
            case 2:
                rot = transform.up;
                break;
        }

        transform.Rotate(rot, Time.fixedDeltaTime * turnSpeed);
        rigidbody.velocity = dir * moveSpeed;//, ForceMode.VelocityChange);

        AddReward(-1 / (float)MaxStep);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var action = actionsOut.DiscreteActions;
        actionsOut.Clear();

        if (Input.GetKey(KeyCode.W))
        {
            action[0] = 1;

        }
        if (Input.GetKey(KeyCode.S))
        {
            action[0] = 2;

        }

        if (Input.GetKey(KeyCode.A))
        {
            action[1] = 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            action[1] = 2;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GoodItem"))
        {
            rigidbody.velocity = rigidbody.angularVelocity = Vector3.zero;
            Destroy(collision.gameObject);
            SetReward(1f);

            StartCoroutine(RevertMaterial(goodMaterial));
        }

        if (collision.gameObject.CompareTag("BadItem"))
        {
            AddReward(-1.0f);
            EndEpisode();

            StartCoroutine(RevertMaterial(badMaterial));
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            AddReward(-0.1f);
        }
    }

    private IEnumerator RevertMaterial(Material material)
    {
        floorRenderer.material = material;
        yield return new WaitForSeconds(0.2f);
        floorRenderer.material = originMaterial;
    }
}
