using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    private GameObject goodItem;
    [SerializeField]
    private GameObject badItem;

    [SerializeField]
    private int gooditemCount = 30;
    [SerializeField]
    private int badItemCount = 20;

    [SerializeField]
    private List<GameObject> goodItemList = new List<GameObject>();
    [SerializeField]
    private List<GameObject> badItemList = new List<GameObject>();

    public void SetStageObject()
    {
        foreach(var obj in goodItemList)
        {
            Destroy(obj);
        }

        foreach(var obj in badItemList)
        {
            Destroy(obj);
        }

        goodItemList.Clear();
        badItemList.Clear();

        // GoodItem 持失
        for(int i = 0; i < gooditemCount; ++i)
        {
            Vector3 pos = new Vector3(Random.Range(-23.0f, 23.0f), 0.05f, Random.Range(-23.0f, 23.0f));
            Quaternion rot = Quaternion.Euler(Vector3.up * Random.Range(0, 360));

            goodItemList.Add(Instantiate(goodItem, transform.position + pos, rot, transform));
        }

        // BadItem 持失
        for (int i = 0; i < badItemCount; ++i)
        {
            Vector3 pos = new Vector3(Random.Range(-23.0f, 23.0f), 0.05f, Random.Range(-23.0f, 23.0f));
            Quaternion rot = Quaternion.Euler(Vector3.up * Random.Range(0, 360));

            goodItemList.Add(Instantiate(badItem, transform.position + pos, rot, transform));
        }
    }

}
