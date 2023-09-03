using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;//DOTween���
public class DialogueUI : Singleton<DialogueUI>
{
    [Header("��������")]
    public Image icon;
    public Text mainText;
    public Button nextButton;
    public GameObject dialoguePanel;//�����Ϣ����ʼ����Ϊfalse

    [Header("Data")]
    public DialogueData_SO currentData;
    public int currentIndex = 0;

    [Header("Options")]
    public RectTransform optionPanel;
    public OptionUI optionPrefab;
    protected override void Awake()
    {
        base.Awake();
        nextButton.onClick.AddListener(ContinueDialogue);//���Button�������¼�������ContinueDialogue
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
        currentIndex = 0;//��֤���²���
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

        mainText.DOText(piece.text, 0.3f);//����������DOText��������һ���԰������⣬����ʱ����һ��

        //next��ť��ʣ��Ի�Ϊ0ʱ����ʾ������ѡ��ʱҲ����ʾ
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
        //����Options
        CreatOptions(piece);
    }
    void CreatOptions(DialoguePiece piece)
    {
        if (optionPanel.childCount > 0)//���ѡ��������ٺ����ɲ��ö�������Ϊ�����ڲ�text��Ҫ����
        {
            for (int i = 0; i < optionPanel.childCount; i++)
            {
                Destroy(optionPanel.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < piece.options.Count; i++)//����Options
        {
            var option = Instantiate(optionPrefab, optionPanel);//����һ��������������ŵĸ���
            option.UpdateOption(piece, piece.options[i]);
        }
    }
}

