using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class EnemyWaveUI : MonoBehaviour
{
    private EnemyWaveManager enemyWaveManager;

    private TextMeshProUGUI waveNumberText;
    private TextMeshProUGUI waveMessageText;

    private void Awake()
    {
        waveNumberText = transform.Find("WaveNumberText").GetComponent<TextMeshProUGUI>();
        waveMessageText = transform.Find("WaveMessageText").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        enemyWaveManager = FindObjectOfType<EnemyWaveManager>();
        enemyWaveManager.OnWaveNumberChanged -= EnemyWaveManager_OnWaveManager;
        enemyWaveManager.OnWaveNumberChanged += EnemyWaveManager_OnWaveManager;
    }

    private void Update()
    {
        float nextWaveSpawnTimer = enemyWaveManager.GetNextWaveSpawnTimer();
        if(nextWaveSpawnTimer <= 0f)
        {
            SetWaveMessageText("");
        }
        else
        {
            SetWaveMessageText($"Next Wave in {nextWaveSpawnTimer.ToString("F1")}s");
        }
    }

    private void EnemyWaveManager_OnWaveManager(object sender, System.EventArgs e)
    {
        SetWaveNumberText($"Wave {enemyWaveManager.GetWaverNumber()}");
    }

    private void SetWaveNumberText(string text)
    {
        waveNumberText.SetText(text);
    }
    private void SetWaveMessageText(string message)
    {
        waveMessageText.SetText(message);
    }
}
