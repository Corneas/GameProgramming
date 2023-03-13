using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ResourceUI : MonoBehaviour
{
    private ResourceTypeListSO resourceTypeList;
    private Dictionary<ResourceTypeSO, Transform> resourceTypeTransformDictionary;

    private void Awake()
    {
        resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

        resourceTypeTransformDictionary = new Dictionary<ResourceTypeSO, Transform>();

        Transform resourceTemplate = transform.Find("ResourceTemplate");

        resourceTemplate.gameObject.SetActive(false);

        int index = 0;
        foreach(ResourceTypeSO resourceTypeItem in resourceTypeList.list)
        {
            Transform resourceTransform = Instantiate(resourceTemplate, transform);

            resourceTransform.gameObject.SetActive(true);

            resourceTransform.Find("Image").GetComponent<Image>().sprite = resourceTypeItem.resourceSprite;

            resourceTypeTransformDictionary[resourceTypeItem] = resourceTransform;

            index++;
        }

    }

    private void Start()
    {
        ResourceManager.Instance.OnResourceAmountChanged += ResourceManager_OnResourceAmountChanged;
        UpdateResourceAmount();
    }

    private void ResourceManager_OnResourceAmountChanged(object sender, EventArgs e)
    {
        UpdateResourceAmount();
    }

    private void UpdateResourceAmount()
    {
        foreach (ResourceTypeSO resourceTypeItem in resourceTypeList.list)
        {
            Transform resourceTransform = resourceTypeTransformDictionary[resourceTypeItem];

            int resourceAmount = ResourceManager.Instance.GetResourceAmount(resourceTypeItem);

            resourceTransform.Find("Text").GetComponent<TextMeshProUGUI>().text = resourceAmount.ToString();

        }
    }
}
