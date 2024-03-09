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
}
/*    public bool TryUse()
    {
        if (Count == 0)
        {
            return false;
        }
        Count -= 1;
         switch(ItemType)
         {
             case ItemType.Health:
             {
                // Health아이템 사용시 플레이어 체력 꽉차기
                 PlayerMoveAbility playerMoveAbility = GameObject.FindWithTag("Player").GetComponent<PlayerMoveAbility>();
                 playerMoveAbility.Health = playerMoveAbility.MaxHealth;
                 break;
             }
             case ItemType.Arrow:
             {
                 // Arrow아이템 사용시 Player가 들고있는 Arrow + 1
                 PlayerGunFireAbility ability = GameObject.FindWithTag("Player").GetComponent<PlayerGunFireAbility>();
                 ability.CurrentGun.BulletRemainCount = ability.CurrentGun.BulletMaxCount;
                 ability.RefreshUI();
                 break;
             }
         }
     return true;
        }
    }
*/

