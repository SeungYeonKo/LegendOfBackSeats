using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // 화살을 쏘면 포물선 형태로 날아가도록 하고싶다.

    private Rigidbody arrowRigidbody;

    void Start()
    {
        arrowRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() // 물리 연산은 FixedUpdate
    {
        if (arrowRigidbody.velocity.magnitude > 0.1f) // 속도가 충분히 클 때만 방향을 바꿉니다
        {
            // 속도 벡터 방향으로 보도록 회전시킵니다
            Quaternion direction = Quaternion.LookRotation(arrowRigidbody.velocity.normalized);
            arrowRigidbody.rotation = Quaternion.Slerp(arrowRigidbody.rotation, direction, Time.fixedDeltaTime * 20f);
        }
    }

}
