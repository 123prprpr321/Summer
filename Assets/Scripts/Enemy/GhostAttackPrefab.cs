using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAttackPrefab : MonoBehaviour
{
    private float duration = 1.5f;//����ʱ�䣬�붯��Ч����ϣ���������Ϊprivate
    private float moveSpeed = 8f;
    private float timer;
    private Vector2 moveDirection;//��Ghost��ȡʵ�������ķ���

    void Start()
    {
        timer = duration;

    }
    void Update()
    {
        if (timer > 0f)
        {
            float moveDirection = Mathf.Sign(transform.localScale.x); // ��ȡ�ƶ�����
            Vector2 movement = new Vector2(moveDirection, 0) * moveSpeed; // �����ƶ�����
            transform.Translate(movement * Time.deltaTime);
            timer -= Time.deltaTime;
        }
        else
        {
            //�ص������
            ObjectPool.Instance.PushObject(gameObject);
            timer = duration;
        }
    }
   
}
