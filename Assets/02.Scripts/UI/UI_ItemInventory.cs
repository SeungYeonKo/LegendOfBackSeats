using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ItemInventory : MonoBehaviour
{
    public TextMeshProUGUI HealthItemCountText;
    public TextMeshProUGUI ArrowItemCountText;

    private void OnEnable()
    {
        // OnDataChanged 이벤트에 Refresh 메소드 구독
        ItemManager.Instance.OnDataChanged.AddListener(Refresh);
    }

    private void OnDisable()
    {
        // 오브젝트가 비활성화될 때 이벤트 구독 해제, 메모리 누수 방지
        ItemManager.Instance.OnDataChanged.RemoveListener(Refresh);
    }

    public void Refresh()
    {
        HealthItemCountText.text = $"x{ItemManager.Instance.GetItemCount(ItemType.Health)}";
        ArrowItemCountText.text = $"x{ItemManager.Instance.GetItemCount(ItemType.Arrow)}";
    }
}