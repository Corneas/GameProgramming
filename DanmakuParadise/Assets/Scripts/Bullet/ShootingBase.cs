using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShootingBase : MonoBehaviour
{
    private Bullet bulletPre;

    protected abstract void StartPattern();

    private void Awake()
    {
        bulletPre = Resources.Load<Bullet>("/prefab/Bullet");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartPattern();
        }
    }

    // 원형발사
    /// <summary>
    /// 발사 횟수, 발사 간격 시간
    /// </summary>
    /// <param name="fireCount"></param>
    /// <param name="fireInterval"></param>
    /// <returns></returns>
    protected IEnumerator IECircleFire(int fireCount, float fireInterval = 0.5f)
    {
        float fireAngle = 0f;
        float angle = 10f;

        WaitForSeconds waitForSeconds = new WaitForSeconds(fireInterval);

        // fireCount번 발사
        for (int i = 0; i < fireCount; ++i)
        {
            // 초기 각도가 계속 같으면 무적존이 생기기 때문에 각도 변경
            fireAngle = i % 2 == 0 ? 0f : 15f;

            for (int j = 0; j < 36; ++j)
            {
                Bullet bullet = null;

                bullet = BulletPool.Instance.Pop(transform.position);

                // 삼각함수를 이용하여 원형으로 방향조절
                Vector2 direction = new Vector2(Mathf.Cos(fireAngle * Mathf.Deg2Rad), Mathf.Sin(fireAngle * Mathf.Deg2Rad));
                // 2차원 수학식은 모두 X축이 기준이 되기 떄문에 x축인 right를 기준점으로 방향을 조절해줌
                bullet.transform.right = direction;

                fireAngle += angle;
                if (fireAngle >= 360)
                {
                    fireAngle -= 360;
                }

            }
            yield return waitForSeconds;
        }
    }

    // 회오리 발사
    /// <summary>
    /// 발사 횟수, 발사 간격
    /// </summary>
    /// <param name="fireCount"></param>
    /// <param name="fireInterval"></param>
    /// <returns></returns>
    protected IEnumerator IEVortexFire(int fireCount, float fireInterval = 0.1f)
    {
        float fireAngle = 0f;
        float angle = 10f;

        WaitForSeconds waitForSeconds = new WaitForSeconds(fireInterval);

        // fireCount번 발사
        for (int i = 0; i < fireCount; ++i)
        {
            // 초기 각도가 계속 같으면 무적존이 생기기 때문에 각도 변경
            fireAngle = i % 2 == 0 ? 0f : 15f;

            for (int j = 0; j < 36; ++j)
            {
                Bullet bullet = null;

                bullet = BulletPool.Instance.Pop(transform.position);

                // 삼각함수를 이용하여 원형으로 방향조절
                Vector2 direction = new Vector2(Mathf.Cos(fireAngle * Mathf.Deg2Rad), Mathf.Sin(fireAngle * Mathf.Deg2Rad));
                // 2차원 수학식은 모두 X축이 기준이 되기 떄문에 x축인 right를 기준점으로 방향을 조절해줌
                bullet.transform.right = direction;

                fireAngle += angle;
                if (fireAngle >= 360)
                {
                    fireAngle -= 360;
                }

                yield return waitForSeconds;
            }
        }
    }

    /// <summary>
    /// 총알 배열, 가속도, 초기 발사 속도, 초기 발사 후 멈추는 시간, 멈춘 이후 가속과 함께 붙는 기초 속도
    /// </summary>
    /// <param name="bullets"></param>
    /// <param name="accel"></param>
    /// <param name="initSpeed"></param>
    /// <param name="stopTime"></param>
    /// <returns></returns>
    protected IEnumerator IEBulletAcceleration(Bullet[] bullets, float accel, float initSpeed = 10f, float stopTime = 0.1f, float startSpeed = 0.8f, float limitSpeed = 10f)
    {
        foreach (var bulletItem in bullets)
        {
            bulletItem.bulletSpd = initSpeed;
        }

        yield return new WaitForSeconds(stopTime);

        foreach (var bulletItem in bullets)
        {
            bulletItem.bulletSpd = startSpeed;
            StartCoroutine(bulletItem.Acc(accel, limitSpeed));
        }

        yield return null;
    }
}
