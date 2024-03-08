using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{

    public CinemachineVirtualCamera Vcam;
    private const float _zoomFOV = 30f;  // 최소 FOV
    private const float _normalFOV = 70f;  // 최대 FOV

    private bool _isZoomMode = false;


    void Start()
    {
        
    }

    void Update()
    {

    }
}
