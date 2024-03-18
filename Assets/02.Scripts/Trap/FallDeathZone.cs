using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Gamemanager.Instance.GameOver();
            CameraManager.Instance.DefaultCamera.Follow = null;
        }
    }
}
