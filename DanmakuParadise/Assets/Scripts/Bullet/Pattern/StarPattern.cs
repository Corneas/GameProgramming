using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPattern : ShootingBase
{
    private float interval = 0.05f;

    protected override void StartPattern()
    {
        Star();
    }

    #region 하드코딩
    //private float[] star = new float[10];

    //private List<Bullet> bulletList = new List<Bullet>();

    //public void Star()
    //{
    //    for(int i = 0; i < 10; ++i)
    //    {
    //        star[i] = i * 36f;
    //    }

    //    for(int i = 0; i < 10; ++i)
    //    {
    //        Bullet bullet = BulletPool.Instance.Pop(transform.position);
    //        bullet.transform.right = Vector3.up;

    //        if(i % 2 == 0)
    //        {
    //            bullet.BulletSpd = 10f;
    //        }
    //        else
    //        {
    //            bullet.BulletSpd = 5f;
    //        }

    //        bullet.transform.Rotate(new Vector3(0, 0, star[i]));

    //        bulletList.Add(bullet);
    //    }

    //    StartCoroutine(StopBullet(bulletList.ToArray()));
    //    //bulletList.Clear();
    //}

    //public IEnumerator StopBullet(Bullet[] bullets)
    //{
    //    yield return new WaitForSeconds(0.25f);
    //    for(int i = 0; i < bulletList.Count; ++i)
    //    {
    //        bullets[i].BulletSpd = 0f;
    //    }
    //}
    #endregion

    #region 게임 매니악스 탄막 별 예제
    // 별 정점 x좌표
    private float[] starX = { 0f, -0.59f, 0.95f, -0.95f, 0.59f };
    // 별 정점 y좌표
    private float[] starY = { 1f, -0.81f, 0.31f, 0.31f, -0.81f };

    public void Star()
    {
        //for(int i = 0; i < 5; ++i)
        //{
        //    Bullet bullet = BulletPool.Instance.Pop(Vector3.zero);
        //    bullet.transform.position = new Vector3(starX[i], starY[i]);
        //    bullet.BulletSpd = 0f;
        //}

        float value = 0f;
        Vector3 lerpPos = Vector3.zero;
        Vector3 _P1;
        Vector3 _P2;

        for(int i = 0; i < starX.Length - 1; ++i)
        {
            _P1 = new Vector3(starX[i], starY[i]);
            _P2 = new Vector3(starX[i + 1], starY[i + 1]);

            while (value <= 1f)
            {
                value += interval;
                lerpPos = LerpTwoPoint(_P1, _P2, value);

                Bullet bullet = BulletPool.Instance.Pop(transform.position);
                bullet.transform.position = lerpPos;
            }
        }
    }

    public Vector3 LerpTwoPoint(Vector3 P1, Vector3 P2, float value)
    {
        Vector3 a = Vector3.Lerp(P1, P2, value);

        return a;
    }

    #endregion
}
