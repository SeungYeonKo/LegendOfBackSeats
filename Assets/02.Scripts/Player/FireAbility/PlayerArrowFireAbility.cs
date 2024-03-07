using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrowFireAbility : MonoBehaviour
{
    public GameObject ArrowPrefab; // 발사할 화살 객체
    public float arrowSpeed = 30f; // 화살의 초기 속도

    private Animator _animator;

    public float shotInterval = 2f; // 화살을 발사하는 간격

    private bool _isRightMouseClicked = false;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {

        _isRightMouseClicked = false;

        // 오른쪽 마우스 버튼 클릭 시 조준
        if (Input.GetMouseButton(1))
        {
            _isRightMouseClicked = true;
            _animator.SetLayerWeight(1, 1);

            _animator.SetTrigger("DrawArrow");

            if (Input.GetMouseButtonDown(0)) // 왼쪽 마우스 버튼 클릭 시 발사
            {
                _animator.SetTrigger("AimRecoil");

                FireArrow();
            }
        }

        if (_isRightMouseClicked == false)
        {
            _animator.SetLayerWeight(1, 0);
        }

    }


    void FireArrow()
    {
        // 화살 인스턴스를 생성하고 위치 및 회전을 초기화합니다
        GameObject arrowInstance = Instantiate(ArrowPrefab, transform.position, Quaternion.identity);
        // 화살에 속도를 부여합니다
        arrowInstance.GetComponent<Rigidbody>().velocity = transform.forward * arrowSpeed;
    }
}
