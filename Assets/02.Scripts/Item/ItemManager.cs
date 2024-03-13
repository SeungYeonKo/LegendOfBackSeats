using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public UnityEvent OnDataChanged;

    public static ItemManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public List<Item> ItemList = new List<Item>();

    private void Start()
    {
        ItemList.Add(new Item(ItemType.Health, 0));
        ItemList.Add(new Item(ItemType.Arrow, 5));
        OnDataChanged.Invoke();
    }

    // 1. 아이템 추가(생성)
    public void AddItem(ItemType itemType)
    {
        PlayerArrowFireAbility fireArrow = GameObject.FindWithTag("Player").GetComponent<PlayerArrowFireAbility>();
        int itemCountToAdd = 0;
        if (itemType == ItemType.Health)
        {
            itemCountToAdd = 1; // 체력 1개 추가
        }
        else if (itemType == ItemType.Arrow)
        {
            itemCountToAdd = 2; // 화살 2개 추가
            fireArrow.ArrowCurrentCount += 2;
            fireArrow.RefreshUI(); // 화살 UI 업데이트
        }

        // 수량을 업데이트
        foreach (var item in ItemList)
        {
            if (item.ItemType == itemType)
            {
                item.Count += itemCountToAdd;
                Debug.Log($"{itemType} 아이템추가");

                // 데이터 변경 이벤트를 호출
                OnDataChanged?.Invoke();
                return; // 아이템을 찾고 업데이트 했으니 반복문 종료
            }
        }
    }

    // 아이템 개수 조회
    public int GetItemCount(ItemType itemType)
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            if (ItemList[i].ItemType == itemType)
            {
                return ItemList[i].Count;
            }
        }
        return 0;
    }

    // 아이템 사용
    public bool TryUseItem(ItemType itemType)
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            if (ItemList[i].ItemType == itemType && ItemList[i].TryUse())
            {
                if (itemType == ItemType.Arrow)
                {
                    // 화살 아이템 사용 시 UI 업데이트
                    PlayerArrowFireAbility fireArrow = GameObject.FindWithTag("Player").GetComponent<PlayerArrowFireAbility>();
                    fireArrow.RefreshUI();
                    /*UI_ItemInventory uiupdate = GameObject.FindWithTag("Item").GetComponent<UI_ItemInventory>();
                    uiupdate.Refresh();*/
                }

                OnDataChanged?.Invoke();
                return true;
            }
        }
        return false;
    }

    void UpdatePlayerArrowCount(ItemType itemType)
    {
        if (itemType == ItemType.Arrow)
        {
            PlayerArrowFireAbility playerArrowFireAbility = GameObject.FindWithTag("Player").GetComponent<PlayerArrowFireAbility>();
            if (playerArrowFireAbility != null)
            {
                // ArrowCurrentCount를 업데이트
                playerArrowFireAbility.ArrowCurrentCount += 2;
            }
        }
    }
}

 