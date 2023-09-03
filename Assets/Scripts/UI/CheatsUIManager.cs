using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CheatsUIManager : MonoBehaviour
{
    public GameObject cheatPanel;
    public TMP_InputField cheatInput;

    private bool isCheatPanelActive = false;

    public delegate void IsPanelOpenRight(bool isActive);//申明委托
    public event IsPanelOpenRight CheckIsOpen;//申明事件
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            ToggleCheatPanel();
        }
    }

    public void CheckCheatCode()
    {
        string inputCode = cheatInput.text.ToLower();

        // 在这里添加作弊码的检查逻辑
        if (inputCode == "mhealth")
        {
            CheatsManager.Instance.GetHealth();
        }
        else if (inputCode == "inshealth")
        {
            CheatsManager.Instance.NotDie();
        }
        else if (inputCode == "jumpt")
        {
            CheatsManager.Instance.JumpUp();
        }
        else if (inputCode == "hjump")
        {
            CheatsManager.Instance.Hjump();
        }

            ClearInputField();
    }

    void ToggleCheatPanel()
    {
        isCheatPanelActive = !isCheatPanelActive;
        cheatPanel.SetActive(isCheatPanelActive);
        if (isCheatPanelActive)
        {
            cheatInput.text = ""; // 清空输入框内容
            cheatInput.Select();  // 设置输入框为活动状态
        }
        if (CheckIsOpen != null)
        {
            CheckIsOpen(cheatPanel.activeSelf);//启用委托
        }
    }

    void ClearInputField()
    {
        cheatInput.text = "";  // 清空输入框内容
        cheatInput.Select();   // 设置输入框为活动状态
    }
}
