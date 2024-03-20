using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ItemManager : MonoBehaviour
{
    public UnityEvent OnDataChanged;

    public static ItemManager Instance { get; private set; }

    // 체력 아이템 없을 때 띄우는 텍스트
    public TextMeshProUGUI NoHealthItemTextUI;
    // 최대 체력인데 체력 아이템 먹을 때 띄우는 텍스트
    public TextMeshProUGUI MaxHealthTextUI;

    // 체력 아이템 사운드
    public AudioSource HealthItemSound;

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
        NoHealthItemTextUI.text = string.Empty;
        MaxHealthTextUI.text = string.Empty;

        ItemList.Add(new Item(ItemType.Health, 0));
        ItemList.Add(new Item(ItemType.Arrow, 5));
        ItemList.Add(new Item(ItemType.Key, 0));

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
            if (ItemList[i].ItemType == itemType)
            {
                // 아이템이 있고, 최대 체력이 아닐 때만 아이템 사용 가능
                if (ItemList[i].Count > 0)
                {

                    if (itemType == ItemType.Health)
                    {
                        ThirdPersonController thirdPersonController = GameObject.FindWithTag("Player").GetComponent<ThirdPersonController>();
                        if (thirdPersonController.CurrentHealth < thirdPersonController.MaxHealth)
                        {
                            // 최대 체력이 아니면 아이템 사용
                            int healthToAdd = Mathf.Min(5, thirdPersonController.MaxHealth - thirdPersonController.CurrentHealth);
                            thirdPersonController.CurrentHealth += healthToAdd;
                            Debug.Log($"체력 아이템 사용! 현재 체력: {thirdPersonController.CurrentHealth}");

                            // 사운드 재생
                            HealthItemSound.Play();

                            ItemList[i].Count -= 1; // 아이템 개수 감소
                            OnDataChanged?.Invoke();
                            return true;
                        }
                        else
                        {
                            // 이미 최대 체력이면 아이템 사용하지 않고 text 띄우기
                            StartCoroutine(ShowMaxHealthMessage());
                            return false;
                        }
                    }
                    else if (itemType == ItemType.Arrow)
                    {
                        ItemList[i].Count -= 1; // 아이템 개수 감소
                        OnDataChanged?.Invoke();
                        return true;
                    }
                }
                else
                {
                    // 체력 아이템이 없는데 아이템을 먹으면 text 띄우기
                    StartCoroutine(ShowNoHealthItemMessage());
                    
                    // 아이템이 없을 때의 처리
                    Debug.Log($"{itemType} 아이템이 없어 사용할 수 없습니다.");
                    return false; // 아이템이 없으므로 false 반환
                }
            }
        }
        return false; // 해당하는 아이템 타입을 찾지 못했을 때
    }

    IEnumerator ShowNoHealthItemMessage()
    {
        NoHealthItemTextUI.gameObject.SetActive(true);
        NoHealthItemTextUI.text = "체력 아이템이 없습니다 !";
        yield return new WaitForSeconds(2f);
        NoHealthItemTextUI.gameObject.SetActive(false);
    }

    IEnumerator ShowMaxHealthMessage()
    {
        MaxHealthTextUI.gameObject.SetActive(true);
        MaxHealthTextUI.text = "이미 최대 체력입니다.";
        yield return new WaitForSeconds(2f);
        MaxHealthTextUI.gameObject.SetActive(false) ;
    }
}

 