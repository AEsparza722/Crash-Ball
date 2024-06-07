using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject Player1, Player2, Player3, Player4;
    Vector3 Player1Pos, Player2Pos, Player3Pos, Player4Pos;
    public static GameManager instance;
    

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
    public void RestartGame()
    {
        Player1.transform.position = Player1Pos;
        Player2.transform.position = Player2Pos;
        Player3.transform.position = Player3Pos;
        Player4.transform.position = Player4Pos;
    }

}
