using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBall : Enemy, API
{


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
            Destroy(gameObject);
            currentHealth = 0;
            isDead = true;
        }
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
