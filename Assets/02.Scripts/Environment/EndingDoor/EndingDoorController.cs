using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingDoorController : MonoBehaviour
{
    public GameObject Player;
    public GameObject UseKeyInteractionUI;


    public Animator _animator;
    public float DoorOpenDistance = 10f;

    private bool _isNear = false;

    void Start()
    {
        _animator = GetComponent<Animator>();

    }

    void Update()
    {
        
    }

    private void CheckPlayerDistance()
    {
        if (Vector3.Distance(Player.transform.position, transform.position) < DoorOpenDistance)
        {
            if (!_isNear && !_animator.GetBool("Opened")) // 플레이어가 근처에 처음 도달했을 때 한 번만 UI를 활성화
            {
                UseKeyInteractionUI.SetActive(true);
                _isNear = true;

            }
        }
        else
        {
            if (_isNear && !_animator.GetBool("Opened")) // 플레이어가 멀어졌을 때 한 번만 UI를 비활성화
            {
                UseKeyInteractionUI.SetActive(false);
                _isNear = false;
            }
        }
    }

}
