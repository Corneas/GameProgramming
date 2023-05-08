using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern2 : ShootingBase
{
    private List<Bullet> bulletList = new List<Bullet>();
    
    protected override void Init()
    {
        bulletFireObj = null;
    }

    private void OnEnable()
    {
        StartCoroutine(AccVortex());
    }

    protected IEnumerator AccVortex()
    {
        float fireAngle = 0f;
        float angle = 2f;
        float acc = 3f;

        for (int i = 0; i < 60; ++i)
        {
            for (int j = 0; j < 6; ++j)
            {

                Bullet bullet = null;

                bullet = BulletPool.Instance.Pop(transform.position);

                // x좌표를 코사인, y좌표를 사인으로 할당하여 반시계 방향으로 움직이는 벡터
                Vector2 direction = new Vector2(Mathf.Cos(fireAngle * Mathf.Deg2Rad), Mathf.Sin(fireAngle * Mathf.Deg2Rad));
                // 이차원 좌표평면에서는 오른쪽을 향해야 값이 상승
                bullet.transform.right = direction;
                bulletList.Add(bullet);
                StartCoroutine(BulletAcceleration(bulletList.ToArray(), acc));

                fireAngle += 60;
                if (fireAngle >= 360)
                {
                    fireAngle -= 360;
                }
            }
            yield return new WaitForSeconds(0.1f);
            bulletList.Clear();
            fireAngle += angle;
            acc += 0.5f;
        }

        gameObject.SetActive(false);

        yield break;
    }
}
