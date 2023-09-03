using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Useable, Weapon, Armor }

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item Data")]

public class itemData_SO : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemImage;
    public int itemAmount;
    public bool stackBool;      //ÊÇ·ñ¿É¶Ñµþ
    [TextArea]
    public string description = "";

    [Header("Useable Item")]
    public UseableItemData_SO itemData;

    [Header("Weapon")]
    public GameObject weaponPrefab;
}
