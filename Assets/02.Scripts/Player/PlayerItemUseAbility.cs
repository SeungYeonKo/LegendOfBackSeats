using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemUseAbility : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // 체력 아이템 사용
            bool result = ItemManager.Instance.TryUseItem(ItemType.Health);
            Debug.Log("체력 아이템을 사용했습니다");
        }
    }
}
