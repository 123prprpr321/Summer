using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : Enemy,API
{
    public GameObject SPrefabe;
    public GameObject BloodEffect;
    public float teleportChance = 0.75f; // ˲�Ƶĸ���
    public float teleportRange = 50f; // ���λ�õķ�Χ
    private SpriteRenderer sp;
    private Rigidbody2D rb;
    private GameObject item;
    public Transform player;
    public ParticleSystem HitEffect;
    public override void Init()
    {
        base.Init();
        currentHealth = health;
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
    public override void AttackAction() //�������������鷽�������Ҹõ������ԣ�ֱ�ӽ���ͨ�����뼼�ܹ���д��һ��
    {
        if (Vector2.Distance(targetPoint.position, transform.position) < attackRange)
        {
            if (nextnormalAttack <= 0)
            {
                //���Ŷ���
                Debug.Log("��ͨ����");
                anim.SetTrigger("attack");

                nextnormalAttack = attackRate;
            }
        }
        if (nextnormalAttack>2 && nextskillAttack <= -1)
        {
            if (Vector2.Distance(targetPoint.position, transform.position) < skillRange)
            {
                 if (nextskillAttack <= 0)
                 {
                        //���Ŷ���
                        Debug.Log("���ܹ���");
                        anim.SetTrigger("skill");
                        nextskillAttack = skillattackRate;
                 }
            }
        }
    }
    void MaybeChangePosition()
    {
        if (Random.value <= teleportChance)
        {
            anim.Play("Death-change");
            if (anim.GetCurrentAnimatorStateInfo(1).IsName("Death-change"))
            {
                Speed = 0;
            }
        }
    }
    void ChangePosition()//����֡�¼�
    {
        Vector3 randomPosition = targetPoint.position + new Vector3(Random.Range(-teleportRange, teleportRange), 0.3f, 0f);
        Debug.Log(targetPoint);
        transform.position = randomPosition;
    }
    void hiteff()
    {
        HitEffect.Play();
    }
    public void pushpool()//����֡�¼�
    {
        item = ObjectPool.Instance.GetObject(SPrefabe);
        item.transform.position = targetPoint.transform.position + new Vector3(0, 2.4f, 0);
        Invoke("returnpool", 0.72f);
    }
    public override void SkillAction()//���⼼��
    {
        base.SkillAction();
    }
    void returnpool()
    {
        ObjectPool.Instance.PushObject(item);
    }
    public void GetHit(float damage)
    {
        sp.color = Color.red;
        GameObject temp = ObjectPool.Instance.GetObject(BloodEffect);
        temp.transform.position = transform.position;
        hiteff();
        
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
        }
        anim.SetTrigger("hit");
        Invoke("ResetColor",0.33f);
    }
    public void ResetColor()
    {
        sp.color = Color.white;
    }

    public void ApplyHealth(float number)
    {
        
    }

}
