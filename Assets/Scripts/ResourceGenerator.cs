using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    
    private BuildingTypeSO buildingType;
    private float timer;
    private float timerMax;
    private int getResourceAmount = 0;

    private void Awake()
    {
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        timerMax = buildingType.resourceGeneratorData.timerMax;
        getResourceAmount = buildingType.resourceGeneratorData.resourceAmount;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer += timerMax;
            ResourceManager.Instance.AddResource(buildingType.resourceGeneratorData.resourceType, getResourceAmount);
        }
    }
}
