using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class storeManager : MonoBehaviour
{
    public int[,] shopItems = new int[5, 5];
    public float coins;
    public Text CoinsTXT;
    public GameObject Store;

    public itemData_SO Object1;
    public itemData_SO Object2;
    public itemData_SO Object3;
    public itemData_SO Object4;
    void Start()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
        GameObject.DontDestroyOnLoad(this.Store);
        CoinsTXT.text = "Coins:" + coins.ToString();
        //ID
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;
        shopItems[1, 4] = 4;
        //价格
        shopItems[2, 1] = 20;
        shopItems[2, 2] = 30;
        shopItems[2, 3] = 50;
        shopItems[2, 4] = 200;
        //曾购买数量
        shopItems[3, 1] = 0;
        shopItems[3, 2] = 0;
        shopItems[3, 3] = 0;
        shopItems[3, 4] = 0;

    }
    void Update()
    {
        SetStore();
    }
    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        
        if (coins >= shopItems[2,ButtonRef.GetComponent<ButtonInfo>().ItemID])
        {
            coins -= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID];
            shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID]++;
            CoinsTXT.text = "Coins:" + coins.ToString();
            //将购买物品添加到背包中

            Debug.Log("购买成功");
            if (shopItems[1, ButtonRef.GetComponent<ButtonInfo>().ItemID] == 1)
            {
                InventoryManager.Instance.inventoryData.AddItem(Object1, 1);
            }
            if (shopItems[1, ButtonRef.GetComponent<ButtonInfo>().ItemID] == 2)
            {
                InventoryManager.Instance.inventoryData.AddItem(Object2, 1);
            }
            if (shopItems[1, ButtonRef.GetComponent<ButtonInfo>().ItemID] == 3)
            {
                InventoryManager.Instance.inventoryData.AddItem(Object3, 1);
            }
            if (shopItems[1, ButtonRef.GetComponent<ButtonInfo>().ItemID] == 4)
            {
                InventoryManager.Instance.inventoryData.AddItem(Object4, 1);
            }           
            //更新背包UI
            InventoryManager.Instance.inventoryUI.RefreshUI();
            InventoryManager.Instance.actionUI.RefreshUI();
            ButtonRef.GetComponent<ButtonInfo>().QuantityTxt.text = shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID].ToString();
        }
        else
        {
            StartCoroutine(ChangeCoinTxtColor());
        }
    }
    void SetStore()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Store != null)
            {
                Store.SetActive(!Store.activeSelf);
            }
        }
    }
    private IEnumerator ChangeCoinTxtColor()
    {
        CoinsTXT.color = Color.red;
        yield return new WaitForSeconds(0.3f);

        CoinsTXT.color = new Color(1f, 1f, 1f, 1f);
    }
}
