using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera zoomCamera;

    private void Awake()
    {
        GameManager.OnWinGame.AddListener(ZoomCamera);
    }
    public void ZoomCamera()
    {
        zoomCamera.Priority = 11;
    }
}
