using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShootingBase : MonoBehaviour
{
    private Transform bulletFireObj = null;

    private void Awake()
    {
        Init();
    }

    protected abstract void Init();

    /// <summary>
    /// 원형 발사
    /// </summary>
    /// <returns></returns>
    protected IEnumerator CircleFire()
    {
        float fireAngle = 0f;
        float angle = 10f;

        for (int i = 0; i < 5; ++i)
        {
            fireAngle = i % 2 == 0 ? 0f : 15f;

            for (int j = 0; j < 36; ++j)
            {
                Bullet bullet = null;

                bullet = BulletPool.Instance.Pop(transform);

                // x좌표를 코사인, y좌표를 사인으로 할당하여 반시계 방향으로 움직이는 벡터
                Vector2 direction = new Vector2(Mathf.Cos(fireAngle * Mathf.Deg2Rad), Mathf.Sin(fireAngle * Mathf.Deg2Rad));
                // 이차원 좌표평면에서는 오른쪽을 향해야 값이 상승
                bullet.transform.right = direction;

                fireAngle += angle;
                if (fireAngle >= 360)
                {
                    fireAngle -= 360;
                }

            }
            yield return new WaitForSeconds(0.5f);
        }

        yield break;
    }

    /// <summary>
    /// bullet의 속도 조절 (총알이 담긴 배열, 가속(0.8f + accel))
    /// </summary>
    /// <param name="bullets"></param>
    /// <param name="accel"></param>
    /// <returns></returns>
    protected IEnumerator BulletAcceleration(Bullet[] bullets, float accel)
    {
        foreach (var bulletItem in bullets)
        {
           bulletItem.bulletSpd = 10f;
        }

        yield return new WaitForSeconds(0.1f);

        foreach (var bulletItem in bullets)
        {
            bulletItem.bulletSpd = 0.8f + accel;
        }

        yield return null;
    }
}
