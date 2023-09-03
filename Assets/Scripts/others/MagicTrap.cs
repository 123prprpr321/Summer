using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicTrap : MonoBehaviour
{
    private Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void destroyself()
    {
        Destroy(gameObject); 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            anim.SetTrigger("start");
        }
    }
}
