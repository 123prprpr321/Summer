using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueOption
{
    public string text;
    public string targetID;//对话跳转位置
    public bool takeQuest;//是否接受任务
}
