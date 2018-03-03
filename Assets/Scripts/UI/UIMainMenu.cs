using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    public Canvas HowToPlay;

    public void StartGame()
    {
        SceneManager.LoadSceneAsync("Game View");
    }

    public void OpenHowToPlay()
    {
        HowToPlay.enabled = true;
    }

    public void CloseHowToPlay()
    {
        HowToPlay.enabled = false;
    }

    public void Quit()
    {
        Application.Quit();
    }
}