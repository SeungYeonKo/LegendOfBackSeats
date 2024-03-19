using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum Items
{
    Arrow,   // 화살 아이템
    Health,  // 체력 아이템
    Key      // 열쇠 아이템
}

public class ChestController : MonoBehaviour
{
    public GameObject Player;
    public GameObject ChestInteractionUI;
    public Animator _animator;
    public float ChestOpenDistance = 5f;


    private bool _isNear = false;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("Opened", false);
    }
    void Update()
    {
        CheckPlayerDistance();

        if (_isNear && Input.GetKeyDown(KeyCode.E))
        {

            OpenChest();
            _animator.SetBool("Opened", true);

        }

    }

    private void CheckPlayerDistance()
    {
        if (Vector3.Distance(Player.transform.position, transform.position) < ChestOpenDistance)
        {
            if (!_isNear && !_animator.GetBool("Opened")) // 플레이어가 근처에 처음 도달했을 때 한 번만 UI를 활성화
            {
                ChestInteractionUI.SetActive(true);
                _isNear = true;

            }
        }
        else
        {
            if (_isNear && !_animator.GetBool("Opened")) // 플레이어가 멀어졌을 때 한 번만 UI를 비활성화
            {
                ChestInteractionUI.SetActive(false);
                _isNear = false;
            }
        }
    }

    private void OpenChest()
    {
        Debug.Log("상자가 열렸다!");
        ChestInteractionUI.SetActive(false);
        _animator.SetTrigger("Open");
        _animator.SetBool("Opened", true);
    }
}
