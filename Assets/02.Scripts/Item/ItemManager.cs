using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
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
    public void AddItem(ItemType itemType, int count)
    {
        // 수량을 업데이트
        foreach (var item in ItemList)
        {
            if (item.ItemType == itemType)
            {
                item.Count += count;
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
            if (ItemList[i].ItemType == itemType )
            {
               ItemList[i].Count -= 1;
                switch (itemType)
                {
                    case ItemType.Health:
                    {
                        ThirdPersonController thirdPersonController = GameObject.FindWithTag("Player").GetComponent<ThirdPersonController>();
                        if (thirdPersonController.CurrentHealth < thirdPersonController.MaxHealth)
                        {
                            // Health아이템 사용시 플레이어 체력 +5, 최대 체력을 초과하지 않게 함
                            int healthToAdd = Mathf.Min(5, thirdPersonController.MaxHealth - thirdPersonController.CurrentHealth);
                            thirdPersonController.CurrentHealth += healthToAdd;
                            Debug.Log($"체력 아이템 사용! 현재 체력 :{thirdPersonController.CurrentHealth}");
                            return true;
                        }
                        break;
                    }


                    case ItemType.Arrow:
                    {
                        Debug.Log("화살 사용됨!");
                        break;
                    }
                }



                OnDataChanged?.Invoke();
                return true;
            }
        }
        return false;
    }
}

 