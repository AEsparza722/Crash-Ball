using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains;
using MoreMountains.Feedbacks;

public class BarrierFeedback : MonoBehaviour
{
    private MMF_Player mmf_Player;
    void Start()
    {
        mmf_Player = GetComponent<MMF_Player>();
    }

    private void OnEnable()
    {
        mmf_Player.PlayFeedbacks();
    }
}
