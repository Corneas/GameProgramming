using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timeTmp = null;

    private float remainingTime = 60f;

    private bool isGameEnd = false;

    private void Update()
    {
        if (!isGameEnd)
        {
            remainingTime -= Time.deltaTime;

            timeTmp.text = string.Format("{0:N2}", remainingTime);

            if (remainingTime < 0f)
            {
                remainingTime = 0f;
                isGameEnd = true;
            }
        }
    }
}
