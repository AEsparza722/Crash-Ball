using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using MoreMountains.Feedbacks;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject Player1, Player2, Player3, Player4;
    Vector3 Player1Pos, Player2Pos, Player3Pos, Player4Pos;
    public static GameManager instance;
    [SerializeField] public int playerLives;
    public static UnityEvent OnRestartGame = new UnityEvent();
    public static UnityEvent OnWinGame = new UnityEvent();
    [SerializeField] public GameObject pauseMenu;
    [SerializeField] public GameObject winMenu;
    int alivePlayers = 4;
    public MMF_Player cameraShakeFeedback;
    

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
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        OnRestartGame?.Invoke();
    }

    public void ResetPositions()
    {
        Player1.transform.position = Player1Pos;
        Player2.transform.position = Player2Pos;
        Player3.transform.position = Player3Pos;
        Player4.transform.position = Player4Pos;
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
        OnWinGame?.Invoke();
        StartCoroutine(ShowWinnerPanel());
    }

    public void DecrementAlivePlayers()
    {
        alivePlayers--;
    }
    IEnumerator ShowWinnerPanel()
    {
        yield return new WaitForSeconds(2f);
        winMenu.SetActive(true);
    }

    public IEnumerator ShakeCamera(float delay)
    {
        yield return new WaitForSeconds(delay);
        cameraShakeFeedback.PlayFeedbacks();
    }
}
