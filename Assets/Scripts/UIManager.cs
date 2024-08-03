using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text Player1Text;
    public TMP_Text Player2Text;
    public TMP_Text Player3Text;
    public TMP_Text Player4Text;
    public TMP_Text winnerText;
    [SerializeField] IAController player1;
    [SerializeField] IAController player2;
    [SerializeField] IAController player3;
    [SerializeField] IAController player4;
    public static UIManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        Player1Text.text = "Player 1: " + player1.currentLives.ToString();
        Player2Text.text = "Player 2: " + player2.currentLives.ToString();
        Player3Text.text = "Player 3: " + player3.currentLives.ToString();
        Player4Text.text = "Player 4: " + player4.currentLives.ToString();
    }
}
