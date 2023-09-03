using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    public class DragData
    {
        public SlotHolder originalHolder;
        public RectTransform originalParent;
    }

    [Header("Inventory Data")]
    public InventoryData_SO inventoryTemplate;
    public InventoryData_SO inventoryData;
    public InventoryData_SO actionTemplate;
    public InventoryData_SO actionData;
    public InventoryData_SO equipmentTemplate;
    public InventoryData_SO equipmentaData;

    [Header("ContainerS")]
    public ContainerUI inventoryUI;
    public ContainerUI actionUI;
    public ContainerUI equipmentUI;

    [Header("Drag Canvas")]
    public Canvas dragCanvas;
    public DragData currentDrag;//临时保存刚开始拖拽时的数据

    [Header("UI Panel")]
    public GameObject bagPanel;
    bool isOpen = false;

    [Header("Tooltip")]
    public ItemToolTip tooltip;

    protected override void Awake()
    {
        base.Awake();// 因为这个背包不会销毁，并且是保留数据，使用直接使用Instantiate
        if (inventoryTemplate != null)
            inventoryData = Instantiate(inventoryTemplate);
        if (actionTemplate != null)
            actionData = Instantiate(actionTemplate);
        if (equipmentTemplate != null)
            equipmentaData = Instantiate(equipmentTemplate);

        DontDestroyOnLoad(this);
    }
    void Start()
    {
        //LoadData();
        inventoryUI.RefreshUI();
        actionUI.RefreshUI();
        equipmentUI.RefreshUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            isOpen = !isOpen;
            bagPanel.SetActive(isOpen);
        }
    }

    //保存和加载
    //public void SaveData()
    //{
    //    SaveManager.Instance.Save(inventoryData, inventoryData.name);
    //    SaveManager.Instance.Save(actionData, actionData.name);
    //    SaveManager.Instance.Save(equipmentData, equipmentData.name);
    //}

    //public void LoadData() //未调用，需要再切换场景时调用
    //{
    //    SaveManager.Instance.Load(inventoryData, inventoryData.name);
    //    SaveManager.Instance.Load(actionData, actionData.name);
    //    SaveManager.Instance.Load(equipmentData, equipmentData.name);
    //}

    #region 检查拖拽物品是否在每一个Slot 范围内
    public bool CheckInInventoryUI(Vector3 position)  //传入鼠标坐标
    {
        for (int i = 0; i < inventoryUI.slotHolders.Length; i++)
        {
            RectTransform t = inventoryUI.slotHolders[i].transform as RectTransform;    //这种转换方式与括号强制转换一样
            if (RectTransformUtility.RectangleContainsScreenPoint(t, position))  //RectangleContainsScreenPoint表示判断屏幕的某一点是否在屏幕方格内，两个参数的含义分别是方格的区域范围，以及指针所指的点
            {                                                                    //判断是否在方格中
                return true;
            }
        }
        return false;
    }

    public bool CheckInActionUI(Vector3 position)  //与上面一样，判断ActionUI，equipment同理
    {
        for (int i = 0; i < actionUI.slotHolders.Length; i++)
        {
            RectTransform t = actionUI.slotHolders[i].transform as RectTransform;    //这种转换方式与括号强制转换一样
            if (RectTransformUtility.RectangleContainsScreenPoint(t, position))  //RectangleContainsScreenPoint表示判断屏幕的某一点是否在屏幕方格内，两个参数的含义分别是方格的区域范围，以及指针所指的点
            {                                                                    //判断是否在方格中
                return true;
            }
        }
        return false;
    }

    public bool CheckInEquipmentUI(Vector3 position)
    {
        for (int i = 0; i < equipmentUI.slotHolders.Length; i++)
        {
            RectTransform t = equipmentUI.slotHolders[i].transform as RectTransform;
            if (RectTransformUtility.RectangleContainsScreenPoint(t, position))//RectangleContainsScreenPoint表示判断屏幕的某一点是否在屏幕方格内，两个参数的含义分别是方格的区域范围，以及指针所指的点
            {
                return true;
            }
        }
        return false;
    }
    #endregion

    #region 检测任务物品

    public void CheckQuestItemBag(string questItemName)
    {
        foreach (var item in inventoryData.items)
        {
            if (item.itemData != null)
            {
                if (item.itemData.itemName == questItemName)
                {
                    QuestManager.Instance.UpdateQuestProgress(item.itemData.itemName, item.amount);
                }
            }
        }

        foreach (var item in actionData.items)
        {
            if (item.itemData != null)
            {
                if (item.itemData.itemName == questItemName)
                {
                    QuestManager.Instance.UpdateQuestProgress(item.itemData.itemName, item.amount);
                }
            }
        }
    }
    #endregion

    //检测背包和快捷栏物品
    public InventoryItem QuestItemInBag(itemData_SO questItem)
    {
        return inventoryData.items.Find(i => i.itemData == questItem);
    }

    public InventoryItem QuestItemInAction(itemData_SO questItem)
    {
        return actionData.items.Find(i => i.itemData == questItem);
    }
}
