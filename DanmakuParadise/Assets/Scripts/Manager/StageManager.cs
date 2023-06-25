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
        // TODO : 발사된 총알 전부 Pool 및 보스 패턴 초기화

        bulletList = FindObjectsOfType<Bullet>();
        foreach(var bullet in bulletList)
        {
            bullet.Pool();
        }

        launcher.Init();
        //launcher.StartPattern();
    }
}
