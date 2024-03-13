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
      /*  if (ItemType == ItemType.Arrow)
        {
            // Arrow 아이템 사용 로직 구현
            // 예: 화살 사용 로직, 화살 수 감소 등
            Debug.Log("화살 사용됨!");
            Count -= 1; // 화살 아이템을 사용했으므로 Count 감소
            return true;
        }*/
        return false; 
    }
}


