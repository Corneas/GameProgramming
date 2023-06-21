using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerPattern1 : ShootingBase
{
    protected override void StartPattern()
    {
        // TODO : 45, 135���� ���� �ݴ��
        StartCoroutine(IEFire(0f));
        StartCoroutine(IEFire(45f, -1f));
        StartCoroutine(IEFire(90f));
        StartCoroutine(IEFire(135f, -1f));
    }

    /// <summary>
    /// �ʱ� ����, ����( 1 : ��, -1 : ��), �߰��Ǵ� ����
    /// </summary>
    /// <param name="initRot"></param>
    /// <param name="dir"></param>
    /// <param name="addAngle"></param>
    /// <returns></returns>
    public IEnumerator IEFire(float initRot, float dir = 1f, float addAngle = 0f, float bulletSpd = 3f)
    {
        float fireAngle = initRot;
        float angle = 5f;

        WaitForSeconds waitForSeconds = new WaitForSeconds(0.025f);

        // fireCount�� �߻�
        //for (int i = 0; i < 32; ++i)
        //{
        //    // �ʱ� ������ ��� ������ �������� ����� ������ ���� ����
        //    fireAngle = i % 2 == 0 ? 0f : 15f;

        for (int i = 0; i < 256; ++i)
        {
            for (int j = 0; j < 2; ++j)
            {
                Bullet bullet = null;

                bullet = BulletPool.Instance.Pop(transform.position);
                bullet.transform.position = transform.parent.position;
                bullet.BulletSpd = bulletSpd;
                Vector2 direction = Vector2.zero;

                // �ﰢ�Լ��� �̿��Ͽ� �������� ��������
                if (dir == 1)
                {
                    direction = new Vector2(Mathf.Cos(fireAngle * Mathf.Deg2Rad), Mathf.Sin(fireAngle * Mathf.Deg2Rad));
                }
                else if (dir == -1)
                {
                    direction = new Vector2(Mathf.Sin(fireAngle * Mathf.Deg2Rad), Mathf.Cos(fireAngle * Mathf.Deg2Rad));
                }
                // 2���� ���н��� ��� X���� ������ �Ǳ� ������ x���� right�� ���������� ������ ��������
                bullet.transform.right = direction;

                fireAngle += 180;
                if (fireAngle >= 360)
                {
                    fireAngle -= 360;
                }

                yield return waitForSeconds;
            }
            fireAngle += angle;
            angle += addAngle;
        }

        // �ӽ�
        gameObject.SetActive(false);
    }
}
