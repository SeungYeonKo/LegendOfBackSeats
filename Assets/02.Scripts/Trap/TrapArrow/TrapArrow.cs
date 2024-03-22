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
                thirdPersonController.Hit(1);
            }
        }
        else if (other.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }
    }
}
