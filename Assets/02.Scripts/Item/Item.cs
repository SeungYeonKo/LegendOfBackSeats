using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public enum ItemType
{
    Health,
    Arrow,
    Key
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

 /*   public bool TryUse()
    {
        if (Count == 0)
        {
            return false;
        }

        return true;

        Count -= 1;

        // Health Item
        ThirdPersonController thirdPersonController = GameObject.FindWithTag("Player").GetComponent<ThirdPersonController>();
        if (ItemType == ItemType.Health && thirdPersonController.CurrentHealth < thirdPersonController.MaxHealth)
        {
            // Health아이템 사용시 플레이어 체력 +5, 최대 체력을 초과하지 않게 함
            int healthToAdd = Mathf.Min(5, thirdPersonController.MaxHealth - thirdPersonController.CurrentHealth);
            thirdPersonController.CurrentHealth += healthToAdd;
            Debug.Log($"체력 아이템 사용! 현재 체력 :{thirdPersonController.CurrentHealth}");
            return true;
        }
        // Arrow
        if (ItemType == ItemType.Arrow)
        {
            Debug.Log("화살 사용됨!");
            return true;
        }
        return false; 
    }*/
}


