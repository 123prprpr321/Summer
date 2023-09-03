using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Useable Item", menuName = "Inventory/Useable Item Data")]
public class UseableItemData_SO : ScriptableObject
{
   //任何UseableItem可以改变的属性都可以写在这里
    public int healthPoint;
    public float speedPoint;
}
