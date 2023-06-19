using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NWayPattern : ShootingBase
{
    protected override void StartPattern()
    {
        StartCoroutine(IENWayPattern());
    }

    /// <summary>
    /// 총 반복 횟수, nWay 개수, 한 행의 총알 개수, 열과 열 사이의 가로 간격, 한 행 내 총알 사이의 간격, 총알 발사 간격
    /// </summary>
    /// <param name="fireCount"></param>
    /// <param name="nWay"></param>
    /// <param name="count"></param>
    /// <param name="angle"></param>
    /// <param name="angleRate"></param>
    /// <param name="interval"></param>
    /// <returns></returns>
    public IEnumerator IENWayPattern(int fireCount = 100, int nWay = 4, int count = 8, float angle = 2f, float angleRate = 8f, float interval = 0.05f)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(interval);

        float fireAngle = 0f;
        
        for(int k = 0; k < fireCount; ++k)
        {
            fireAngle = angle * k;
            for(int j = 0; j < count; ++j)
            {
                for (int i = 0; i < nWay; ++i)
                {
                    Bullet bullet = BulletPool.Instance.Pop(transform.position);
                    bullet.BulletSpd = 5f;

                    Vector2 dir = Vector2.zero;
                    dir = new Vector2(Mathf.Cos(fireAngle * Mathf.Deg2Rad), Mathf.Sin(fireAngle * Mathf.Deg2Rad));

                    bullet.transform.right = dir;

                    fireAngle += (360 / (nWay));

                    if (fireAngle >= 360f)
                    {
                        fireAngle -= 360f;
                    }
                }
                fireAngle += angleRate;
            }
            yield return waitForSeconds;
        }
    }
}
