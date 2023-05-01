using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoSingleton<BulletPool>
{
    private Bullet bullet;
    private Queue<Bullet> bulletQueue = new Queue<Bullet>();

    private void Awake()
    {
        bullet = Instantiate(GameAssets.Instance.pfBullet, transform).GetComponent<Bullet>();
        bullet.gameObject.SetActive(false);

        CreatePool();
    }

    private void CreatePool()
    {
        for(int i = 0; i < 100; ++i)
        {
            Bullet bulletClone = Instantiate(bullet, transform);
            bulletClone.transform.SetParent(transform);
            bulletClone.gameObject.SetActive(false);
            bulletQueue.Enqueue(bulletClone);
        }
    }

    public Bullet Pop(Transform pos, Transform parent = null)
    {
        //Debug.Log("BulletCount : " + bulletQueue.Count);

        Bullet bulletClone = null;

        if(bulletQueue.Count <= 0)
        {
            //Debug.Log("Instantiate");
            bulletClone = Instantiate(bullet, pos.position, Quaternion.identity);
        }
        else
        {
            bulletClone = bulletQueue.Dequeue();
        }

        bulletClone.gameObject.transform.position = pos.position;
        bulletClone.gameObject.SetActive(true);
        bulletClone.transform.SetParent(parent);
        return bulletClone;

    }

    public void Push(Bullet bullet)
    {
        //Debug.Log("push");
        bulletQueue.Enqueue(bullet);
        bullet.gameObject.SetActive(false);
        bullet.transform.SetParent(transform);
    }
}
