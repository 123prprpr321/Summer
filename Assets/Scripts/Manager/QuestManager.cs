using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class QuestManager : Singleton<QuestManager>
{
    [System.Serializable]
    public class QuestTask//�����б��࣬��֤��Ϸ��ʼʱ���б���Ա��޸�
    {
        public QuestData_SO questData;
        public bool isStarted { get { return questData.isStarted; } set { questData.isStarted = value; } }
        //���ԣ�Property����д����ʵ�����ǵ��������Ե�getter��setter����
        public bool isComplete { get { return questData.isComplete; } set { questData.isComplete = value; } }
        public bool isFinished { get { return questData.isFinished; } set { questData.isFinished = value; } }
    }
    public List<QuestTask> tasks = new List<QuestTask>();
    
    private void Update()
    {
      
    }
    //ʰȡ��Ʒ����������ʱ���ã����½���
    public void UpdateQuestProgress(string requireName, int amount)
    {
        foreach (var task in tasks)
        {
            var matchTask = task.questData.questRequires.Find(r => r.name == requireName);//�ҵ�����Ҫ�������

            if (matchTask != null)
            {
                matchTask.currentAmount += amount;
            }
            task.questData.CheckQuestProgress();//ÿһ��������Ҫ���
        }
    }


    public bool HaveQuest(QuestData_SO data)//�ж������Ƿ��Ѵ���
    {
        if(data != null)
        {
            return tasks.Any(x => x.questData.questName == data.questName);//lambda���ʽ������Linq�����ռ���Կ��ٲ��ҵĺ���
        }
        else
        {
            return false;
        }    
    }
    public QuestTask GetTask(QuestData_SO data)
    {
        return tasks.Find(x => x.questData.questName == data.questName);//lambda���ʽ���ж��Ƿ��ж�Ӧ���ֵ������еĻ��Ǿ͵õ����������俪ʼ
    }
}
