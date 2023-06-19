using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomShotPattern : ShootingBase
{
    private List<Bullet> bulletList = new List<Bullet>();

    private float fireAngle = 0f;

    private WaitForSeconds waitForZeroPointZeroFive = new WaitForSeconds(0.05f);
    private WaitForSeconds waitForZeroPointOne = new WaitForSeconds(0.1f);
    private WaitForSeconds waitForZeroPointTwoFive = new WaitForSeconds(0.25f);

    protected override void StartPattern()
    {
        StartCoroutine(IERandomShot());
    }

    private IEnumerator IERandomShot()
    {
        // 초기 발사각 지정
        fireAngle += Random.Range(0f, 359f);

        //for(int i = 0; i < 12; ++i)
        //{

            for(int j = 0; j < 4; ++j)
            {
                Bullet bullet = BulletPool.Instance.Pop(transform.position);
                bullet.BulletSpd = 5f;

                Vector3 dir = new Vector3(Mathf.Cos(fireAngle * Mathf.Deg2Rad), Mathf.Sin(fireAngle * Mathf.Deg2Rad)).normalized;
                bullet.transform.right = dir;

                bulletList.Add(bullet);

                yield return waitForZeroPointOne;
            }

            StartCoroutine(IEShotBulletInBullet(bulletList.ToArray()));
            yield return waitForZeroPointTwoFive;
            fireAngle += Random.Range(50f, 310f);
            if (fireAngle >= 360f)
                fireAngle -= 360f;
            bulletList.Clear();
        //}

    }

    private IEnumerator IEShotBulletInBullet(Bullet[] bulletArr)
    {
        yield return waitForZeroPointOne;

        for(int i = bulletArr.Length - 1; i >= 0; --i)
        {
            for(int j = 0; j < 3; j += 2)
            {
                Bullet addBullet = BulletPool.Instance.Pop(bulletArr[i].transform.position);
                addBullet.transform.position = bulletArr[i].transform.position;
                addBullet.BulletSpd = 5f;

                Vector3 dir = new Vector3(Mathf.Cos((j * 90f) * Mathf.Deg2Rad), Mathf.Sin((j * 90f) * Mathf.Deg2Rad));
                addBullet.transform.right = dir;
            }
            yield return waitForZeroPointOne;
        }
    }
}
