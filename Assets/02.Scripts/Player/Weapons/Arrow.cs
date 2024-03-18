using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // 화살을 쏘면 포물선 형태로 날아가도록 하고싶다.

    public float arrowSpeed = 30f; // 화살의 초기 속도
    public float shotInterval = 2f; // 화살을 발사하는 간격

    private int damage = 3;  // 화살이 몬스터에게 입히는 데미지

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

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트가 몬스터인지 확인
        MonsterMove monster = collision.collider.GetComponent<MonsterMove>();
        if (monster != null)
        {
            // 몬스터의 TakeDamage 메서드를 호출하여 체력을 줄임
            monster.Hit(damage);
            this.gameObject.SetActive(false);


        }
       
    }
    private void OnCollisionStay(Collision collision)
    {
        GameObject gronds = collision.collider.gameObject;
    }
}
