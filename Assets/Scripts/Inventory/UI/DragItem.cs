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
    public void OnBeginDrag(PointerEventData eventData) //记录原始数据
    {
        InventoryManager.Instance.currentDrag = new InventoryManager.DragData();//开始拖拽
        InventoryManager.Instance.currentDrag.originalHolder = GetComponentInParent<SlotHolder>();//保存原有数据
        InventoryManager.Instance.currentDrag.originalParent = (RectTransform)transform.parent;
        transform.SetParent(InventoryManager.Instance.dragCanvas.transform, true);
    }

    public void OnDrag(PointerEventData eventData)//跟随鼠标
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)//放下物品，交换
    {
        
        //EventSystem函数，判断是否指向UI
        if (EventSystem.current.IsPointerOverGameObject())
        {
            
            if (InventoryManager.Instance.CheckInActionUI(eventData.position) || InventoryManager.Instance.CheckInEquipmentUI(eventData.position)
                || InventoryManager.Instance.CheckInInventoryUI(eventData.position))
            {
                //开始拖拽后物品的ItemSlot会被存入Drag Canvas中 

                if (eventData.pointerEnter.gameObject.GetComponent<SlotHolder>())//如果放在格子中则放入，不在则放回
                {
                    targetHolder = eventData.pointerEnter.gameObject.GetComponent<SlotHolder>();
                }
                else
                {
                    targetHolder = eventData.pointerEnter.gameObject.GetComponentInParent<SlotHolder>();
                }
                if (targetHolder != InventoryManager.Instance.currentDrag.originalHolder)//判断是否拖到原地，防止bug
                //// 接下来交换物品，但是注意类型，道具不能放在武器栏
                switch (targetHolder.slotType)
                {
                    case SlotType.BAG:  //背包中都能交换，但是物品栏不一样
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
        //拖拽完成，需要改回canvas
        transform.SetParent(InventoryManager.Instance.currentDrag.originalParent);

        RectTransform t = transform as RectTransform;

        t.offsetMax = -Vector2.one * 0;
        t.offsetMin = -Vector2.one * 0;
    }


    public void SwapItem()
    {
        //改变列表，刷新，先获取序号，只要刷新后就成功改变
        var targetItem = targetHolder.itemUI.Bag.items[targetHolder.itemUI.Index];  //目标格子物品
        var tempItem = currentHolder.itemUI.Bag.items[currentHolder.itemUI.Index];  //当前拖拽物品


        bool isSameItem = (tempItem.itemData == targetItem.itemData);     //需要判断是否相同，因为如果item可以堆叠，那么可以合并

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
    bool InteractWithUI()   //判断是否触碰到UI
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
        else
            return false;
    }
}
