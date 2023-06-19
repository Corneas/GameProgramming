using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePattern : ShootingBase
{
    private bool isTurn = false;

    private WaitForSeconds waitForSeconds = new WaitForSeconds(0.025f);

    protected override void StartPattern()
    {
        StartCoroutine(IEWave());
    }

    private IEnumerator IEWave()
    {
        float fireAngle = 0f;
        float angle = 2f;

        for(int j = 0; j < 320; ++j)
        {
            if (j % Random.Range(20, 41) == 0)
                isTurn = !isTurn;

            for(int i = 0; i < 8; ++i)
            {
                Bullet bullet = BulletPool.Instance.Pop(transform.position);
                bullet.BulletSpd = 5f;

                Vector2 direction = Vector2.zero;

                direction = new Vector2(Mathf.Cos(fireAngle * Mathf.Deg2Rad), Mathf.Sin(fireAngle * Mathf.Deg2Rad));

                bullet.transform.right = direction;

                fireAngle += 45f;

                if (fireAngle >= 360f)
                {
                    fireAngle -= 360f;
                }

            }

            if (isTurn)
                fireAngle += angle;
            else
                fireAngle -= angle;

            yield return waitForSeconds;
        }

        yield break;
    }
}
