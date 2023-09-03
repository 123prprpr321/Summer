using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragPanel : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    RectTransform rectTransform;
    Canvas canvas;

    float x, y;
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        canvas = InventoryManager.Instance.GetComponent<Canvas>();//注意两个单例，所以Bag必须初始为setactive false
    }
    void Start()
    {
        x = rectTransform.anchoredPosition.x;
        y = rectTransform.anchoredPosition.y;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;//改变中心点位置，delta是鼠标指针位移
        //除上scaleFactor的原因是直接改变会导致Panel偏移
    }

    public void OnPointerDown(PointerEventData eventData)//改变层级，虽然本demo只有一个可拖动的(  ，不过注意更改的话不能挡住DragCanvas
    {
        rectTransform.SetSiblingIndex(2);
    }
    public void Reset()
    {
        rectTransform.anchoredPosition = new Vector2(x, y);
    }
}
