using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(DialogueController))]
public class QuestGiver : MonoBehaviour
{
    DialogueController controller;//控制对话跳转
    QuestData_SO currentQuest;//当前任务

    public DialogueData_SO startDialogue;//任务状态，可以通过获取任务，判断是否为空，然后来修改值
    public DialogueData_SO progressDialogue;//任务进行中
    public DialogueData_SO completeDialogue;//任务完成，给奖励
    public DialogueData_SO finishDialogue;//都完成了，普通对话
    #region 获得任务状态
    public bool isStarted
    {
        get
        {
            if (QuestManager.Instance.HaveQuest(currentQuest))
            {
                return QuestManager.Instance.GetTask(currentQuest).isStarted;
            }
            else return false;

        }
    }
    public bool isComplete
    {
        get
        {
            if (QuestManager.Instance.HaveQuest(currentQuest))
            {
                return QuestManager.Instance.GetTask(currentQuest).isComplete;
            }
            else return false;
        }
    }
    public bool isFinished
    {
        get
        {
            if (QuestManager.Instance.HaveQuest(currentQuest))
            {
                return QuestManager.Instance.GetTask(currentQuest).isFinished;
            }
            else return false;
        }
    }
    #endregion
    void Awake()
    {
        controller = GetComponent<DialogueController>();

    }
    void Start()
    {
        controller.currentData = startDialogue;
        currentQuest = controller.currentData.GetQuest();
    }
    void Update()
    {

        if (isStarted)
        {
            if (isComplete)
            {
                controller.currentData = completeDialogue;
            }
            else
            {
                controller.currentData = progressDialogue;
            }
        }

        if (isFinished)
        {
            controller.currentData = finishDialogue;

            for (int i = QuestManager.Instance.tasks.Count - 1; i >= 0; i--)//此处调用，而不是OtionUI中调用，为了确保对话跳转
            {
                var task = QuestManager.Instance.tasks[i];
                if (task.isFinished)
                {
                    QuestManager.Instance.tasks.RemoveAt(i);
                    QuestUI.Instance.SetupQuestList();
                }
            }
        }
    }
}
