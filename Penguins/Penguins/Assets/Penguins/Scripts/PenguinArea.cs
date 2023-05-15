using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PenguinArea : MonoBehaviour
{
    public PenguinAgent penguinAgent;
    public GameObject penguinBaby;
    public TextMeshPro cumulativeRewardText;
    public Fish fishPrefab;
    private List<GameObject> fishList = new List<GameObject>();
}
