using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpd = 10f;

    WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

    private void OnEnable()
    {
        transform.position = Vector3.zero;
        bulletSpd = 10f;
    }

    private void Update()
    {
        transform.Translate(Vector3.right * bulletSpd * Time.deltaTime, Space.Self);

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0f || pos.x > 1f || pos.y < 0f || pos.y > 1f)
        {
            Pool();
        }
        transform.position = Camera.main.ViewportToWorldPoint(pos);

    }

    public void Pool()
    {
        BulletPool.Instance.Push(this);
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
