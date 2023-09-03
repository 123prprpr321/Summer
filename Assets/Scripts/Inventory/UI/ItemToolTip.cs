using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemToolTip : MonoBehaviour
{
    public Text itemNameText;    //ʹ��TextMeshPro����ΪText̫����
    public Text itemInfoText;    //����ʹ��Text�ɣ�TextMeshPro���Ĳ�����

    RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetupTooltip(itemData_SO item)
    {
        itemNameText.text = item.itemName;
        itemInfoText.text = item.description;
    }
    private void OnEnable()//�ж�����
    {
        UpdatePosition();
    }
    void Update()
    {
        UpdatePosition();
    }

    void UpdatePosition()
    {
        Vector3 mousePos = Input.mousePosition;

        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);//��UI�����ê��ֵ��ֵ��������飬�������ϣ���������

        float width = corners[3].x - corners[0].x;
        float height = corners[1].y - corners[0].y;
        //�жϱ߽磬��������λ��
        if (mousePos.y < height)  //��0.6f��ԭ���������Ϣ����ס��꣬�ͻ���˸����������Ϣ��ƫ��һ�ξ���
            rectTransform.position = mousePos + Vector3.up * height * 0.6f;
        else if (Screen.width - mousePos.x > width)
            rectTransform.position = mousePos + Vector3.right * width * 0.6f;
        else
            rectTransform.position = mousePos + Vector3.left * width * 0.6f;
    }

}
