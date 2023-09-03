using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GetCamera : MonoBehaviour
{
    private Camera mainCamera;

    private void OnEnable()
    {
        mainCamera = Camera.main;
        Canvas canvas = GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.worldCamera = mainCamera;
        }
    }
}
