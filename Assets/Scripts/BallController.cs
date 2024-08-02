using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    void Start()
    {
        GameManager.OnWinGame += Deactivate;
    }

    void Deactivate(object sender, EventArgs e)
    {
        gameObject.SetActive(false);
    }
}
