using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{ 
    public float FireBallSpeed = 2f;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Shoot(Vector3 targertPosition)
    {
        // 1.  방향을 구한다.
        Vector3 dir = targertPosition - transform.position ;
        dir.Normalize();
        _rigidbody.AddForce(dir * FireBallSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
