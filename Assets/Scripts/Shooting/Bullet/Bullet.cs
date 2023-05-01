using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpd = 5f;
    [SerializeField]
    private int bulletDamage = 30;

    private List<Enemy> enemies;

    private Vector3 initPos;

    [SerializeField]
    private float attackRange = 2f;

    private bool isPool = false;

    private void OnEnable()
    {
        isPool = false;
        bulletSpd = 5f;
        initPos = transform.position;
    }

    private void Update()
    {
        CollisionObject();

        Move();
    }

    private void FixedUpdate()
    {
        LimitMove();
    }

    private void Move()
    {
        transform.Translate(Vector3.right * Time.deltaTime * bulletSpd, Space.Self);
    }

    private void LimitMove()
    {
        //if (transform.position.x > initPos.x + GameManager.Instance.maxPosX
        //    || transform.position.x < initPos.x + GameManager.Instance.minPosX
        //    || transform.position.y > initPos.y + GameManager.Instance.maxPosY
        //    || transform.position.y < initPos.y + GameManager.Instance.minPosY)
        //{
        //    if (!isPool)
        //    {
        //        isPool = true;
        //        Debug.Log("limit");
        //        Pool();
        //    }
        //}
        if (transform.position.x > initPos.x + GameManager.Instance.maxPosX)
        {
            Pool();
        }
        else if (transform.position.x < initPos.x + GameManager.Instance.minPosX)
        {
            Pool();
        }
        else if (transform.position.y > initPos.y + GameManager.Instance.maxPosY)
        {
            Pool();
        }
        else if (transform.position.y < initPos.y + GameManager.Instance.minPosY)
        {
            Pool();
        }
    }

    private void CollisionObject()
    {
        enemies = GameManager.Instance.GetEnemyList();

        for(int i = 0; i < enemies.Count; ++i)
        {
            if (enemies[i] == null)
                return;

            if(Vector2.Distance(gameObject.transform.position, enemies[i].transform.position) < attackRange)
            {
                if (enemies[i] == null)
                    return;
                if(enemies[i].healthSystem == null)
                {
                    Debug.Log("healthSystem is null");
                    return;
                }
                enemies[i].healthSystem.Damage(bulletDamage);
                Pool();
            }
        }
    }

    private void Pool()
    {
        //Debug.Log("Pool");
        BulletPool.Instance.Push(this);
    }
}
