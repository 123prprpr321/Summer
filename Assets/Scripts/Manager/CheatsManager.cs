using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatsManager : Singleton<CheatsManager>
{
    public List<string> cheatCodes;//作弊码列表

    private PlayController player;

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayController>();

    }
   
    void Start()
    {
        cheatCodes = new List<string>()
        {
            "mhealth",   //回复至血量上限
            "inshealth", //无敌模式
            "jumpt",     //"多"次跳跃
            "hjump"      //跳跃力度提升 
            // 其他作弊码，等完成武器系统后继续，飞天，无技能CD有时间再加

            //获得武器
            //获得药水
            //伤害提升
        };
    }

    public void GetHealth()
    {
        //回复至血量上限
        player.currentHealth = player.health;
    }
    public void NotDie()
    {
        //无敌模式
        Debug.Log("OK");
        player.health = 9999f;
        player.currentHealth = player.health;
    }
    public void JumpUp()
    {
        player.jumpCount = 100;
    }
    public void Hjump()
    {
        player.jumpForce = 50f;
    }
}
