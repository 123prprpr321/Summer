using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OptionUI : MonoBehaviour
{
    //判断Option数量，生成对应个数
    public Text optionText;
    private Button thisButton;
    private DialoguePiece currentPiece;
    private string nextPieceID;//跳转目标ID

    private bool takeQuest;//是否接受任务
    void Awake()
    {
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(OnOptionClicked);
    }
    public void UpdateOption(DialoguePiece piece, DialogueOption option)//参数分别是对话和选项
    {
        currentPiece = piece;
        optionText.text = option.text;
        nextPieceID = option.targetID;
        takeQuest = option.takeQuest;
    }

    public void OnOptionClicked()//该函数处理点击后的跳转
    {
        if (currentPiece.quest != null)
        {
            var newTask = new QuestManager.QuestTask
            {
                questData = Instantiate(currentPiece.quest)
            };//与下面等价
            //var newTask = new QuestManager.QuestTask();
            //newTask.questData = Instantiate(currentPiece.quest);

            if (takeQuest)
            {
                //各种判断,添加到任务列表，需要先判断是否已存在该任务
                if(QuestManager.Instance.HaveQuest(newTask.questData))
                {
                    //存在就判断是否完成
                    if (QuestManager.Instance.GetTask(newTask.questData).isComplete)
                    {
                        Debug.Log(465156);
                        newTask.questData.GiveRewards();
                        QuestManager.Instance.GetTask(newTask.questData).isFinished = true;
                    }
                }
                else
                {
                    //不存在就添加进去
                    QuestManager.Instance.tasks.Add(newTask);
                    QuestManager.Instance.GetTask(newTask.questData).isStarted = true;//更改任务状态，注意不能直接再脚本给isState赋值
                
                    foreach (var requireItem in newTask.questData.RequireTargetName())
                    {
                        InventoryManager.Instance.CheckQuestItemBag(requireItem);
                    }
                }
            }
        }
        if (nextPieceID == "")//判断空
        {
            DialogueUI.Instance.dialoguePanel.SetActive(false);
            return;
        }
        else
        {
            //DialogueUI.Instance.UpdateMainDialogue(DialogueUI.Instance.currentData.dialoguePieces[nextPieceID]);//这样不行，因为ID不一定是数字，所以选择字典来判断ID是否符合
            DialogueUI.Instance.UpdateMainDialogue(DialogueUI.Instance.currentData.dialogueIndex[nextPieceID]);
            DialogueUI.Instance.currentIndex++;
        }
    }

}
