using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemToolTip : MonoBehaviour
{
    public Text itemNameText;    //使用TextMeshPro，因为Text太糊了
    public Text itemInfoText;    //还是使用Text吧，TextMeshPro中文不方便

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
    private void OnEnable()//判断坐标
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
        rectTransform.GetWorldCorners(corners);//将UI界面的锚点值赋值给这个数组，左下左上，右上右下

        float width = corners[3].x - corners[0].x;
        float height = corners[1].y - corners[0].y;
        //判断边界，决定出现位置
        if (mousePos.y < height)  //乘0.6f的原因是如果信息栏挡住鼠标，就会闪烁，所以让信息栏偏移一段距离
            rectTransform.position = mousePos + Vector3.up * height * 0.6f;
        else if (Screen.width - mousePos.x > width)
            rectTransform.position = mousePos + Vector3.right * width * 0.6f;
        else
            rectTransform.position = mousePos + Vector3.left * width * 0.6f;
    }

}
