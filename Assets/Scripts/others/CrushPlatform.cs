using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushPlatform : MonoBehaviour
{
    public float BackTime;
    private Animator anim;
    private BoxCollider2D boxColl;

    void Start()
    {
        anim = GetComponent<Animator>();
        boxColl = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("start");
        }
    }
    void hideColl()
    {
        boxColl.enabled = false;
    }
    void BackState()
    {
        Invoke("Creat", BackTime);
    }
    void Creat()
    {
        anim.Play("idle");
        boxColl.enabled = true;
    }
}
