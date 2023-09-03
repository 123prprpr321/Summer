using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkUI : MonoBehaviour //�����ڼ��׶Ի�NPC
{

    public static bool isOpen;
    public GameObject talkUI;

    private bool isPlayerInside = false;
    private void FixedUpdate()  //ͨ��Fixedupdate�����߼�����Ŀ���ǶԻ���ʼ���ֹͣ����
    {
        isOpen = talkUI.activeSelf;
    }

    private void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.R))
        {
            talkUI.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }
}
