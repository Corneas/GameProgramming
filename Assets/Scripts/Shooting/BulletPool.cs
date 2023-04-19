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
            bulletClone.gameObject.SetActive(false);
            bulletQueue.Enqueue(bulletClone);
        }
    }

    public Bullet Pop()
    {
        Debug.Log(bulletQueue.Count);

        if(bulletQueue.Count <= 0)
        {
            return Instantiate(bullet);
        }
        else
        {
            Bullet bulletClone = bulletQueue.Dequeue();
            bulletClone.gameObject.SetActive(true);
            bulletClone.transform.SetParent(null);
            return bulletClone;
        }

    }

    public void Push(Bullet bullet)
    {
        bullet.transform.SetParent(transform);
        bullet.gameObject.SetActive(false);
        bulletQueue.Enqueue(bullet);
    }



}
