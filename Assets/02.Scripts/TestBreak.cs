using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBreak : MonoBehaviour, IHitable
{
    public Animator _animator;
    public bool IsFirstWall;
    private BoxCollider _collider;

    void Start()
    {

    }
    public void Hit(int amount)
    {
        _animator.SetBool("Break", true);
        _collider = GetComponent<BoxCollider>();
        _collider.enabled = false;
        // todo: 카메라 2초간 비춰주기

    }
    public void CloseUpCamera()
    {

    }

/*    private IEnumerator SwitchCameraCoroutine()
    {
        // 대체 카메라 활성화
        alternateCamera.gameObject.SetActive(true);
        mainCamera.gameObject.SetActive(false);

        // 2초간 대체 카메라로 유지
        yield return new WaitForSeconds(2f);

        // 기본 카메라로 전환
        alternateCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);

        isSwitching = false;
    }*/
}
