using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    public event EventHandler OnResourceAmountchanged;

    private Dictionary<ResourceTypeSO, int> resourceAmountDictionary;

    private void Awake()
    {
        Instance = this;

        resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();

        ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

        foreach (ResourceTypeSO resourceType in resourceTypeList.list)
        {
            resourceAmountDictionary[resourceType] = 0;
        }

        TestLogResourceAmountDictionary();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        //    AddResource(resourceTypeList.list[0], 1);
        //}
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        //    AddResource(resourceTypeList.list[1], 1);
        //}
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        //    AddResource(resourceTypeList.list[2], 1);
        //}
    }

    private void TestLogResourceAmountDictionary()
    {
        foreach(ResourceTypeSO resourceType in resourceAmountDictionary.Keys)
        {
            Debug.Log(resourceType.nameString + ": " + resourceAmountDictionary[resourceType]);
        }
    }

    public void AddResource(ResourceTypeSO resourceType, int amount)
    {
        resourceAmountDictionary[resourceType] += amount;

        OnResourceAmountchanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetResourceAmount(ResourceTypeSO resourceType)
    {
        return resourceAmountDictionary[resourceType];
    }

    public bool CanAfford(ResourceAmount[] resourceAmountArray)
    {
        foreach (ResourceAmount resourceAmount in resourceAmountArray)
        {
            if (GetResourceAmount(resourceAmount.resourceType) >= resourceAmount.amount)
            {
                // Can afford
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    public void SpendResources(ResourceAmount[] resourceAmountArray)
    {
        foreach (ResourceAmount resourceAmount in resourceAmountArray)
        {
            resourceAmountDictionary[resourceAmount.resourceType] -= resourceAmount.amount;
        }
    }
}
