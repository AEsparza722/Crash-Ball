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
        GameManager.OnWinGame += ZoomCamera;
    }
    public void ZoomCamera(object sender, EventArgs e)
    {
        zoomCamera.Priority = 11;
    }
}
