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
    public DragData currentDrag;//��ʱ����տ�ʼ��קʱ������

    [Header("UI Panel")]
    public GameObject bagPanel;
    bool isOpen = false;

    [Header("Tooltip")]
    public ItemToolTip tooltip;

    protected override void Awake()
    {
        base.Awake();// ��Ϊ��������������٣������Ǳ������ݣ�ʹ��ֱ��ʹ��Instantiate
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

    //����ͼ���
    //public void SaveData()
    //{
    //    SaveManager.Instance.Save(inventoryData, inventoryData.name);
    //    SaveManager.Instance.Save(actionData, actionData.name);
    //    SaveManager.Instance.Save(equipmentData, equipmentData.name);
    //}

    //public void LoadData() //δ���ã���Ҫ���л�����ʱ����
    //{
    //    SaveManager.Instance.Load(inventoryData, inventoryData.name);
    //    SaveManager.Instance.Load(actionData, actionData.name);
    //    SaveManager.Instance.Load(equipmentData, equipmentData.name);
    //}

    #region �����ק��Ʒ�Ƿ���ÿһ��Slot ��Χ��
    public bool CheckInInventoryUI(Vector3 position)  //�����������
    {
        for (int i = 0; i < inventoryUI.slotHolders.Length; i++)
        {
            RectTransform t = inventoryUI.slotHolders[i].transform as RectTransform;    //����ת����ʽ������ǿ��ת��һ��
            if (RectTransformUtility.RectangleContainsScreenPoint(t, position))  //RectangleContainsScreenPoint��ʾ�ж���Ļ��ĳһ���Ƿ�����Ļ�����ڣ����������ĺ���ֱ��Ƿ��������Χ���Լ�ָ����ָ�ĵ�
            {                                                                    //�ж��Ƿ��ڷ�����
                return true;
            }
        }
        return false;
    }

    public bool CheckInActionUI(Vector3 position)  //������һ�����ж�ActionUI��equipmentͬ��
    {
        for (int i = 0; i < actionUI.slotHolders.Length; i++)
        {
            RectTransform t = actionUI.slotHolders[i].transform as RectTransform;    //����ת����ʽ������ǿ��ת��һ��
            if (RectTransformUtility.RectangleContainsScreenPoint(t, position))  //RectangleContainsScreenPoint��ʾ�ж���Ļ��ĳһ���Ƿ�����Ļ�����ڣ����������ĺ���ֱ��Ƿ��������Χ���Լ�ָ����ָ�ĵ�
            {                                                                    //�ж��Ƿ��ڷ�����
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
            if (RectTransformUtility.RectangleContainsScreenPoint(t, position))//RectangleContainsScreenPoint��ʾ�ж���Ļ��ĳһ���Ƿ�����Ļ�����ڣ����������ĺ���ֱ��Ƿ��������Χ���Լ�ָ����ָ�ĵ�
            {
                return true;
            }
        }
        return false;
    }
    #endregion

    #region ���������Ʒ

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

    //��ⱳ���Ϳ������Ʒ
    public InventoryItem QuestItemInBag(itemData_SO questItem)
    {
        return inventoryData.items.Find(i => i.itemData == questItem);
    }

    public InventoryItem QuestItemInAction(itemData_SO questItem)
    {
        return actionData.items.Find(i => i.itemData == questItem);
    }
}
