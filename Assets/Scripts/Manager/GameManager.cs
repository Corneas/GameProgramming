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

}
