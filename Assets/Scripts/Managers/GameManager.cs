using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public int ShipPiecesToCollect = 3;

    private bool gameOver;

    private void Awake()
    {
        TimeManager.Instance.OnTimeChanged.AddListener(OnTimeChanged);
    }

    public void OnTimeChanged(int seconds)
    {
        if (gameOver)
        {
            return;
        }

        if (seconds <= 0)
        {
            gameOver = true;

            GameOver();
        }
    }

    private void GameOver()
    {
        SceneManager.LoadScene("Game Over");
    }
}