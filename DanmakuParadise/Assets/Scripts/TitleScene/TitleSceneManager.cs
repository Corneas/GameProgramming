using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneManager : MonoBehaviour
{

    public void GameStart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void AIGame()
    {
        SceneManager.LoadScene("AIScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
