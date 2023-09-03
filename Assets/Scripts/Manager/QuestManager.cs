using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class QuestManager : Singleton<QuestManager>
{
    [System.Serializable]
    public class QuestTask//创建列表类，保证游戏开始时，列表可以被修改
    {
        public QuestData_SO questData;
        public bool isStarted { get { return questData.isStarted; } set { questData.isStarted = value; } }
        //属性（Property）的写法，实际上是调用了属性的getter和setter方法
        public bool isComplete { get { return questData.isComplete; } set { questData.isComplete = value; } }
        public bool isFinished { get { return questData.isFinished; } set { questData.isFinished = value; } }
    }
    public List<QuestTask> tasks = new List<QuestTask>();
    
    private void Update()
    {
      
    }
    //拾取物品，敌人死亡时调用，更新进度
    public void UpdateQuestProgress(string requireName, int amount)
    {
        foreach (var task in tasks)
        {
            var matchTask = task.questData.questRequires.Find(r => r.name == requireName);//找到符合要求的任务

            if (matchTask != null)
            {
                matchTask.currentAmount += amount;
            }
            task.questData.CheckQuestProgress();//每一个任务都需要检测
        }
    }


    public bool HaveQuest(QuestData_SO data)//判断名字是否已存在
    {
        if(data != null)
        {
            return tasks.Any(x => x.questData.questName == data.questName);//lambda表达式，这是Linq命名空间可以快速查找的函数
        }
        else
        {
            return false;
        }    
    }
    public QuestTask GetTask(QuestData_SO data)
    {
        return tasks.Find(x => x.questData.questName == data.questName);//lambda表达式，判断是否有对应名字的任务，有的话那就得到他，并将其开始
    }
}
