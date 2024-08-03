using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    void Start()
    {
        GameManager.OnWinGame.AddListener(Deactivate);
    }

    void Deactivate()
    {
        Destroy(gameObject);        
    }
}
