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

    public delegate void IsPanelOpenRight(bool isActive);//����ί��
    public event IsPanelOpenRight CheckIsOpen;//�����¼�
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

        // ���������������ļ���߼�
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
            cheatInput.text = ""; // ������������
            cheatInput.Select();  // ���������Ϊ�״̬
        }
        if (CheckIsOpen != null)
        {
            CheckIsOpen(cheatPanel.activeSelf);//����ί��
        }
    }

    void ClearInputField()
    {
        cheatInput.text = "";  // ������������
        cheatInput.Select();   // ���������Ϊ�״̬
    }
}
