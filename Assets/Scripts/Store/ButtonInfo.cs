using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonInfo : MonoBehaviour
{
    public int ItemID;
    public Text PirceTxt;
    public Text QuantityTxt;
    public GameObject ShopManager;
    void Update()
    {
        PirceTxt.text = "¼Û¸ñ£º" + ShopManager.GetComponent<storeManager>().shopItems[2, ItemID].ToString();
        QuantityTxt.text = ShopManager.GetComponent<storeManager>().shopItems[3, ItemID].ToString();
    }
}
