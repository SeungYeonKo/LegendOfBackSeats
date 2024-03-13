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


        ThirdPersonController thirdPersonController = GameObject.FindWithTag("Player").GetComponent<ThirdPersonController>();

        if (ItemType == ItemType.Health && thirdPersonController.CurrentHealth < thirdPersonController.MaxHealth)
        {
            // Health아이템 사용시 플레이어 체력 +5, 최대 체력을 초과하지 않게 함
            int healthToAdd = Mathf.Min(5, thirdPersonController.MaxHealth - thirdPersonController.CurrentHealth);
            thirdPersonController.CurrentHealth += healthToAdd;
            Debug.Log($"체력 +{healthToAdd}!");

            Count -= 1; 
            return true;
        }

        return false; 
    }
}


