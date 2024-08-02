using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject Player1, Player2, Player3, Player4;
    Vector3 Player1Pos, Player2Pos, Player3Pos, Player4Pos;
    public static GameManager instance;
    [SerializeField] public int playerLives;
    public static event EventHandler OnRestartGame;
    public static event EventHandler OnWinGame;
    [SerializeField] public GameObject pauseMenu;
    int alivePlayers = 4;
    

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Player1Pos = Player1.transform.position;
        Player2Pos = Player2.transform.position;
        Player3Pos = Player3.transform.position;
        Player4Pos = Player4.transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            PauseGame();
        }

        if (alivePlayers == 1)
        {
            WinGame();
            alivePlayers = 0;
        }
    }
    public void RestartGame()
    {
        Player1.transform.position = Player1Pos;
        Player2.transform.position = Player2Pos;
        Player3.transform.position = Player3Pos;
        Player4.transform.position = Player4Pos;
        OnRestartGame?.Invoke(this, EventArgs.Empty);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
        
    }

    public void WinGame()
    {
        OnWinGame?.Invoke(this, EventArgs.Empty);
    }

    public void DecrementAlivePlayers()
    {
        alivePlayers--;
    }
}
