using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShootingBase : MonoBehaviour
{
    protected Transform bulletFireObj = null;

    private void Start()
    {
        Init();
    }

    protected abstract void Init();

    /// <summary>
    /// ���� �߻�
    /// </summary>
    /// <returns></returns>
    protected IEnumerator CircleFire(Vector3 pos, int count)
    {
        float fireAngle = 0f;
        float angle = 10f;

        for (int i = 0; i < count; ++i)
        {
            fireAngle = i % 2 == 0 ? 0f : 15f;

            for (int j = 0; j < 36; ++j)
            {
                Bullet bullet = null;

                bullet = BulletPool.Instance.Pop(pos);

                // x��ǥ�� �ڻ���, y��ǥ�� �������� �Ҵ��Ͽ� �ݽð� �������� �����̴� ����
                Vector2 direction = new Vector2(Mathf.Cos(fireAngle * Mathf.Deg2Rad), Mathf.Sin(fireAngle * Mathf.Deg2Rad));
                // ������ ��ǥ��鿡���� �������� ���ؾ� ���� ���
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
    /// bullet�� �ӵ� ���� (�Ѿ��� ��� �迭, ����(0.8f + accel))
    /// </summary>
    /// <param name="bullets"></param>
    /// <param name="accel"></param>
    /// <returns></returns>
    protected IEnumerator BulletAcceleration(Bullet[] bullets, float accel, float initSpeed = 10f)
    {
        foreach (var bulletItem in bullets)
        {
           bulletItem.bulletSpd = initSpeed;
        }

        yield return new WaitForSeconds(0.1f);

        foreach (var bulletItem in bullets)
        {
            bulletItem.bulletSpd = 0.8f + accel;
        }

        yield return null;
    }
}
