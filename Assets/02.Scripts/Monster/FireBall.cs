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

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어에게 데미지를 입히기
        IHitable hitable = other.GetComponent<IHitable>();
        if (hitable != null)
        {
           // hitable.Hit(Damage);
        }

        Destroy(gameObject); // Fireball 파괴
    }
}
