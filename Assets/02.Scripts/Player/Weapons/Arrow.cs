using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // 화살을 쏘면 포물선 형태로 날아가도록 하고싶다.

    public float arrowSpeed = 30f; // 화살의 초기 속도
    public float shotInterval = 2f; // 화살을 발사하는 간격

    public TrailRenderer trailRenderer;


    private Rigidbody arrowRigidbody;

 

    void Awake()
    {
        arrowRigidbody = GetComponent<Rigidbody>();
        trailRenderer = GetComponentInChildren<TrailRenderer>();
        
    }


    private void Start()
    {

        StartCoroutine(ArrowEffect_Coroutine());

    }

    private void FixedUpdate() // 물리 연산은 FixedUpdate
    {
        return;

        if (arrowRigidbody.velocity.magnitude > 0.1f) // 속도가 충분히 클 때만 방향을 바꿉니다
        {
            // 속도 벡터 방향으로 보도록 회전
            Quaternion direction = Quaternion.LookRotation(arrowRigidbody.velocity.normalized);
            arrowRigidbody.rotation = Quaternion.Slerp(arrowRigidbody.rotation, direction, Time.fixedDeltaTime * 20f);
        }

        // 화살에 속도를 부여
        arrowRigidbody.GetComponent<Rigidbody>().velocity = transform.forward * arrowSpeed;

    }

    public void Shoot(Vector3 dir, float Power)
    {
        arrowRigidbody.velocity = Vector3.zero;
        arrowRigidbody.AddForce(dir * Power, ForceMode.Impulse);
    }

    private IEnumerator ArrowEffect_Coroutine()
    {
        yield return new WaitForSeconds(2f);
        trailRenderer.enabled = false;
    }

}
