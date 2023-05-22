using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    private bool isCatch = false;

    private WaitForEndOfFrame waitForEndOfFrame;

    [SerializeField]
    private float speed = 0.5f;

    public void Init()
    {
        isCatch = false;
    }

    private void Start()
    {
        isCatch = false;
        waitForEndOfFrame = new WaitForEndOfFrame();
        speed = 8f;

        StartCoroutine(IEChaseTarget());
    }

    private IEnumerator IEChaseTarget()
    {
        while (!isCatch)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);

            //transform.LookAt(target);
            //transform.Translate(transform.forward);

            //var dir = (target.transform.position - transform.position).normalized;
            //var rot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            yield return waitForEndOfFrame;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("catch!");
            isCatch = true;
        }
    }
}
