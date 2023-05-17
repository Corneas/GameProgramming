using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class PenguinAgent : Agent
{
    public float moveSpeed = 5f;
    public float turnSpeed = 180f;
    public GameObject heartPrefab;
    public GameObject regurgitatedFishPrefab;

    private PenguinArea penguinArea;
    new private Rigidbody rigidbody;
    private GameObject baby;
    private bool isFull;

    public override void Initialize()
    {
        penguinArea = GetComponentInParent<PenguinArea>();
        baby = penguinArea.penguinBaby;
        rigidbody = GetComponent<Rigidbody>();
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float forwardAmount = actions.DiscreteActions[0];
        float turnAmount = 0f;

        if(actions.DiscreteActions[1] == 1f)
        {
            turnAmount = -1f;
        }
        else if(actions.DiscreteActions[1] == 2f)
        {
            turnAmount = 1f;
        }

        rigidbody.MovePosition(transform.position + transform.forward * forwardAmount * moveSpeed * Time.fixedDeltaTime);
        transform.Rotate(transform.up * turnAmount * turnSpeed * Time.fixedDeltaTime);

        if(MaxStep > 0)
        {
            AddReward(-1f / MaxStep);
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        int forwardAction = 0;
        int turnAction = 0;
        if (Input.GetKey(KeyCode.W))
        {
            forwardAction = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            turnAction = 1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            turnAction = 2;
        }

        actionsOut.DiscreteActions.Array[0] = forwardAction;
        actionsOut.DiscreteActions.Array[1] = turnAction;
    }

    public override void OnEpisodeBegin()
    {
        isFull = false;
        penguinArea.ResetArea();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(isFull);
        sensor.AddObservation(Vector3.Distance(baby.transform.position, transform.position));
        sensor.AddObservation((baby.transform.position - transform.position).normalized);
        sensor.AddObservation(transform.forward);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Fish"))
        {
            EatFish(collision.gameObject);
        }
        else if (collision.transform.CompareTag("BabyPenguin"))
        {
            RegurgitateFish();
        }
    }

    private void EatFish(GameObject fishObject)
    {
        if (isFull) return;
        isFull = true;

        penguinArea.RemoveSpecificFish(fishObject);

        AddReward(1f);
    }

    private void RegurgitateFish()
    {
        if (!isFull) return;
        isFull = false;

        GameObject regurgitatedFish = Instantiate(regurgitatedFishPrefab);
        regurgitatedFish.transform.parent = transform.parent;
        regurgitatedFish.transform.position = baby.transform.position;
        Destroy(regurgitatedFish, 4f);

        GameObject heart = Instantiate(heartPrefab);
        heart.transform.parent = transform.parent;
        heart.transform.position = baby.transform.position + Vector3.up;
        Destroy(heart, 4f);

        AddReward(1f);

        if(penguinArea.FishRemaining <= 0)
        {
            EndEpisode();
        }
    }
}
