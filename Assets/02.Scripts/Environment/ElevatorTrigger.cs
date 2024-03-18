using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTrigger : MonoBehaviour
{
    private Elevator _elevator;
    private float _timer = 0;
    public float ActivatedTime = 0.5f;
    private void Awake()
    {
        _elevator = GetComponentInParent<Elevator>();
        _timer = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        _timer += Time.unscaledDeltaTime;
        if (other.CompareTag("Player") && _timer > ActivatedTime)
        {
            _elevator.TriggerActivated = true;
            _timer = 0;
        }
    }
}
