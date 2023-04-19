using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private List<Enemy> enemyList = new List<Enemy>();

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

    private void Start()
    {
        BulletPool.Instance.Pop();
    }

}
