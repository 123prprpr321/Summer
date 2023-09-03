using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dailogue", menuName = "Dialogue/Diglogue Data")]
public class DialogueData_SO : ScriptableObject//对话数据
{
    public List<DialoguePiece> dialoguePieces = new List<DialoguePiece>();//列表表示语句块

    public Dictionary<string, DialoguePiece> dialogueIndex = new Dictionary<string, DialoguePiece>();
#if UNITY_EDITOR//不影响打包
    void OnValidate()//当该脚本被加载或监控面板的值被修改时调用
    {
        dialogueIndex.Clear();
        foreach (var piece in dialoguePieces)
        {
            if (!dialogueIndex.ContainsKey(piece.ID))
                dialogueIndex.Add(piece.ID, piece);
        }
    }
#endif

    public QuestData_SO GetQuest()//得到当前任务
    {
        QuestData_SO currentQuest = null;
        foreach (var piece in dialoguePieces)
        {
            if (piece.quest != null)
                currentQuest = piece.quest;
        }
        return currentQuest;
    }
}
