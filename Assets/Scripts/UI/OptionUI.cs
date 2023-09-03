using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OptionUI : MonoBehaviour
{
    //�ж�Option���������ɶ�Ӧ����
    public Text optionText;
    private Button thisButton;
    private DialoguePiece currentPiece;
    private string nextPieceID;//��תĿ��ID

    private bool takeQuest;//�Ƿ��������
    void Awake()
    {
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(OnOptionClicked);
    }
    public void UpdateOption(DialoguePiece piece, DialogueOption option)//�����ֱ��ǶԻ���ѡ��
    {
        currentPiece = piece;
        optionText.text = option.text;
        nextPieceID = option.targetID;
        takeQuest = option.takeQuest;
    }

    public void OnOptionClicked()//�ú��������������ת
    {
        if (currentPiece.quest != null)
        {
            var newTask = new QuestManager.QuestTask
            {
                questData = Instantiate(currentPiece.quest)
            };//������ȼ�
            //var newTask = new QuestManager.QuestTask();
            //newTask.questData = Instantiate(currentPiece.quest);

            if (takeQuest)
            {
                //�����ж�,��ӵ������б���Ҫ���ж��Ƿ��Ѵ��ڸ�����
                if(QuestManager.Instance.HaveQuest(newTask.questData))
                {
                    //���ھ��ж��Ƿ����
                    if (QuestManager.Instance.GetTask(newTask.questData).isComplete)
                    {
                        Debug.Log(465156);
                        newTask.questData.GiveRewards();
                        QuestManager.Instance.GetTask(newTask.questData).isFinished = true;
                    }
                }
                else
                {
                    //�����ھ���ӽ�ȥ
                    QuestManager.Instance.tasks.Add(newTask);
                    QuestManager.Instance.GetTask(newTask.questData).isStarted = true;//��������״̬��ע�ⲻ��ֱ���ٽű���isState��ֵ
                
                    foreach (var requireItem in newTask.questData.RequireTargetName())
                    {
                        InventoryManager.Instance.CheckQuestItemBag(requireItem);
                    }
                }
            }
        }
        if (nextPieceID == "")//�жϿ�
        {
            DialogueUI.Instance.dialoguePanel.SetActive(false);
            return;
        }
        else
        {
            //DialogueUI.Instance.UpdateMainDialogue(DialogueUI.Instance.currentData.dialoguePieces[nextPieceID]);//�������У���ΪID��һ�������֣�����ѡ���ֵ����ж�ID�Ƿ����
            DialogueUI.Instance.UpdateMainDialogue(DialogueUI.Instance.currentData.dialogueIndex[nextPieceID]);
            DialogueUI.Instance.currentIndex++;
        }
    }

}
