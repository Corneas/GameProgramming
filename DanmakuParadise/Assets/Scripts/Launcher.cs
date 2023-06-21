using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    private WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

    private bool isPattern = false;

    private int prePattern = 0;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        StartCoroutine(CheckPattern());
    }

    public void Init()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void StartPattern()
    {
        StartCoroutine(CheckPattern());
    }

    int rand = 0;
    public IEnumerator CheckPattern()
    {
        while (true)
        {
            isPattern = false;
            for (int i = 0; i < transform.childCount; ++i)
            {
                if (transform.GetChild(i).gameObject.activeSelf)
                {
                    isPattern = true;
                    break;
                }
            }

            if (!isPattern)
            {
                yield return new WaitForSeconds(1f);


                rand = Random.Range(0, transform.childCount);
                if(prePattern == rand)
                {
                    while(prePattern == rand)
                    {
                        rand = Random.Range(0, transform.childCount);
                    }
                }

                prePattern = rand;
                transform.GetChild(rand).gameObject.SetActive(true);
            }

            yield return waitForEndOfFrame;
        }
    }
}
