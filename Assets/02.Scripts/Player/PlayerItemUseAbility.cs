using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemUseAbility : MonoBehaviour
{
    void Update()
    {
        /*        if (GameManager.Instance.state != GameState.Start)
                {
                    return;
                }*/
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // 체력 아이템 사용
            bool result = ItemManager.Instance.TryUseItem(ItemType.Health);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
                // 화살 아이템 사용
                ItemManager.Instance.TryUseItem(ItemType.Arrow);
        }
    }
}
