using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapArrow : MonoBehaviour
{
    Rigidbody _rigidbody;
    public float Speed = 7f;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = transform.up * Speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        ThirdPersonController thirdPersonController = other.GetComponent<ThirdPersonController>();
        if (thirdPersonController != null)
        {
            if (other.CompareTag("Player"))
            {
                thirdPersonController.Hit(10);
                
                Debug.Log("트랩화살에 맞음! :  체력 -1");
            }
        }
        else if (other.CompareTag("Wall"))
        {
            Debug.Log("벽에 쿵");
            gameObject.SetActive(false);
        }
    }
}
