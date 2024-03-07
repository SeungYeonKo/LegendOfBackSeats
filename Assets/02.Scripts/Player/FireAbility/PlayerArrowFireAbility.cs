using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrowFireAbility : MonoBehaviour
{
    public Arrow ArrowPrefab; // 발사할 화살 객체

    private Animator _animator;   // 

    public Transform ArrowPlace;  // 화살 생성 위치

    public float Power = 3f;
    private const float MAX_POWER = 200;

    private bool _isRightMouseClicked = false;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();

        Power = 100f;
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

            Power += Time.deltaTime * 2f;
            Power = Mathf.Min(MAX_POWER, Power);

            if (Input.GetMouseButtonDown(0)) // 왼쪽 마우스 버튼 클릭 시 발사
            {
                _animator.SetTrigger("AimRecoil");

                FireArrow();
            }
        }
        else
        {
            Power = 100f;
        }

        if (_isRightMouseClicked == false)
        {
            _animator.SetLayerWeight(1, 0);
        }

    }


    void FireArrow()
    {
        _animator = GetComponentInChildren<Animator>();

        // 화살 인스턴스를 생성하고 위치 및 회전을 초기화
       Arrow arrowInstance = Instantiate<Arrow>(ArrowPrefab, ArrowPlace.position, Quaternion.identity);

        arrowInstance.transform.forward = Camera.main.transform.forward;
        arrowInstance.Shoot(Camera.main.transform.forward, Power);
    }
}
