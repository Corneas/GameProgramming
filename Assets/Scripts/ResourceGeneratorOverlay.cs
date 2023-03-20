using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceGeneratorOverlay : MonoBehaviour
{
    [SerializeField]
    private ResourceGenerator resourceGenerator;

    private ResourceGeneratorData resourceGeneratorData;

    private Transform barTransform;
    private Sprite sprite;
    private TextMeshPro text;

    private void Start()
    {
        resourceGenerator = GetComponentInParent<ResourceGenerator>();
        resourceGeneratorData = resourceGenerator.GetResourceGeneratorData();

        barTransform = transform.Find("Bar");
        transform.Find("Icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.resourceSprite;
        text = transform.Find("Text").GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        barTransform.localScale = new Vector3(resourceGenerator.GetTimerNormalized(), 1, 1);
        text.SetText(resourceGenerator.GetAmountGeneratorPerSecond().ToString());
    }


}
