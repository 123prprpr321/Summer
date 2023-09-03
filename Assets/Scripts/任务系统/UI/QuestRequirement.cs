using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuestRequirement : MonoBehaviour
{
    private Text requireName;
    private Text progressNumber;

    void Awake()//一个本身的任务要求，一个子物体的进展
    {
        requireName = GetComponent<Text>();
        progressNumber = transform.GetChild(0).GetComponent<Text>();
    }

    public void SetupRequtirment(string name, int amount, int currentAmount)
    {
        requireName.text = name;
        progressNumber.text = currentAmount.ToString() + " / " + amount.ToString();
    }
}
