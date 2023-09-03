using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dailogue", menuName = "Dialogue/Diglogue Data")]
public class DialogueData_SO : ScriptableObject//�Ի�����
{
    public List<DialoguePiece> dialoguePieces = new List<DialoguePiece>();//�б��ʾ����

    public Dictionary<string, DialoguePiece> dialogueIndex = new Dictionary<string, DialoguePiece>();
#if UNITY_EDITOR//��Ӱ����
    void OnValidate()//���ýű������ػ�������ֵ���޸�ʱ����
    {
        dialogueIndex.Clear();
        foreach (var piece in dialoguePieces)
        {
            if (!dialogueIndex.ContainsKey(piece.ID))
                dialogueIndex.Add(piece.ID, piece);
        }
    }
#endif

    public QuestData_SO GetQuest()//�õ���ǰ����
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
