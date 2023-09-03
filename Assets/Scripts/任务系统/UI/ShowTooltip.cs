using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ShowTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler//∏˙ÀÊ Û±Í“∆∂Ø£¨œ‘ æTip
{
    private ItemUI currentItemUI;

    void Awake()
    {
        currentItemUI = GetComponent<ItemUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(123);
        QuestUI.Instance.tooltip.gameObject.SetActive(true);
        QuestUI.Instance.tooltip.SetupTooltip(currentItemUI.currentItemData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log(456);
        QuestUI.Instance.tooltip.gameObject.SetActive(false);
    }
}
