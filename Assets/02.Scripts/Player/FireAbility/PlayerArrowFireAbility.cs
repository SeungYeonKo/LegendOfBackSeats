using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerArrowFireAbility : MonoBehaviour
{
    // 화살 구현 방법: 
    // - 조준: 마우스 오른쪽 버튼
    // - 발사: 마우스 오른쪽 버튼을 누르고 있는 동안 마우스 왼쪽 버튼 누르면 발사
    

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

    public float Power = 3f;
    private const float MAX_POWER = 200;

    public bool IsAiming;

    // UI
    public GameObject AimUI;

    // 현재 화살 개수 텍스트
    public TextMeshProUGUI ArrowCountText;

    // 화살 없을 때 띄우는 텍스트
    public TextMeshProUGUI NoArrowTextUI;

    public AudioSource BowDrawSound;
    public AudioSource BowShotSound;




    private void Start()
    {
        NoArrowTextUI.text = string.Empty;
        _animator = GetComponent<Animator>();
        Power = 100f;
        _offset = new Vector3(0, 20, 0);
   
        AimUI.SetActive(false);
    }

    private void Update()
    {
        if (ItemManager.Instance.GetItemCount(ItemType.Arrow) >  0 && Gamemanager.Instance.State == GameState.Go)
        {
            if (Input.GetMouseButtonDown(1))
            {
                BowDrawSound.Play();
                _animator.SetTrigger("DrawArrow");
                if (_animator.)
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
                _zoomProgress = 0;
                Power = 100f;
                
                BowShotSound.Play();
                _animator.SetTrigger("AimRecoil");
                Vcam.m_Lens.FieldOfView = NormalFOV;
            }
        }
        if(Input.GetMouseButtonDown(1) && Gamemanager.Instance.State == GameState.Go)
        {
            if (ItemManager.Instance.GetItemCount(ItemType.Arrow) <= 0)
            {
                    StartCoroutine(ShowNoArrowMessage());
            }
        }
    }

    void FireArrow()
    {
        // 화살 발사 로직
        Arrow arrowInstance = Instantiate<Arrow>(ArrowPrefab, ArrowPlace.position, Quaternion.identity);
        arrowInstance.transform.forward = Camera.main.transform.forward + _offset;
        arrowInstance.Shoot(Camera.main.transform.forward, Power);

        Debug.Log("화살 사용됨!");

        ItemManager.Instance.TryUseItem(ItemType.Arrow);


        if (ItemManager.Instance.GetItemCount(ItemType.Arrow) <= 0)
        { 
            return; // 화살이 없으면 메소드 종료
        }
    }
    IEnumerator ShowNoArrowMessage()
    {
        NoArrowTextUI.gameObject.SetActive(true); // 메시지 표시
        NoArrowTextUI.text = "화살이 없습니다!";
        yield return new WaitForSeconds(2f);
        NoArrowTextUI.gameObject.SetActive(false); // 메시지 숨김
    }
    private void SetAimingTrue()
    {
          IsAiming = true;
        AimUI.SetActive(true);
    }

    private void SetAimingFalse()
    {
        IsAiming = false;
        AimUI.SetActive(false);
    }
}

