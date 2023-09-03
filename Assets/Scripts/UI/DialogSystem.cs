using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogSystem : MonoBehaviour
{
    [Header("UI���")]
    public Text textLabel;//�ı���

    [Header("�ı��ļ�")]
    public TextAsset textFile;
    public int index;//���


    bool textFinished;
    bool cancelTyping;
    public List<string> textList = new List<string>();
    void Awake()
    {
        GetTextFromFile(textFile);
    }

    private void OnEnable()
    {
        textLabel.text = textList[index++];
        textFinished = true;
        StartCoroutine(SetTextUI());
    }
    void Update()
    {
        enterDialog();
    }
    void GetTextFromFile(TextAsset file)
    {
        textList.Clear();
        index = 0;
        var lineDate = file.text.Split('\n'); //�����и�  ��������

        foreach (var line in lineDate)
        {
            textList.Add(line);
        }
    }
    void enterDialog()
    {
        if (Input.GetKeyDown(KeyCode.R) && textFinished == true)
        {
            if (index == textList.Count)
            {
                index = 0;
                gameObject.SetActive(false);
                return;
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (textFinished && cancelTyping == false)
            {
                StartCoroutine(SetTextUI());
            }
            else if (textFinished == false && cancelTyping == false)
            {
                cancelTyping = true;
            }

        }
    }
    IEnumerator SetTextUI()
    {
        textFinished = false;
        textLabel.text = null;

        switch (textList[index])
        {
            case "A":
                index++;
                break;
            case "B":
                index++;
                break;
        }
        int letter = 0;
        while (!cancelTyping && letter < textList[index].Length)
        {
            textLabel.text += textList[index][letter];
            letter++;
            yield return new WaitForSeconds(0.12f);
        }

        textLabel.text = textList[index];
        textFinished = true;
        cancelTyping = false;
        index++;
    }
}
