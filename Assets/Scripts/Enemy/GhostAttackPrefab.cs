using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAttackPrefab : MonoBehaviour
{
    private float duration = 1.5f;//持续时间，与动画效果配合，后续设置为private
    private float moveSpeed = 8f;
    private float timer;
    private Vector2 moveDirection;//在Ghost获取实例来更改方向

    void Start()
    {
        timer = duration;

    }
    void Update()
    {
        if (timer > 0f)
        {
            float moveDirection = Mathf.Sign(transform.localScale.x); // 获取移动方向
            Vector2 movement = new Vector2(moveDirection, 0) * moveSpeed; // 设置移动距离
            transform.Translate(movement * Time.deltaTime);
            timer -= Time.deltaTime;
        }
        else
        {
            //回到对象池
            ObjectPool.Instance.PushObject(gameObject);
            timer = duration;
        }
    }
   
}
