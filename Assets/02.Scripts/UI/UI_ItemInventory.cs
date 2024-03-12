using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ItemInventory : MonoBehaviour
{
    public TextMeshProUGUI HealthItemCountText;
    public TextMeshProUGUI ArrowItemCountText;


    public void Refresh()
    {
        HealthItemCountText.text = $"x{ItemManager.Instance.GetItemCount(ItemType.Health)}";
        ArrowItemCountText.text = $"x{ItemManager.Instance.GetItemCount(ItemType.Arrow)}";
    }
}
