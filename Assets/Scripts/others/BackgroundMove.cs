using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BackgroundMove : MonoBehaviour
{
    public float speed;
    private RectTransform bgRectTransform;
    private float imageWidth;

    private void Start()
    {
        bgRectTransform = GetComponent<RectTransform>();
        imageWidth = bgRectTransform.rect.width;
    }

    private void Update()
    {
        bgRectTransform.localPosition += new Vector3(-speed * Time.deltaTime, 0, 0);

        if (bgRectTransform.localPosition.x <= -imageWidth / 2f)
        {
            bgRectTransform.localPosition += new Vector3(imageWidth, 0, 0);
        }
    }
}
