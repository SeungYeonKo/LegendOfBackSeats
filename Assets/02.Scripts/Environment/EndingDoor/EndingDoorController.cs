using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingDoorController : MonoBehaviour
{
    public GameObject Player;
    public GameObject UseKeyInteractionUI;


    public Animator _animator;
    public float DoorOpenDistance = 5f;

    private bool _isNear = false;

    public AudioSource DoorOpen_Audio;


    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("Opened", false);
    }

    void Update()
    {
        CheckPlayerDistance();


        if (_isNear && Input.GetKeyDown(KeyCode.E) /* && ItemManager.Instance.ItemList[2].Count >= 1*/)
        {

            OpenEndingDoor();
            _animator.SetBool("Opened", true);
            
            DoorOpen_Audio.Play();
        }

    }

    private void CheckPlayerDistance()
    {
        if (Vector3.Distance(Player.transform.position, transform.position) < DoorOpenDistance)
        {
            if (!_isNear && !_animator.GetBool("Opened") /* &&ItemManager.Instance.ItemList[2].Count >= 1*/) // 플레이어가 근처에 처음 도달했을 때 (열쇠가 있을 경우) 한 번만 UI를 활성화
            {
                Debug.Log("플레이어 열쇠 사용 가능");
                UseKeyInteractionUI.SetActive(true);
                _isNear = true;
            }
        }
        else
        {
            if (_isNear && !_animator.GetBool("Opened")/* &&ItemManager.Instance.ItemList[2].Count >= 1*/) // 플레이어가 멀어졌을 때 한 번만 UI를 비활성화
            {
                UseKeyInteractionUI.SetActive(false);
                _isNear = false;
            }
        }
    }
    private void OpenEndingDoor()
    {
        Debug.Log("문이 열렸다!");
       UseKeyInteractionUI.SetActive(false);
        _animator.SetTrigger("Open");
        _animator.SetBool("Opened", true);
    }
}
