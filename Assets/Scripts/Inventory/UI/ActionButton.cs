using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButton : MonoBehaviour
{
    public KeyCode actionKey;//���ÿ����
    private SlotHolder currentSlotHolder;//��ǰ��ק��

    void Awake()
    {
        currentSlotHolder = GetComponent<SlotHolder>();

    }

    void Update()
    {
        if (Input.GetKeyDown(actionKey) && currentSlotHolder.itemUI.GetItem())//ȷ��������
            currentSlotHolder.UseItem();
    }
}
