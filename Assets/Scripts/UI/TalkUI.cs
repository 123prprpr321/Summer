using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkUI : MonoBehaviour //挂载于简易对话NPC
{

    public static bool isOpen;
    public GameObject talkUI;

    private bool isPlayerInside = false;
    private void FixedUpdate()  //通过Fixedupdate进行逻辑处理，目的是对话开始玩家停止控制
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
