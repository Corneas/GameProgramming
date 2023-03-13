using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    private BuildingTypeListSO btnTypeList;
    private Dictionary<BuildingTypeSO, Transform> btnTypeTransformDictionary;

    private void Awake()
    {
        btnTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);

        btnTypeTransformDictionary = new Dictionary<BuildingTypeSO, Transform>();

        Transform btnsTemplate = transform.Find("BtnTemplate");

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

            btnTypeTransformDictionary[btnTypeItem] = btnTransform;
        }
    }

}
