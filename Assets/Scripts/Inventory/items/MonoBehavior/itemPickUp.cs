using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemPickUp : MonoBehaviour
{
    public itemData_SO itemData;    //获取itemData_SO中的信息
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            //武器放入背包
            InventoryManager.Instance.inventoryData.AddItem(itemData, itemData.itemAmount);
            InventoryManager.Instance.inventoryUI.RefreshUI();
            //检测任务进展
            QuestManager.Instance.UpdateQuestProgress(itemData.itemName, itemData.itemAmount);
            //删除物品
            ObjectPool.Instance.PushObject(gameObject);//多对象池统一管理，如果原本在场景中放置过预制体，并且预制体够用，那么就不会生成新的父类
            //Destroy(gameObject);
        }
    }
}
