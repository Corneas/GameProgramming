using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    private List<Enemy> enemyList = new List<Enemy>();

    public float minPosX { get; private set; }
    public float maxPosX { get; private set; }
    public float minPosY { get; private set; }
    public float maxPosY { get; private set; }

    private void Awake()
    {
        minPosX = -40f;
        maxPosX = 40f;
        minPosY = -20f;
        maxPosY = 20f;
    }

    private void Start()
    {
        BulletPool.Instance.Pop(transform); // ������
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(DebugTestBullet());
        }
    }

    public void AddEnemyInEnemyList(Enemy enemy)
    {
        enemyList.Add(enemy);
    }

    public void RemoveEnemyInEnemyList(Enemy enemy)
    {
        enemyList.Remove(enemy);
    }

    public List<Enemy> GetEnemyList()
    {
        return enemyList;
    }

    public IEnumerator DebugTestBullet()
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

}
