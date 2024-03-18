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
        if (other.tag == "Player")
        {
            Debug.Log("아야!!!");
        }
        else if (other.tag == "Wall")
        {
            Debug.Log("벽에 쿵");
            gameObject.SetActive(false);
        }
    }
}
