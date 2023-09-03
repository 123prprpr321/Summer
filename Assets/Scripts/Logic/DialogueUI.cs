using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;//DOTween插件
public class DialogueUI : Singleton<DialogueUI>
{
    [Header("基础设置")]
    public Image icon;
    public Text mainText;
    public Button nextButton;
    public GameObject dialoguePanel;//面板信息，初始设置为false

    [Header("Data")]
    public DialogueData_SO currentData;
    public int currentIndex = 0;

    [Header("Options")]
    public RectTransform optionPanel;
    public OptionUI optionPrefab;
    protected override void Awake()
    {
        base.Awake();
        nextButton.onClick.AddListener(ContinueDialogue);//点击Button，加入事件，调用ContinueDialogue
    }


    private void Update()
    {

        if (nextButton.transform.GetChild(0).gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.R))
        {

            if (currentIndex < currentData.dialoguePieces.Count)
                UpdateMainDialogue(currentData.dialoguePieces[currentIndex]);
            else
            {
                dialoguePanel.SetActive(false);

            }
        }
    }


    void ContinueDialogue()
    {
        if (currentIndex < currentData.dialoguePieces.Count)
            UpdateMainDialogue(currentData.dialoguePieces[currentIndex]);
        else
            dialoguePanel.SetActive(false);
    }
    public void UpdateDialogueData(DialogueData_SO data)
    {
        currentData = data;
        currentIndex = 0;//保证重新播放
    }
    public void UpdateMainDialogue(DialoguePiece piece)
    {
        dialoguePanel.SetActive(true);
        //currentIndex++;
        if (piece.image != null)
        {
            icon.enabled = true;
            icon.sprite = piece.image;
        }
        else icon.enabled = false;

        mainText.text = "";
        //mainText.text = piece.text;

        mainText.DOText(piece.text, 0.3f);//这里由于用DOText，可能有一次性按多问题，所以时间少一点

        //next按钮在剩余对话为0时不显示，在有选项时也不显示
        if (piece.options.Count == 0 && currentData.dialoguePieces.Count > 0)
        {

            nextButton.interactable = true;
            nextButton.gameObject.SetActive(true);
            currentIndex++;
            nextButton.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            //nextButton.gameObject.SetActive(false);
            nextButton.interactable = false;
            nextButton.transform.GetChild(0).gameObject.SetActive(false);
        }
        //创建Options
        CreatOptions(piece);
    }
    void CreatOptions(DialoguePiece piece)
    {
        if (optionPanel.childCount > 0)//多个选项，这里销毁和生成不用多对象池因为考虑内部text需要更新
        {
            for (int i = 0; i < optionPanel.childCount; i++)
            {
                Destroy(optionPanel.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < piece.options.Count; i++)//生成Options
        {
            var option = Instantiate(optionPrefab, optionPanel);//后面一个参数代表所存放的父级
            option.UpdateOption(piece, piece.options[i]);
        }
    }
}

