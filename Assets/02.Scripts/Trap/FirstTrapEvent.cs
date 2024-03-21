using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTrapEvent : MonoBehaviour
{
    private BoxCollider _collider;
    public CinemachineVirtualCamera TrapCam;
    public float SwitchTime = 3f;
    // Start is called before the first frame update
    private void Start()
    {
        _collider = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(SwitchCam_Coroutine());
    }
    private IEnumerator SwitchCam_Coroutine()
    {
        TrapCam.Priority = 12;
        yield return new WaitForSeconds(SwitchTime);
        TrapCam.Priority = 0;
    }
}
