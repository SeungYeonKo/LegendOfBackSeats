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
        int itemCountToAdd = 0;
        if (itemType == ItemType.Health)
        {
            itemCountToAdd = 1; // 체력 1개 추가
        }
        else if (itemType == ItemType.Arrow)
        {
            itemCountToAdd = 2; // 화살 2개 추가
        }

        // 수량을 업데이트
        foreach (var item in ItemList)
        {
            if (item.ItemType == itemType)
            {
                item.Count += itemCountToAdd;
                Debug.Log($"{itemType} 아이템추가.  현재 개수: {item.Count}");

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
            if (ItemList[i].ItemType == itemType)
            {
                bool result = ItemList[i].TryUse();

                if (OnDataChanged != null)
                {
                    OnDataChanged.Invoke();
                }
                return result;
            }
        }
        return false;
    }
}

 