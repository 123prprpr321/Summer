using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class AsynchronousLoading : MonoBehaviour
{
    public GameObject loadScreen;
    public TMP_Text text;
    public Slider slider;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            EnterNextLevel();

        }
    }

    public void EnterNextLevel()
    {
        StartCoroutine(LoadLevel());
    }
    IEnumerator LoadLevel()
    {
        loadScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        operation.allowSceneActivation = false;//�ڰ��°���֮ǰ�����������һ����
        while (!operation.isDone)//���û�����
        {
            slider.value = operation.progress * 0.01f;
            text.text = operation.progress * 100 * 0.01f + "%";
            if (operation.progress >= 0.9f)
            {
                slider.value = 1;
                text.text = "Press AnyKey To Continue";
                if (Input.anyKeyDown)
                {
                    operation.allowSceneActivation = true;

                }
            }
            yield return null;//��ͣһ֡
        }
    }
}
