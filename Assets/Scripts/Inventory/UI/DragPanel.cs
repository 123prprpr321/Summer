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

        canvas = InventoryManager.Instance.GetComponent<Canvas>();//ע����������������Bag�����ʼΪsetactive false
    }
    void Start()
    {
        x = rectTransform.anchoredPosition.x;
        y = rectTransform.anchoredPosition.y;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;//�ı����ĵ�λ�ã�delta�����ָ��λ��
        //����scaleFactor��ԭ����ֱ�Ӹı�ᵼ��Panelƫ��
    }

    public void OnPointerDown(PointerEventData eventData)//�ı�㼶����Ȼ��demoֻ��һ�����϶���(  ������ע����ĵĻ����ܵ�סDragCanvas
    {
        rectTransform.SetSiblingIndex(2);
    }
    public void Reset()
    {
        rectTransform.anchoredPosition = new Vector2(x, y);
    }
}
