using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ScreenFader : MonoBehaviour
{
    public float faderSpeed = 1.5f;
    Image image;
    bool sceneStart = true;
    bool sceneEnd = false;
    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if(sceneStart)
        {
            StartScene();
        }
    }
    void StartScene()
    {
        FaderToClear();
        if(image.color.a < 0.05f)
        {
            sceneStart = false;
        }
    }
    void FaderToClear()
    {
        image.color = Color.Lerp(image.color, Color.clear, faderSpeed * Time.deltaTime);
    }
}
