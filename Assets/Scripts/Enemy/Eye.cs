using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : Enemy, API
{
    public GameObject BloodEffect;

    private SpriteRenderer sp;
    private Rigidbody2D rb;
    public override void Init()
    {
        base.Init();
        currentHealth = health;
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
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


}
