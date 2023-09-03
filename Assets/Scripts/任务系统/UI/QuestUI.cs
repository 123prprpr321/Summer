using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuestUI : Singleton<QuestUI>
{
    [Header("Elements")]
    public GameObject questPanel;
    public ItemToolTip tooltip;
    bool isOpen;

    [Header("Quest Name")]//获取任务名字
    public RectTransform questListTransform;
    public QuestNameButton questNameButton;

    [Header("Text Content")]//任务描述
    public Text questContentText;

    [Header("Requirement")]
    public RectTransform requireTransform;
    public QuestRequirement requirement;

    [Header("Reward Panel")]
    public RectTransform rewardTransform;
    public ItemUI rewardUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isOpen = !isOpen;
            questPanel.SetActive(isOpen);
            questContentText.text = "";

            SetupQuestList();
            if (!isOpen)
                tooltip.gameObject.SetActive(false);
        }
    }
    public void SetupQuestList()
    {
        foreach (Transform item in questListTransform)//先销毁Button
        {
            Destroy(item.gameObject);
        }
        foreach (Transform item in rewardTransform)//再销毁Reward
        {
            Destroy(item.gameObject);
        }
        foreach (Transform item in requireTransform)//最后销毁require
        {
            Destroy(item.gameObject);
        }

        foreach (var task in QuestManager.Instance.tasks)//获取名字
        {
            var newTask = Instantiate(questNameButton, questListTransform);

            newTask.SetupNameButton(task.questData);
            newTask.questContentText = questContentText;
        }
    }

    public void SetupRequireList(QuestData_SO questData)
    {
        //questContentText.text = questData.description;
        foreach (Transform item in requireTransform)
        {
            Destroy(item.gameObject);
        }

        foreach (var require in questData.questRequires)//生成所有需要
        {
            var q = Instantiate(requirement, requireTransform);
            q.SetupRequtirment(require.name, require.requireAmount, require.currentAmount);//更新进程
        }
    }
    public void SetupRewardItem(itemData_SO itemData, int amount)
    {
        var item = Instantiate(rewardUI, rewardTransform);
        item.SetupItemUI(itemData, amount);
    }
}