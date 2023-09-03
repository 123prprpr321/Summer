using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy, API
{
    public GameObject BloodEffect;
    public ParticleSystem HitEffect;
    private SpriteRenderer sp;
    private Rigidbody2D rb;
    private float recordSpeed;
    public override void Init()
    {
        base.Init();
        currentHealth = health;
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        recordSpeed = Speed;
    }
    public void GetHit(float damage)
    {
        sp.color = Color.red;
      
        hiteff();
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
        }
        anim.SetTrigger("hit");
        Invoke("ResetColor", 0.33f);
    }
    void hiteff()
    {
        HitEffect.Play();
    }
    public void ApplyHealth(float number)
    {

    }
    public void ResetColor()
    {
        sp.color = Color.white;
    }
    public override void AttackAction()//���ع�������
    {
        if (Vector2.Distance(targetPoint.position, transform.position) < attackRange)
        {
            if (nextnormalAttack <= 0)
            {
                //���Ŷ���
                Debug.Log("��ͨ����");
                anim.SetTrigger("attack");
                Speed = 0;
                nextnormalAttack = attackRate;
            }
        }

    }
    public void resetSpeed()//�¼�
    {
        Speed = recordSpeed;
    }
    public override void SkillAction()
    {

    }
}
