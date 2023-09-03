using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButton : MonoBehaviour
{
    public KeyCode actionKey;//设置快捷栏
    private SlotHolder currentSlotHolder;//当前拖拽物

    void Awake()
    {
        currentSlotHolder = GetComponent<SlotHolder>();

    }

    void Update()
    {
        if (Input.GetKeyDown(actionKey) && currentSlotHolder.itemUI.GetItem())//确保有物体
            currentSlotHolder.UseItem();
    }
}
