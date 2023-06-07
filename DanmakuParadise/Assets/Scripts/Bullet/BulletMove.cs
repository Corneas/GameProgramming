using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float bulletSpd = 10f;

    private Poolable poolable;

    WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
    private void Awake()
    {
        poolable = GetComponent<Poolable>();
    }

    private void OnEnable()
    {
        transform.position = Vector3.zero;
        bulletSpd = 10f;
    }

    private void Update()
    {
        transform.Translate(Vector3.right * bulletSpd * Time.deltaTime, Space.Self);

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < -2f || pos.x > 2f || pos.y < -2f || pos.y > 2f)
        {
            Pool();
        }
        transform.position = Camera.main.ViewportToWorldPoint(pos);

    }

    public void Pool()
    {
        Managers.Pool.Push(poolable);
    }

    public IEnumerator Acc(float accel = 0.2f, float limitSpeed = 10f)
    {
        while (bulletSpd < limitSpeed)
        {
            bulletSpd += accel;
            if (bulletSpd > limitSpeed)
            {
                bulletSpd = limitSpeed;
            }

            yield return waitForEndOfFrame;
        }
    }

}
