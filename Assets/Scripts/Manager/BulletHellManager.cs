using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHellManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] bulletHellPatterns;

    int rand = 0;

    float timer = 0f;
    float timerMax = 15f;

    private void Start()
    {
        timer = timerMax;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            bulletHellPatterns[0].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            bulletHellPatterns[1].SetActive(true);
        }
    }

    public void FireBulletHellPattern()
    {
        if(timer >= timerMax)
        {
            timer = 0;

            rand = Random.Range(0, bulletHellPatterns.Length);

            bulletHellPatterns[rand].SetActive(true);
        }

    }

}
