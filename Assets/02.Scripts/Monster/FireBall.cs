using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float FireBallSpeed = 2f;
    private Rigidbody _rigidbody;
    public int Damage = 5;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Shoot(Vector3 targertPosition)
    {
        // 1.  방향을 구한다.
        Vector3 dir = targertPosition - transform.position;
        dir.Normalize();
        _rigidbody.AddForce(dir * FireBallSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어에게만 데미지를 입히기
        if (other.CompareTag("Player")) // "Player"는 플레이어 게임오브젝트의 태그와 일치해야 합니다.
        {
            IHitable hitable = other.GetComponent<IHitable>();
            if (hitable != null)
            {
                hitable.Hit(Damage);
                Debug.Log("파이어볼에 맞았다!");
            }
            Destroy(gameObject); // 파이어볼 파괴
        }
    }
}
