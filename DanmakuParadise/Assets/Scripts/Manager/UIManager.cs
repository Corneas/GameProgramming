using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField]
    private TextMeshProUGUI timeTmp = null;
    [SerializeField]
    private GameObject pausePanel = null;
    [SerializeField]
    private GameObject gameOverPanel = null;

    [SerializeField]
    private TextMeshProUGUI damageCountTMP = null;
    [SerializeField]
    private TextMeshProUGUI scoreTMP = null;
    [SerializeField]
    private TextMeshProUGUI remainingTimeTMP = null;

    private float remainingTime = 60f;

    private bool isGameEnd = false;

    private PlayerMove playerMove;

    [SerializeField]
    private GameObject[] heartArr = null;

    private void Start()
    {
        playerMove = FindObjectOfType<PlayerMove>();
    }

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
                GameOver();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePausePanel();
        }

    }

    public void UpdateHpUI(int damageCount)
    {
        heartArr[damageCount].SetActive(false);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);

        damageCountTMP.text = string.Format("DamagedCount : {0}", playerMove.damageCount);
        scoreTMP.text = string.Format("Score : {0}", 1000 - remainingTime * 10f);
        remainingTimeTMP.text = string.Format("remainingTime : {0}", remainingTime);
    }

    public void TogglePausePanel()
    {
        pausePanel.SetActive(!pausePanel.activeSelf);
        if (pausePanel.activeSelf)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }

    public void Resume()
    {
        TogglePausePanel();
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScene");
    }
}
