using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern1 : ShootingBase
{
    List<Bullet> bulletList = new List<Bullet>();

    int pos = 0;
    Vector2 fireDir = Vector2.zero;

    protected override void Init()
    {
        bulletFireObj = null;
    }

    private void OnEnable()
    {
        StartCoroutine(CrossFire());
    }

    public IEnumerator CrossFire()
    {
        StartCoroutine(CircleFire(transform.position, 5));

        for (int i = 0; i < 4; ++i)
        {
            if(i == 0)
                fireDir = Vector2.up;
            else if (i == 1)
                fireDir = Vector2.right;
            else if (i == 2)
                fireDir = Vector2.down;
            else if (i == 3)
                fireDir = Vector2.left;

            for (int j = 0; j < 50; ++j)
            {
                if(j % 5 == 0 && j != 0)
                {
                    yield return new WaitForSeconds(0.1f);
                }

                Bullet bullet = null;
                pos = j;
                pos = (pos % 5) - 2;

                if(fireDir == Vector2.up || fireDir == Vector2.down)
                    bullet = BulletPool.Instance.Pop(Vector3.zero + new Vector3(transform.position.x + pos, transform.position.y, 0));
                else if(fireDir == Vector2.left || fireDir == Vector2.right)
                    bullet = BulletPool.Instance.Pop(Vector3.zero + new Vector3(transform.position.x, transform.position.y + pos, 0));

                bullet.transform.right = fireDir;
                bullet.bulletSpd *= 3f;
            }

            yield return new WaitForSeconds(0.25f);
        }

        yield return new WaitForSeconds(0.25f);

        StartCoroutine(CircleFire(transform.position, 5));

        for (int i = 0; i < 50; ++i)
        {
            if(i % 5 == 0 && i != 0)
            {
                StartCoroutine(BulletAcceleration(bulletList.ToArray(), i * 0.5f));
                yield return new WaitForSeconds(0.1f);
                bulletList.Clear();
            }

            for (int j = 0; j < 4; ++j)
            {
                if (j == 0)
                    fireDir = Vector2.up;
                else if (j == 1)
                    fireDir = Vector2.right;
                else if (j == 2)
                    fireDir = Vector2.down;
                else if (j == 3)
                    fireDir = Vector2.left;

                Bullet bullet = null;
                pos = i;
                pos = (pos % 5) - 2;

                if (fireDir == Vector2.up || fireDir == Vector2.down)
                    bullet = BulletPool.Instance.Pop(Vector3.zero + new Vector3(transform.position.x + pos, transform.position.y, 0));
                else if (fireDir == Vector2.left || fireDir == Vector2.right)
                    bullet = BulletPool.Instance.Pop(Vector3.zero + new Vector3(transform.position.x, transform.position.y + pos, 0));

                bullet.transform.right = fireDir;
                bullet.bulletSpd *= 3f;
                bulletList.Add(bullet);
            }
        }

        gameObject.SetActive(false);

        yield break;
    }
}
