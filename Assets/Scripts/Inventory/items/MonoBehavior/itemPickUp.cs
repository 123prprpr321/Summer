using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemPickUp : MonoBehaviour
{
    public itemData_SO itemData;    //��ȡitemData_SO�е���Ϣ
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            //�������뱳��
            InventoryManager.Instance.inventoryData.AddItem(itemData, itemData.itemAmount);
            InventoryManager.Instance.inventoryUI.RefreshUI();
            //��������չ
            QuestManager.Instance.UpdateQuestProgress(itemData.itemName, itemData.itemAmount);
            //ɾ����Ʒ
            ObjectPool.Instance.PushObject(gameObject);//������ͳһ�������ԭ���ڳ����з��ù�Ԥ���壬����Ԥ���幻�ã���ô�Ͳ��������µĸ���
            //Destroy(gameObject);
        }
    }
}
