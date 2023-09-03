using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemUI : MonoBehaviour
{
    public Image icon = null;
    public Text amount = null;

    public itemData_SO currentItemData;
    public InventoryData_SO Bag { get; set; }
    public int Index { get; set; } = -1;

    public void SetupItemUI(itemData_SO item, int itemAmount)
    {
        if (itemAmount == 0)
        {
            Bag.items[Index].itemData = null;
            icon.gameObject.SetActive(false);
            return;
        }

        if(itemAmount < 0)
        {
            item = null;
        }
        if(item != null)
        {
            currentItemData = item;
            icon.sprite = item.itemImage;
            amount.text = itemAmount.ToString();
            icon.gameObject.SetActive(true);
        }
        else
        {
            icon.gameObject.SetActive(false);
        }
    }
    public itemData_SO GetItem()
    {
        return Bag.items[Index].itemData;
    }
}
