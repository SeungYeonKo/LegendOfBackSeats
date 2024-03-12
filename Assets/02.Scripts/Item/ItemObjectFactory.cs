using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ItemObjectFactory : MonoBehaviour
{
    public static ItemObjectFactory Instance { get; private set; }

    // 아이템 프리팹  
    public List<GameObject> ItemPrefabs;

    // 아이템 풀링
    private List<ItemObject> _itemPool;
    public int PoolSize = 10;

    private void Awake()
    {
        Instance = this;
        _itemPool = new List<ItemObject>(); 

        for (int i = 0; i < PoolSize; i++)
        {
            foreach (GameObject prefab in ItemPrefabs)
            {
                GameObject item = Instantiate(prefab);
                item.transform.SetParent(this.transform);       //Hierachy창에서 깔끔히 정리
                _itemPool.Add(item.GetComponent<ItemObject>());
                item.SetActive(false);
            }
        }
    }

    // 확률 생성
    public void MakePercent(Vector3 position)
    {
        int percentage = UnityEngine.Random.Range(0, 100);
        if (percentage <= 50)
        {
            Make(ItemType.Health, position);
        }
        else
        {
            Make(ItemType.Arrow, position);
        }
    }

    private ItemObject Get(ItemType itemType)       // 창고 뒤지기
    {
        foreach (ItemObject itemObject in _itemPool)         // 창고를 뒤진다
        {
            if (!itemObject.gameObject.activeSelf && itemObject.ItemType == itemType)
            {
                return itemObject;
            }
        }
        return null;
    }

    // 아이템 생성
    public void Make(ItemType itemType, Vector3 position)
    {
        ItemObject itemObject = Get(itemType);

        if (itemObject != null)    //아이템 오브젝트가 존재하는지 확인
        {
            itemObject.transform.position = position;
            itemObject.Init();
            itemObject.gameObject.SetActive(true);
        }
    }
}
