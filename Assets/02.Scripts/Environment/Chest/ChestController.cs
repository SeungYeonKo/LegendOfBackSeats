using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ChestController : MonoBehaviour
{
    public ItemType item;

   
    public GameObject Player;
    public GameObject ChestInteractionUI;


    [Header("상자 이펙트")]
    public GameObject ChestItemEffect1;
    public GameObject ChestItemEffect2;
    public GameObject ChestItemEffect3;

    [Header("Get Chest Text UI")]
    public TextMeshProUGUI GetChest_ArrowItemUI;
    public TextMeshProUGUI GetChest_HealthItemUI;
    public TextMeshProUGUI GetChest_KeyItemUI;
    
    public Animator _animator;
    public float ChestOpenDistance = 5f;

    public AudioSource ChestOpen_Audio;



    private bool _isNear = false;



    void Start()
    {
        _animator = GetComponent<Animator>();

        _animator.SetBool("Opened", false);

        ChestItemEffect1.SetActive(false);
        ChestItemEffect2.SetActive(false);
        ChestItemEffect3.SetActive(false);

        GetChest_ArrowItemUI.gameObject.SetActive(false);
        GetChest_HealthItemUI.gameObject.SetActive(false);
        GetChest_KeyItemUI.gameObject.SetActive(false);
    }


    void Update()
    {
        CheckPlayerDistance();

        if (_isNear && Input.GetKeyDown(KeyCode.E) && !_animator.GetBool("Opened"))
        {

            OpenChest();
            _animator.SetBool("Opened", true);
            StartCoroutine(ChestEffect_Coroutine());

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
        ChestOpen_Audio.Play();

        if (item == ItemType.Health)
        {
            GetChest_HealthItemUI.gameObject.SetActive(true);
            ItemManager.Instance.AddItem(ItemType.Health, 1);
            
        }
        else if (item == ItemType.Arrow)
        {
            GetChest_ArrowItemUI.gameObject.SetActive(true);
            ItemManager.Instance.AddItem(ItemType.Arrow, 2);
        }
        else if (item == ItemType.Key)
        {
            GetChest_KeyItemUI.gameObject.SetActive(true);
            ItemManager.Instance.AddItem(ItemType.Key, 1);
        }

        StartCoroutine(GetChestItemUi_Coroutine());

    }

    private IEnumerator ChestEffect_Coroutine()
    {
        yield return new WaitForSeconds(0.7f);
       ChestItemEffect1.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        ChestItemEffect2.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        ChestItemEffect3.SetActive(true);

    }

    private IEnumerator GetChestItemUi_Coroutine()
    {
        yield return new WaitForSeconds(3f);
        GetChest_HealthItemUI.gameObject.SetActive(false);
        GetChest_ArrowItemUI.gameObject.SetActive(false);
        GetChest_KeyItemUI.gameObject.SetActive(false);

    }
}
