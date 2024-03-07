using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // 화살을 쏘면 포물선 형태로 날아가도록 하고싶다.


    private Rigidbody arrowRigidbody;


    [Header("화살의 강도")]
    public float ArrowPower = 12f;



    void Start()
    {
        arrowRigidbody = GetComponent<Rigidbody>();
        arrowRigidbody.AddForce(ArrowPower * transform.forward, ForceMode.Impulse);
    }

    private void FixedUpdate() // 물리 연산은 FixedUpdate에서 처리하는 것이 좋음
    {
        if (arrowRigidbody.velocity.magnitude > 0.1f) // 속도가 충분히 클 때만 방향을 바꾸도록 
        {
            // 속도 벡터 방향으로 보도록 회전
            Quaternion direction = Quaternion.LookRotation(arrowRigidbody.velocity.normalized);
            arrowRigidbody.rotation = Quaternion.Slerp(arrowRigidbody.rotation, direction, Time.fixedDeltaTime * 20f);
        }

    }

}
