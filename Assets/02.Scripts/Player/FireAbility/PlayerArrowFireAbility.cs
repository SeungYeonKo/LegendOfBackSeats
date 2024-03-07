using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrowFireAbility : MonoBehaviour
{
    public GameObject ArrowPrefab; // 발사할 화살 객체
    public Transform ArrowContainer; // 화살이 위치할 곳

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

            _animator.SetTrigger("DrawArrow");

            if (Input.GetMouseButtonDown(0)) // 왼쪽 마우스 버튼 클릭 시 발사
            {
                _animator.SetTrigger("AimRecoil");

                // 화살 발사각도 = 20 + 20i (20, 40, 60)
                for (int i = 0; i < 3; i++)
                {
                    GameObject tempObject = Instantiate(ArrowPrefab, ArrowContainer);
                    Vector3 direction = new Vector3(Mathf.Cos((20 + 20 * i) * Mathf.Deg2Rad), 0, Mathf.Sin((20 + 20 * i) * Mathf.Deg2Rad));
                    tempObject.transform.forward = direction; // 화살의 방향을 설정합니다.
                    tempObject.transform.position = transform.position + shotInterval * direction;
                    // shotInterval * direction은 화살이 플레이어 앞에서 발사되도록 자연스럽게 만들어주기 위해
                    // direction을 곱해 발사 각도에 맞게 발사 위치를 띄웁니다. 이는 화살이 나아가고자 하는 방향으로 간격을 띄우는 것입니다.
                }
            }
        }

        if (_isRightMouseClicked == false)
        {

        }


    }

}
