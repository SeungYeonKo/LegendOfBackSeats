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
    private const float ZoomFOV = 30f;    // 최소 FOV
    private const float NormalFOV = 70f;  // 최대 FOV
    
    // 보간법 사용할 것
    private const float ZoomInDuration = 0.4f;   
    private const float ZoomOutDuration = 0.2f;  
    private float _zoomProgress; // 0~1
    private Vector3 _offset;

    // 화살 쿨타임
    /*    private float _arrow_Cool_Time = 3f;
        private float _arrow_Timer = 0f;*/
    private float _arrowPowerFactor;

    public Arrow ArrowPrefab; // 발사할 화살 객체

    private Animator _animator;   // 

    public Transform ArrowPlace;  // 화살 생성 위치

    private float _buttonDowntime;
    public float Power = 3f;
    private const float MAX_POWER = 200;

    public bool IsAiming;
    private bool _isFireable;


    private void Start()
    {
        _animator = GetComponent<Animator>();

        Power = 100f;
        _isFireable = true;
        _offset = new Vector3(0, 20, 0);

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && _isFireable)
        {
            _buttonDowntime = Time.time;
            _animator.SetTrigger("DrawArrow");

        }
        if (Input.GetMouseButton(1))
        {
            
            if (_zoomProgress < 1)
            {
                _zoomProgress += Time.deltaTime / ZoomInDuration;
                Vcam.m_Lens.FieldOfView = Mathf.Lerp(NormalFOV, ZoomFOV, _zoomProgress);
            }
/*            Power += Time.deltaTime * 2f;
            Power = Mathf.Min(MAX_POWER, Power);*/
        }

        if (Input.GetMouseButtonUp(1))
        {
/*            float heldTime = Time.time - _buttonDowntime;
            if (heldTime < 1f)
            {
                StartCoroutine(Shoot_Coroutine(1f - heldTime));
            }
            else if (heldTime >= 1f)
            {
                Debug.Log(heldTime);
                StartCoroutine(Shoot_Coroutine(0f));
            }*/
            _zoomProgress = 0;
            Power = 100f;
            _animator.SetTrigger("AimRecoil");
            Vcam.m_Lens.FieldOfView = NormalFOV;
        }


    }

    void FireArrow()
    {

        // 화살 인스턴스를 생성하고 위치 및 회전을 초기화
       Arrow arrowInstance = Instantiate<Arrow>(ArrowPrefab, ArrowPlace.position, Quaternion.identity);

        arrowInstance.transform.forward = Camera.main.transform.forward + _offset;


        arrowInstance.Shoot(Camera.main.transform.forward, Power);
    }
    private void SetAimingTrue()
    {
            IsAiming = true;
    }
    private void SetAimingFalse()
    {
        IsAiming = false;
    }    

}

