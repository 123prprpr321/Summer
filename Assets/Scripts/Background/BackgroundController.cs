using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public float speed = 0.1f; // 背景层级的移动速度，每个层级可以根据需求设置不同的速度值
    private Transform playerTransform; // 人物对象的引用
    private float lastPlayerX; // 上一帧玩家的X位置
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // 根据标签获取人物对象
        lastPlayerX = playerTransform.position.x;
    }



    void Update()
    {
        // 获取玩家的移动输入
        float moveInput = Input.GetAxis("Horizontal");

        // 如果玩家有移动输入，则更新背景层级的位置
        if (moveInput != 0f)
        {
            // 根据玩家当前位置和上一帧位置计算移动量
            float playerDeltaX = playerTransform.position.x - lastPlayerX;

            // 根据人物移动速度和背景速度计算位移量
            float offset = playerDeltaX * speed;

            // 计算背景层级的目标位置
            Vector2 targetPosition = new Vector2(transform.position.x + offset, transform.position.y);

            // 平滑移动背景层级
            transform.position = Vector2.Lerp(transform.position, targetPosition, Time.deltaTime);
        }

        // 更新上一帧的玩家X位置
        lastPlayerX = playerTransform.position.x;
    }
}
