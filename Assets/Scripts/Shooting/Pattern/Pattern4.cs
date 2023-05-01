using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern4 : ShootingBase
{
    private List<Bullet> bulletList = new List<Bullet>();

    protected override void Init()
    {
        bulletFireObj = null;
    }

    private void OnEnable()
    {
        StartCoroutine(AccVortex());
        StartCoroutine(MainAccVortex());
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

                // x��ǥ�� �ڻ���, y��ǥ�� �������� �Ҵ��Ͽ� �ݽð� �������� �����̴� ����
                Vector2 direction = new Vector2(Mathf.Cos(fireAngle * Mathf.Deg2Rad), Mathf.Sin(fireAngle * Mathf.Deg2Rad));
                // ������ ��ǥ��鿡���� �������� ���ؾ� ���� ���
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

    protected IEnumerator MainAccVortex()
    {
        float fireAngle = 0f;
        float angle = 2f;
        float acc = 0.5f;

        for (int i = 0; i < 45; ++i)
        {
            if (i == 30)
            {
                StartCoroutine(SubAccVortex());
            }

            for (int j = 0; j < 6; ++j)
            {
                Bullet bullet = null;

                bullet = BulletPool.Instance.Pop(transform.position);

                // x��ǥ�� �ڻ���, y��ǥ�� �������� �Ҵ��Ͽ� �ݽð� �������� �����̴� ����
                Vector2 direction = new Vector2(Mathf.Cos(fireAngle * Mathf.Deg2Rad), Mathf.Sin(fireAngle * Mathf.Deg2Rad));
                // ������ ��ǥ��鿡���� �������� ���ؾ� ���� ���
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
            acc += 0.125f;

        }

        StartCoroutine(CircleFire(transform.position, 2));

        yield break;
    }

    protected IEnumerator SubAccVortex()
    {
        float fireAngle = 0f;
        float angle = 2f;
        float acc = 3f;

        for (int i = 0; i < 45; ++i)
        {
            for (int j = 0; j < 6; ++j)
            {

                Bullet bullet = null;

                bullet = BulletPool.Instance.Pop(transform.position);

                // x��ǥ�� �ڻ���, y��ǥ�� �������� �Ҵ��Ͽ� �ݽð� �������� �����̴� ����
                Vector2 direction = new Vector2(Mathf.Sin(fireAngle * Mathf.Deg2Rad), Mathf.Cos(fireAngle * Mathf.Deg2Rad));
                // ������ ��ǥ��鿡���� �������� ���ؾ� ���� ���
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
