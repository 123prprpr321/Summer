using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public enum SlotType { BAG, WEAPON, ARMOR, ACTION }
public class SlotHolder : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public SlotType slotType;
    public ItemUI itemUI;
    public API api;

    public GameObject Object;
    public Image slotHightlight;
    public bool isSelected;
    public PlayController player;


    public static bool open = false;
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        api = GameObject.FindGameObjectWithTag("Player").GetComponent<API>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayController>();
        GameObject bow = GameObject.FindWithTag("Bow");
        if (bow != null)
        {
           Object = bow;
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        api = GameObject.FindGameObjectWithTag("Player").GetComponent<API>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayController>();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void OnPointerClick(PointerEventData eventData)//判断点击次数
    {
        if (eventData.clickCount % 2 == 0)
        {
            UseItem();
        }

    }
    public void UseItem()
    {
        if (itemUI.GetItem() != null)
        {
            if (itemUI.GetItem().itemType == ItemType.Useable && itemUI.Bag.items[itemUI.Index].amount > 0)
            {
                if (api != null)
                {
                    api.ApplyHealth(itemUI.GetItem().itemData.healthPoint);
                }
                if (player != null && itemUI.Bag.items[itemUI.Index].itemData.itemName == "敏捷药水")
                {
                    StartCoroutine(敏捷(25f, 1.2f));
                    //StartCoroutine(敏捷(40f, itemUI.GetItem().itemData.speedPoint));
                }
                itemUI.Bag.items[itemUI.Index].amount -= 1;
                QuestManager.Instance.UpdateQuestProgress(itemUI.GetItem().itemName, -1);
            }
           
        }
        UpdataItem();
    }
    private IEnumerator 敏捷(float during, float amount)
    {
        Debug.Log(player.speed);
        float originalSpeed = player.speed;
        player.speed *= amount;
        yield return new WaitForSeconds(during);
        player.speed = originalSpeed;
        Debug.Log(player.speed);
    }
    public void UpdataItem()
    {
        switch (slotType)
        {
            case SlotType.BAG:
                itemUI.Bag = InventoryManager.Instance.inventoryData;
                break;
            case SlotType.WEAPON:
                itemUI.Bag = InventoryManager.Instance.equipmentaData;
                //接下来判断tag，改变攻击方式
                if (itemUI.Bag.items[itemUI.Index].itemData != null)
                {
                    //改变方式
                    if (itemUI.Bag.items[itemUI.Index].itemData.itemName == "暗影焰弓")
                    {
                        if (Object != null)
                        Object.SetActive(true);
                        open = true;
                    }
                    else
                    {
                        if (Object != null)
                        Object.SetActive(false);
                        open = false;
                    }
                }
                break;
            case SlotType.ARMOR:
                itemUI.Bag = InventoryManager.Instance.equipmentaData;
                if (itemUI.Bag.items[itemUI.Index].itemData != null)
                {
                    //改变防御
                }
                break;
            case SlotType.ACTION:
                itemUI.Bag = InventoryManager.Instance.actionData;
                break;

        }
        var item = itemUI.Bag.items[itemUI.Index];
        itemUI.SetupItemUI(item.itemData, item.amount);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemUI.GetItem())
        {
            InventoryManager.Instance.tooltip.SetupTooltip(itemUI.GetItem());
            InventoryManager.Instance.tooltip.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (itemUI.GetItem())
        {
            InventoryManager.Instance.tooltip.gameObject.SetActive(false);
        }
    }
    void OnDisable()
    {
        InventoryManager.Instance.tooltip.gameObject.SetActive(false);
    }
}
