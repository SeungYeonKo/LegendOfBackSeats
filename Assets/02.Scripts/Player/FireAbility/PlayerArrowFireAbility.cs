using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrowFireAbility : MonoBehaviour
{
    // 화살 구현 방법: 
    // - 조준: 마우스 오른쪽 버튼
    // - 발사: 마우스 오른쪽 버튼을 누르고 있는 동안 마우스 왼쪽 버튼 누르면 발사
    // 

    [Header("플레이어 활 쏠때")]
    public CinemachineVirtualCamera Vcam;
    private const float ZoomFOV = 35f;    // 최소 FOV
    private const float NormalFOV = 70f;  // 최대 FOV
    
    // 보간법 사용할 것
    private const float ZoomInDuration = 0.4f;   
    private const float ZoomOutDuration = 0.2f;  
    private float _zoomProgress; // 0~1



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
        //_isRightMouseClicked = false;

        if (_isRightMouseClicked)
        {

        }


        // 오른쪽 마우스 버튼 클릭 시 조준
        if (Input.GetMouseButtonDown(1))
        {
            
            _isRightMouseClicked = true;
            _animator.SetLayerWeight(1, 1);

            _animator.SetTrigger("DrawArrow");

        }

        else if (Input.GetMouseButton(1))
        {

            if (_zoomProgress < 1 && _isRightMouseClicked)
            {
                _zoomProgress += Time.deltaTime / ZoomInDuration;
                Vcam.m_Lens.FieldOfView = Mathf.Lerp(NormalFOV, ZoomFOV, _zoomProgress);
            }

            Power += Time.deltaTime * 2f;
            Power = Mathf.Min(MAX_POWER, Power);


        }
        else if (Input.GetMouseButtonUp(1))
        {
            _zoomProgress = 0;

            Power = 100f;
            _animator.SetTrigger("AimRecoil");

            FireArrow();
            _isRightMouseClicked = false;

            _animator.SetLayerWeight(1, 0);
            Vcam.m_Lens.FieldOfView = NormalFOV;

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
