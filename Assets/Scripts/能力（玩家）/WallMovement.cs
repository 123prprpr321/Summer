using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMovement : MonoBehaviour
{
    public Vector3 wallOffset;//左右距离检测墙体
    public bool isLeftWall, isRightWall;
    public LayerMask wallLayer;//检测图层，后续可以扩展
    public bool isWallMove;
    private float hor = 1f;


    private Rigidbody2D rb;
    private PlayController controller;
    private Animator anim;

    public enum WallState
    {
        wallGrab,
        none
    }
    WallState nowSatae;
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<PlayController>();
        rb = GetComponent<Rigidbody2D>();
        nowSatae = WallState.none;
    }

    // Update is called once per frame
    void Update()
    {
        WallCheck();
       if(!controller.isGround && (isLeftWall || isRightWall))
       {
            isWallMove = true;
            controller.jumpCount = controller.jumpCountController;
            if(isLeftWall)
            {
                rb.transform.localScale = new Vector3(-1, 1, 1);
            }
            else if(isRightWall)
            {
                rb.transform.localScale = new Vector3(1, 1, 1);
            }
            anim.SetBool("iswalling", true);
       }
       else
       {
            isWallMove = false;
            anim.SetBool("iswalling", false);
        }
       if(isWallMove)
       {
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
            nowSatae = WallState.wallGrab;
            onWall();
       }
       else
       {
            nowSatae = WallState.none;
            rb.gravityScale = 5.2f;
       }
       if(!controller.isGround && (isLeftWall && isRightWall))
       {
            if (Input.GetKeyDown(KeyCode.S))
            {
                rb.transform.position -= new Vector3(0, 0.2f, 0);
            }
       }
    }
    void WallCheck()
    {
        Vector3 it = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);

        isLeftWall = Physics2D.OverlapCircle(it - wallOffset, 0.1f, wallLayer);
        isRightWall = Physics2D.OverlapCircle(it + wallOffset, 0.1f, wallLayer);
    }

    void Jump(Vector2 direction)//在墙上重置二段跳，以及实现蹬墙跳
    {

    }
    private void OnDrawGizmos()
    {
        Vector3 it = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        Gizmos.DrawWireSphere(it - wallOffset, 0.1f);
        Gizmos.DrawWireSphere(it + wallOffset, 0.1f);
    }
    private void onWall()//墙上水平行动控制方向
    {
        if (controller.horizontalMove != 0)
        {
            transform.localScale = new Vector3(controller.horizontalMove, 1, 1);
            hor = controller.horizontalMove;
        }
        if (controller.horizontalMove == 0)
        {
            transform.localScale = new Vector3(hor, 1, 1);
        }
    }
}
