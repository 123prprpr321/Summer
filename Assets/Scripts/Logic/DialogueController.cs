using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public DialogueData_SO currentData;
    bool canTalk = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && currentData != null)
        {
            canTalk = true;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        canTalk = false;
        if (collision.CompareTag("Player") && currentData != null)
        {
            DialogueUI.Instance.dialoguePanel.SetActive(false);
        }
    }
    void Update()
    {

        if (canTalk && Input.GetKeyDown(KeyCode.R))
        {
            if(!DialogueUI.Instance.dialoguePanel.activeSelf)//防止重启
            OpenDialogue();
        }
    }
    void OpenDialogue()
    {
        //打开UI面板
        //传输对话内容信息
        DialogueUI.Instance.UpdateDialogueData(currentData);//获取对话
        DialogueUI.Instance.UpdateMainDialogue(currentData.dialoguePieces[0]);//传入第一句话
        Debug.Log(currentData);

    }
}
