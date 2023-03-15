using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePositionSortingOrder : MonoBehaviour
{
    [SerializeField]
    private bool runOnce;

    private SpriteRenderer spriteRenderer = null;

    private float percisionMultiplier = 5f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        spriteRenderer.sortingOrder = (int)(-(transform.position.y - transform.localPosition.y) * percisionMultiplier);

        if (runOnce)
        {
            Destroy(this);
        }
    }
}
