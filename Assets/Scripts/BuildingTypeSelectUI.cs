using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    private Dictionary<BuildingTypeSO, Transform> btnTransformDicionary;

    private BuildingTypeListSO btnTypeList;

    [SerializeField]
    private Sprite arrowSpr;

    private Transform arrowBtn;

    private void Awake()
    {
        btnTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);

        btnTransformDicionary = new Dictionary<BuildingTypeSO, Transform>();
        

        Transform btnsTemplate = transform.Find("BtnTemplate");

        arrowBtn = Instantiate(btnsTemplate, transform);
        arrowBtn.gameObject.SetActive(true);

        arrowBtn.Find("Image").GetComponent<Image>().sprite = arrowSpr;

        arrowBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            BuildingManager.Instance.SetActiveBuildingType(null);
        });

        btnsTemplate.gameObject.SetActive(false);

        foreach (BuildingTypeSO btnTypeItem in btnTypeList.list)
        {
            Transform btnTransform = Instantiate(btnsTemplate, transform);

            btnTransform.gameObject.SetActive(true);

            btnTransform.Find("Image").GetComponent<Image>().sprite = btnTypeItem.prefab.GetComponentInChildren<SpriteRenderer>().sprite;

            btnTransform.GetComponent<Button>().onClick.AddListener(() => 
            {
                BuildingManager.Instance.SetActiveBuildingType(btnTypeItem);
            });

            btnTransformDicionary[btnTypeItem] = btnTransform;
        }
    }

    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged -= BuildingManager_OnActiveBuildingTypeChanged;
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
    }

    private void BuildingManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
    {
        UpdateActiveBuildingTypeButton();
    }

    private void UpdateActiveBuildingTypeButton()
    {
        arrowBtn.Find("Selected").gameObject.SetActive(false);
        foreach (BuildingTypeSO buildingType in btnTransformDicionary.Keys)
        {
            Transform btnTransform = btnTransformDicionary[buildingType];
            btnTransform.Find("Selected").gameObject.SetActive(false);
        }

        BuildingTypeSO activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();

        if (activeBuildingType == null)
        {
            arrowBtn.Find("Selected").gameObject.SetActive(true);
        }
        else
        {
            btnTransformDicionary[activeBuildingType].Find("Selected").gameObject.SetActive(true);
        }
    }

}
