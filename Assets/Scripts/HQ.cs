using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HQ : MonoBehaviour
{
    [SerializeField]
    private GameObject pfBulletHellBtn = null;

    private void Start()
    {
        pfBulletHellBtn = transform.Find("pfBulletHellBtn").gameObject;
        HideBulletHellBtn();
    }

    private void OnMouseEnter()
    {
        ShowBulletHellBtn();
    }

    private void OnMouseExit()
    {
        HideBulletHellBtn();
    }

    private void ShowBulletHellBtn()
    {
        if (pfBulletHellBtn != null)
        {
            pfBulletHellBtn.gameObject.SetActive(true);
        }
    }

    private void HideBulletHellBtn()
    {
        if (pfBulletHellBtn != null)
        {
            pfBulletHellBtn.gameObject.SetActive(false);
        }
    }
}
