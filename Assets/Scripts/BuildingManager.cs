using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    //[SerializeField] private Transform pfWoodHarvester;
    //[SerializeField] private BuildingTypeSO buildingType;
    public static BuildingManager Instance { private set; get; }

    public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;

    public class OnActiveBuildingTypeChangedEventArgs : EventArgs
    {
        public BuildingTypeSO activeBuildingType;
    }

    private BuildingTypeSO activeBuildingType;
    private BuildingTypeListSO buildingTypeList;
    
    private Camera mainCamera;

    private void Awake()
    {
        Instance = this;
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        activeBuildingType = buildingTypeList.list[0];
    }
    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if(activeBuildingType != null)
            {
                if(CanSpawnBuilding(activeBuildingType, Utils.GetMouseWorldPosition()))
                    Instantiate(activeBuildingType.prefab, Utils.GetMouseWorldPosition(), Quaternion.identity);
            }
        }

    }

    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        activeBuildingType = buildingType;
        OnActiveBuildingTypeChanged?.Invoke(this, new OnActiveBuildingTypeChangedEventArgs { activeBuildingType = activeBuildingType });
    }

    public BuildingTypeSO GetActiveBuildingType()
    {
        return activeBuildingType;
    }

    private bool CanSpawnBuilding(BuildingTypeSO buildingTypeSO, Vector3 pos)
    {
        BoxCollider2D boxCollider2D = buildingTypeSO.prefab.GetComponent<BoxCollider2D>();


        Collider2D[] collider2DArr = Physics2D.OverlapBoxAll(pos + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);

        bool isAreaClear = collider2DArr.Length == 0;

        if (!isAreaClear)
        {
            return false;
        }

        collider2DArr = Physics2D.OverlapCircleAll(pos, buildingTypeSO.minConstructionRadius);

        foreach(Collider2D col in collider2DArr)
        {
            BuildingTypeHolder buildingTypeHolder = col.GetComponent<BuildingTypeHolder>();
            if(buildingTypeHolder != null)
            {
                if(buildingTypeHolder.buildingType == buildingTypeSO)
                {
                    return false;
                }
            }
        }

        return true;
    }
}
