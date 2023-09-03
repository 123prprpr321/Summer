using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : Enemy,API
{
    public GameObject SPrefabe;
    public GameObject BloodEffect;
    public float teleportChance = 0.75f; // 瞬移的概率
    public float teleportRange = 50f; // 随机位置的范围
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
    public override void AttackAction() //攻击，由于是虚方法，并且该敌人特性，直接将普通攻击与技能攻击写在一起
    {
        if (Vector2.Distance(targetPoint.position, transform.position) < attackRange)
        {
            if (nextnormalAttack <= 0)
            {
                //播放动画
                Debug.Log("普通攻击");
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
                        //播放动画
                        Debug.Log("技能攻击");
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
    void ChangePosition()//动画帧事件
    {
        Vector3 randomPosition = targetPoint.position + new Vector3(Random.Range(-teleportRange, teleportRange), 0.3f, 0f);
        Debug.Log(targetPoint);
        transform.position = randomPosition;
    }
    void hiteff()
    {
        HitEffect.Play();
    }
    public void pushpool()//动画帧事件
    {
        item = ObjectPool.Instance.GetObject(SPrefabe);
        item.transform.position = targetPoint.transform.position + new Vector3(0, 2.4f, 0);
        Invoke("returnpool", 0.72f);
    }
    public override void SkillAction()//特殊技能
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
