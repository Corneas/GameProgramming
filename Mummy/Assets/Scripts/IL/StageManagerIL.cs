using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManagerIL : MonoBehaviour
{
    public enum HINT_COLOR
    {
        Black = 0,
        Red,
        Green,
        Blue,
    }

    public HINT_COLOR hintColor = HINT_COLOR.Black;
    public Material[] hintMaterial;
    public string[] hintTag;

    private Renderer hintRenderer;
    private int prevTag = -1;

    private void Start()
    {
        hintRenderer = transform.Find("Hint").GetComponent<Renderer>();
    }

    public void InitStage()
    {
        int idx = 0;
        do
        {
            idx = Random.Range(0, hintMaterial.Length);
        }
        while (idx == prevTag);
        prevTag = idx;

        hintRenderer.material = hintMaterial[idx];
        //hintRenderer.gameObject.tag = hintTag[idx];

        hintColor = (HINT_COLOR)idx;
    }
}
