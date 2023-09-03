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
            if(!DialogueUI.Instance.dialoguePanel.activeSelf)//��ֹ����
            OpenDialogue();
        }
    }
    void OpenDialogue()
    {
        //��UI���
        //����Ի�������Ϣ
        DialogueUI.Instance.UpdateDialogueData(currentData);//��ȡ�Ի�
        DialogueUI.Instance.UpdateMainDialogue(currentData.dialoguePieces[0]);//�����һ�仰
        Debug.Log(currentData);

    }
}
