using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(DialogueController))]
public class QuestGiver : MonoBehaviour
{
    DialogueController controller;//���ƶԻ���ת
    QuestData_SO currentQuest;//��ǰ����

    public DialogueData_SO startDialogue;//����״̬������ͨ����ȡ�����ж��Ƿ�Ϊ�գ�Ȼ�����޸�ֵ
    public DialogueData_SO progressDialogue;//���������
    public DialogueData_SO completeDialogue;//������ɣ�������
    public DialogueData_SO finishDialogue;//������ˣ���ͨ�Ի�
    #region �������״̬
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

            for (int i = QuestManager.Instance.tasks.Count - 1; i >= 0; i--)//�˴����ã�������OtionUI�е��ã�Ϊ��ȷ���Ի���ת
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
