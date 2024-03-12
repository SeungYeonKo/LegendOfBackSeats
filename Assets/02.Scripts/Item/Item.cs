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

        switch (ItemType)
        {
            case ItemType.Health:
            {
                // Health아이템 사용시 플레이어 체력 꽉차기
                ThirdPersonController ThirdPersonController = GameObject.FindWithTag("Player").GetComponent<ThirdPersonController>();
                ThirdPersonController.CurrentHealth += 5;
                Debug.Log("체력 +5!");
                break;
            }
            case ItemType.Arrow:
            {
                // Arrow아이템 사용시 Player가 들고있는 Arrow + 2
                PlayerArrowFireAbility ability = GameObject.FindWithTag("Player").GetComponent<PlayerArrowFireAbility>();
                ability.ArrowCurrentCount += 2;
                Debug.Log("화살 +2");
                break;
            }
        }
        return true;
    }
}


