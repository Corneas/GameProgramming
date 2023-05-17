using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class MummyAgent : Agent
{
    private Transform trans;
    private Rigidbody rigid;
    [SerializeField]
    private Transform targetTransform;
    [SerializeField]
    private Renderer floorRenderer;

    private Material originMaterial;
    [SerializeField]
    private Material badMaterial;
    [SerializeField]
    private Material goodMaterial;

    public override void Initialize()
    {
        trans = GetComponent<Transform>();
        rigid = GetComponent<Rigidbody>();
        originMaterial = floorRenderer.material;
    }

    public override void OnEpisodeBegin()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;

        trans.localPosition = new Vector3(Random.Range(-4.0f, 4.0f), 0.05f, Random.Range(-4.0f, 4.0f));
        targetTransform.localPosition = new Vector3(Random.Range(-4.0f, 4.0f), 0.55f, Random.Range(-4.0f, 4.0f));

        StartCoroutine(RevertMaterial());
    }

    private IEnumerator RevertMaterial()
    {
        yield return new WaitForSeconds(0.2f);
        floorRenderer.material = originMaterial;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(targetTransform.localPosition);
        sensor.AddObservation(trans.localPosition);
        sensor.AddObservation(rigid.velocity.x);
        sensor.AddObservation(rigid.velocity.z);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float h = Mathf.Clamp(actions.ContinuousActions[0], -1.0f, 1.0f);
        float v = Mathf.Clamp(actions.ContinuousActions[1], -1.0f, 1.0f);

        Vector3 dir = (Vector3.forward * v) + (Vector3.right * h);
        rigid.AddForce(dir.normalized * 50.0f);

        SetReward(-0.001f);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        actionsOut.ContinuousActions.Array[0] = Input.GetAxisRaw("Horizontal");
        actionsOut.ContinuousActions.Array[1] = Input.GetAxisRaw("Vertical");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("DeadZone"))
        {
            floorRenderer.material = badMaterial;
            SetReward(-1.0f);
            EndEpisode();
        }

        if (collision.collider.CompareTag("Target"))
        {
            floorRenderer.material = goodMaterial;
            SetReward(1.0f);
            EndEpisode();
        }
    }
}
