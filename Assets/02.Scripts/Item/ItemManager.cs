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
        ItemList.Add(new Item(ItemType.Arrow, 0));
        OnDataChanged.Invoke();
    }

    // 1. 아이템 추가(생성)
    public void AddItem(ItemType itemType)
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            if (ItemList[i].ItemType == itemType)
            {
                ItemList[i].Count++;

                if (OnDataChanged != null)
                {
                    OnDataChanged.Invoke();
                }
                break;
            }
        }
    }
}
    /*// 아이템 개수 조회
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
}*/