using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class MummyILAgent : Agent
{
    private StageManagerIL stageManagerIL;

    private Rigidbody rigid;

    private Renderer floorRenderer;
    private Material material;

    public Material originMaterial;
    public Material goodMaterial;
    public Material badMaterial;

    public float turnSpeed = 5f;
    public float moveSpeed = 5f;

    public override void Initialize()
    {
        MaxStep = 2000;
        rigid = GetComponent<Rigidbody>();

        floorRenderer = transform.parent.Find("Floor").GetComponent<Renderer>();
        originMaterial = floorRenderer.material;
        
        stageManagerIL = transform.parent.GetComponent<StageManagerIL>();
    }

    public override void OnEpisodeBegin()
    {
        stageManagerIL.InitStage();

        rigid.velocity = rigid.angularVelocity = Vector3.zero;
        transform.localPosition = new Vector3(0, 0.0f, -3.5f);
        transform.localRotation = Quaternion.identity;
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
                rot = transform.up;
                break;
            case 2:
                rot = -transform.up;
                break;
        }

        transform.Rotate(rot, Time.fixedDeltaTime * turnSpeed);
        rigid.AddForce(dir * moveSpeed, ForceMode.VelocityChange);

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var action = actionsOut.DiscreteActions;

        actionsOut.Clear();

        if (Input.GetKey(KeyCode.W))
        {
            action[0] = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            action[0] = 2;
        }

        if (Input.GetKey(KeyCode.A))
        {
            action[1] = 1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            action[1] = 2;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor") return;

        if (collision.collider.tag == stageManagerIL.hintColor.ToString())
        {
            SetReward(1.0f);
            EndEpisode();
            StartCoroutine(RevertMaterial(goodMaterial));
        }
        else
        {
            if(collision.collider.CompareTag("Wall") || collision.gameObject.name == "Hint")
            {
                SetReward(-0.05f);
            }
            else
            {
                SetReward(-1.0f);
                EndEpisode();
                StartCoroutine(RevertMaterial(badMaterial));
            }
        }
    }

    public IEnumerator RevertMaterial(Material material)
    {
        floorRenderer.material = material;
        yield return new WaitForSeconds(0.2f);
        floorRenderer.material = originMaterial;
    }
}
