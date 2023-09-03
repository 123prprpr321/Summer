using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialoguePiece
{
    public string ID; //Óï¾ä¿éĞòºÅ
    public Sprite image;   //Í¼Æ¬£¬ÈÎÎñ½±Àø
    [TextArea]
    public string text; //ÎÄ±¾
    public QuestData_SO quest;
    public List<DialogueOption> options = new List<DialogueOption>();
}
