using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class PlayerAgents : Agent
{
    [SerializeField]
    private float speed = 21f;

    [SerializeField]
    private GameObject enemyPrefab = null;
    [SerializeField]
    private List<EnemyMove> enemyList = new List<EnemyMove>();

    public override void Initialize()
    {
        speed = 21f;

        //for (int i = 0; i < 5; ++i)
        //{
        //    var randPosX = Random.Range(-15f, 15f);
        //    var randPosZ = Random.Range(-15f, 15f);
        //    EnemyMove enemy = Instantiate(enemyPrefab, new Vector3(randPosX, 0, randPosZ), Quaternion.identity).GetComponent<EnemyMove>();

        //    enemyList.Add(enemy);
        //}
    }

    public override void OnEpisodeBegin()
    {
        transform.localPosition = Vector3.zero;

        for (int i = 0; i < enemyList.Count; ++i)
        {
            var randPosX = Random.Range(transform.position.x - 15f, transform.position.x + 15f);
            var randPosZ = Random.Range(transform.position.z - 15f, transform.position.z + 15f);

            enemyList[i].Init();
            enemyList[i].transform.position = new Vector3(randPosX, 0, randPosZ);
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        for(int i = 0; i < enemyList.Count; ++i)
        {
            sensor.AddObservation(enemyList[i].transform.position);
        }
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(GetComponent<Rigidbody>().velocity.x);
        sensor.AddObservation(GetComponent<Rigidbody>().velocity.z);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float h = Mathf.Clamp(actions.ContinuousActions[0], -100.0f, 100.0f);
        float v = Mathf.Clamp(actions.ContinuousActions[1], -100.0f, 100.0f);

        Vector3 dir = (Vector3.forward * v) + (Vector3.right * h);
        transform.Translate(dir * speed * Time.deltaTime);

        SetReward(0.01f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("catched");
            SetReward(-1.0f);
            EndEpisode();
        }
    }


}
