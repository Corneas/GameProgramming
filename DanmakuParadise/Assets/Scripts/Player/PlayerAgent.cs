using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;

public class PlayerAgent : Agent
{
    [SerializeField]
    private float speed = 10f;

    private StageManager stageManager = null;

    public override void Initialize()
    {
        MaxStep = 3000;
        stageManager = FindObjectOfType<StageManager>();
    }

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(0, -2f, 0);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var action = actions.DiscreteActions;

        Vector3 dir = Vector3.zero;

        switch (action[0])
        {
            case 1:
                dir = transform.up;
                break;
            case 2:
                dir = -transform.up;
                break;
            case 3:
                dir = -transform.right;
                break;
            case 4:
                dir = transform.right;
                break;
        }

        transform.Translate(dir * speed * Time.deltaTime);

        Vector3 playerPos = Camera.main.WorldToViewportPoint(transform.position);

        playerPos.x = Mathf.Clamp(playerPos.x, 0.02f, 0.98f);
        playerPos.y = Mathf.Clamp(playerPos.y, 0.03f, 0.97f);

        transform.position = Camera.main.ViewportToWorldPoint(playerPos);

        SetReward(0.01f);
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
        else if (Input.GetKey(KeyCode.A))
        {
            action[0] = 3;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            action[0] = 4;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            SetReward(-1f);
            stageManager.InitStage();
            transform.position = Vector3.down * 0.5f;
            EndEpisode();
        }
    }
}
