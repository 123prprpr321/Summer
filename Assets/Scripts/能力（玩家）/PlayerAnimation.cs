using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private PlayController controller;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<PlayController>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        SwitchAnim();
    }
    void SwitchAnim()
    {
        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));

        if (controller.isGround)
        {
            anim.SetBool("jumping", false);
            anim.SetBool("falling", false);
        }
        else if (!controller.isGround && rb.velocity.y > 0)
        {
            anim.SetBool("jumping", true);
        }
        else if (rb.velocity.y <= 0)
        {
            anim.SetBool("falling", true);
            anim.SetBool("jumping", false);
        }

    }
}
