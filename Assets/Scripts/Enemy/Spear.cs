using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : Enemy, API
{
    public GameObject BloodEffect;

    private SpriteRenderer sp;
    private Rigidbody2D rb;

    private bool isSkillAttacking = false;//是否正在执行技能攻击
    public override void Init()
    {
        base.Init();
        currentHealth = health;
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    public override void AttackAction()//重载攻击函数
    {
        if (Vector2.Distance(targetPoint.position, transform.position) < skillRange)
        {
            if (nextskillAttack <= 0)
            {

                anim.SetTrigger("skill");
                isSkillAttacking = true;
                StartCoroutine("MoveToGoal");
                nextskillAttack = skillattackRate;
            }
        }
        if (!isSkillAttacking && nextnormalAttack <= 0)
        {
            if (Vector2.Distance(targetPoint.position, transform.position) < attackRate)
            {
                if (nextnormalAttack <= 0)
                {
                    anim.SetTrigger("attack");
                    nextnormalAttack = attackRate;
                }
            }
        }
    }

    public override void SkillAction() 
    {
       
    }

    IEnumerator MoveToGoal()
    {
        float timeC = 0;
        Vector2 destinationPosition;
        float y = transform.position.y;
        if (transform.position.x < targetPoint.position.x)
        {
            destinationPosition = (Vector2)targetPoint.position + new Vector2(3f, 0.3f);
        }
        else
        {
            destinationPosition = (Vector2)targetPoint.position + new Vector2(-3f, 0.3f);
        }
        while (timeC < 1f) //动画时长
        {
            transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, destinationPosition.x, Speed * Time.deltaTime * 1.5f), transform.position.y, transform.position.z);
            timeC += Time.deltaTime;

            yield return null;
        }
        transform.position = new Vector2(destinationPosition.x, y);
    }
    public void GetHit(float damage)
    {
        sp.color = Color.red;
       
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
        }
        anim.SetTrigger("hit");
        Invoke("ResetColor", 0.33f);
    }
    public void ResetColor()
    {
        sp.color = Color.white;
    }

    public void ApplyHealth(float number)
    {

    }
  
    void FinishNormalAttack()//动画帧事件
    {
        isSkillAttacking = false;
    }


    void FinishSkillAttack()//动画帧事件
    {
        isSkillAttacking = false;
    }
}
