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

    [Header("Quest Name")]//��ȡ��������
    public RectTransform questListTransform;
    public QuestNameButton questNameButton;

    [Header("Text Content")]//��������
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
        foreach (Transform item in questListTransform)//������Button
        {
            Destroy(item.gameObject);
        }
        foreach (Transform item in rewardTransform)//������Reward
        {
            Destroy(item.gameObject);
        }
        foreach (Transform item in requireTransform)//�������require
        {
            Destroy(item.gameObject);
        }

        foreach (var task in QuestManager.Instance.tasks)//��ȡ����
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

        foreach (var require in questData.questRequires)//����������Ҫ
        {
            var q = Instantiate(requirement, requireTransform);
            q.SetupRequtirment(require.name, require.requireAmount, require.currentAmount);//���½���
        }
    }
    public void SetupRewardItem(itemData_SO itemData, int amount)
    {
        var item = Instantiate(rewardUI, rewardTransform);
        item.SetupItemUI(itemData, amount);
    }
}