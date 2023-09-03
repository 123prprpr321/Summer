using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeHit : MonoBehaviour, API
{

    public float currentHealth;
    public Animator anim;
    private void Start()
    {
        anim = transform.parent.GetComponent<Animator>();

    }
    public void GetHit(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            anim.Play("fort-dead");
            Debug.Log(123);
        }

    }
    void destroyself()
    {
        Destroy(gameObject);
    }

    public void ApplyHealth(float number)
    {

    }
}
