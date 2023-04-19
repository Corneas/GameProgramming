using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float bulletSpd = 5f;
    [SerializeField]
    private int bulletDamage = 30;

    private List<Enemy> enemies;

    private void OnEnable()
    {
        bulletSpd = 5f;
        Invoke("Pool", 3f);
    }

    private void Update()
    {
        CollisionObject();

        Move();
    }



    private void Move()
    {
        transform.Translate(transform.right * Time.deltaTime * bulletSpd, Space.Self);
    }

    private void CollisionObject()
    {
        enemies = GameManager.Instance.GetEnemyList();

        for(int i = 0; i < enemies.Count; ++i)
        {
            if(Vector2.Distance(gameObject.transform.position, enemies[i].transform.position) < 2f)
            {
                enemies[i].healthSystem.Damage(bulletDamage);
                BulletPool.Instance.Push(this);
            }
        }
    }

    private void Pool()
    {
        BulletPool.Instance.Push(this);
    }
}
