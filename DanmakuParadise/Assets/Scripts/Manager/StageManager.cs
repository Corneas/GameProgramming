using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private Bullet[] bulletList;

    private Launcher launcher;

    private void Awake()
    {
        launcher = FindObjectOfType<Launcher>();
    }

    public void InitStage()
    {
        // TODO : �߻�� �Ѿ� ���� Pool �� ���� ���� �ʱ�ȭ

        bulletList = FindObjectsOfType<Bullet>();
        foreach(var bullet in bulletList)
        {
            bullet.Pool();
        }

        launcher.Init();
        //launcher.StartPattern();
    }
}
