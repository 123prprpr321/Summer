using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialoguePiece
{
    public string ID; //�������
    public Sprite image;   //ͼƬ��������
    [TextArea]
    public string text; //�ı�
    public QuestData_SO quest;
    public List<DialogueOption> options = new List<DialogueOption>();
}
