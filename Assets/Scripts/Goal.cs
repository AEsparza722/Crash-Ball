using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    //[SerializeField] Players player;
    [SerializeField] IAController player;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Ball"))
        {
            player.TakeGoal();
        }
    }
}
