using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatsManager : Singleton<CheatsManager>
{
    public List<string> cheatCodes;//�������б�

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
            "mhealth",   //�ظ���Ѫ������
            "inshealth", //�޵�ģʽ
            "jumpt",     //"��"����Ծ
            "hjump"      //��Ծ�������� 
            // ���������룬���������ϵͳ����������죬�޼���CD��ʱ���ټ�

            //�������
            //���ҩˮ
            //�˺�����
        };
    }

    public void GetHealth()
    {
        //�ظ���Ѫ������
        player.currentHealth = player.health;
    }
    public void NotDie()
    {
        //�޵�ģʽ
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
