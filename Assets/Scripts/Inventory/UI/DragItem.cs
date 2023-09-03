using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ItemUI))]
public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    ItemUI currentItemUI;
    SlotHolder currentHolder;
    SlotHolder targetHolder;



    void Awake()
    {
        currentItemUI = GetComponent<ItemUI>();
        currentHolder = GetComponentInParent<SlotHolder>();
    }
    public void OnBeginDrag(PointerEventData eventData) //��¼ԭʼ����
    {
        InventoryManager.Instance.currentDrag = new InventoryManager.DragData();//��ʼ��ק
        InventoryManager.Instance.currentDrag.originalHolder = GetComponentInParent<SlotHolder>();//����ԭ������
        InventoryManager.Instance.currentDrag.originalParent = (RectTransform)transform.parent;
        transform.SetParent(InventoryManager.Instance.dragCanvas.transform, true);
    }

    public void OnDrag(PointerEventData eventData)//�������
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)//������Ʒ������
    {
        
        //EventSystem�������ж��Ƿ�ָ��UI
        if (EventSystem.current.IsPointerOverGameObject())
        {
            
            if (InventoryManager.Instance.CheckInActionUI(eventData.position) || InventoryManager.Instance.CheckInEquipmentUI(eventData.position)
                || InventoryManager.Instance.CheckInInventoryUI(eventData.position))
            {
                //��ʼ��ק����Ʒ��ItemSlot�ᱻ����Drag Canvas�� 

                if (eventData.pointerEnter.gameObject.GetComponent<SlotHolder>())//������ڸ���������룬������Ż�
                {
                    targetHolder = eventData.pointerEnter.gameObject.GetComponent<SlotHolder>();
                }
                else
                {
                    targetHolder = eventData.pointerEnter.gameObject.GetComponentInParent<SlotHolder>();
                }
                if (targetHolder != InventoryManager.Instance.currentDrag.originalHolder)//�ж��Ƿ��ϵ�ԭ�أ���ֹbug
                //// ������������Ʒ������ע�����ͣ����߲��ܷ���������
                switch (targetHolder.slotType)
                {
                    case SlotType.BAG:  //�����ж��ܽ�����������Ʒ����һ��
                        SwapItem();
                        break;
                    case SlotType.WEAPON:
                        if (currentItemUI.Bag.items[currentItemUI.Index].itemData.itemType == ItemType.Weapon)
                            SwapItem();
                        break;
                    case SlotType.ARMOR:
                        if (currentItemUI.Bag.items[currentItemUI.Index].itemData.itemType == ItemType.Armor)
                            SwapItem();
                        break;
                    case SlotType.ACTION:
                        if (currentItemUI.Bag.items[currentItemUI.Index].itemData.itemType == ItemType.Useable)
                            SwapItem();
                        break;
                }
                currentHolder.UpdataItem();
                targetHolder.UpdataItem();
            }
        }
        //��ק��ɣ���Ҫ�Ļ�canvas
        transform.SetParent(InventoryManager.Instance.currentDrag.originalParent);

        RectTransform t = transform as RectTransform;

        t.offsetMax = -Vector2.one * 0;
        t.offsetMin = -Vector2.one * 0;
    }


    public void SwapItem()
    {
        //�ı��б�ˢ�£��Ȼ�ȡ��ţ�ֻҪˢ�º�ͳɹ��ı�
        var targetItem = targetHolder.itemUI.Bag.items[targetHolder.itemUI.Index];  //Ŀ�������Ʒ
        var tempItem = currentHolder.itemUI.Bag.items[currentHolder.itemUI.Index];  //��ǰ��ק��Ʒ


        bool isSameItem = (tempItem.itemData == targetItem.itemData);     //��Ҫ�ж��Ƿ���ͬ����Ϊ���item���Զѵ�����ô���Ժϲ�

        if (isSameItem && targetItem.itemData.stackBool)
        {
            targetItem.amount += tempItem.amount;
            tempItem.itemData = null;
            tempItem.amount = 0;
        }
        else
        {
            currentHolder.itemUI.Bag.items[currentHolder.itemUI.Index] = targetItem;
            targetHolder.itemUI.Bag.items[targetHolder.itemUI.Index] = tempItem;
        }
    }
    bool InteractWithUI()   //�ж��Ƿ�����UI
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
        else
            return false;
    }
}
