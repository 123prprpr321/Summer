using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuestNameButton : MonoBehaviour
{
    public Text questNameText;//任务名字
    public QuestData_SO currentData;//任务数据
    public Text questContentText;
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(UpdateQuestContent);
    }
    void UpdateQuestContent()//获取所有任务信息，之后更新UI
    {
        questContentText.text = currentData.description;
        QuestUI.Instance.SetupRequireList(currentData);

        foreach (Transform item in QuestUI.Instance.rewardTransform)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in currentData.rewards)
        {
            QuestUI.Instance.SetupRewardItem(item.itemData, item.amount);
        }
    }
    public void SetupNameButton(QuestData_SO questData)
    {
        currentData = questData;

        if (questData.isComplete)
            questNameText.text = questData.questName + "(完成)";
        else
            questNameText.text = questData.questName;
    }
}
