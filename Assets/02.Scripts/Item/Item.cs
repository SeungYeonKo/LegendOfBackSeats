using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public enum ItemType
{
    Health,
    Arrow
}

public class Item
{
    public ItemType ItemType;
    public int Count;

    public Item(ItemType itemType, int count)
    {
        ItemType = itemType;
        Count = count;
    }

    public bool TryUse()
    {
        if (Count == 0)
        {
            return false;
        }

        Count -= 1;

        if (ItemType == ItemType.Health)
        {
            
            // Health아이템 사용시 플레이어 체력 +5
            ThirdPersonController ThirdPersonController = GameObject.FindWithTag("Player").GetComponent<ThirdPersonController>();
            if(ThirdPersonController.CurrentHealth <= ThirdPersonController.MaxHealth)
            {
                ThirdPersonController.CurrentHealth += 5;
                Debug.Log("체력 +5!");
            }
        }
        return true;
    }
}


