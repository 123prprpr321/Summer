using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public float speed = 0.1f; // �����㼶���ƶ��ٶȣ�ÿ���㼶���Ը����������ò�ͬ���ٶ�ֵ
    private Transform playerTransform; // ������������
    private float lastPlayerX; // ��һ֡��ҵ�Xλ��
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // ���ݱ�ǩ��ȡ�������
        lastPlayerX = playerTransform.position.x;
    }



    void Update()
    {
        // ��ȡ��ҵ��ƶ�����
        float moveInput = Input.GetAxis("Horizontal");

        // ���������ƶ����룬����±����㼶��λ��
        if (moveInput != 0f)
        {
            // ������ҵ�ǰλ�ú���һ֡λ�ü����ƶ���
            float playerDeltaX = playerTransform.position.x - lastPlayerX;

            // ���������ƶ��ٶȺͱ����ٶȼ���λ����
            float offset = playerDeltaX * speed;

            // ���㱳���㼶��Ŀ��λ��
            Vector2 targetPosition = new Vector2(transform.position.x + offset, transform.position.y);

            // ƽ���ƶ������㼶
            transform.position = Vector2.Lerp(transform.position, targetPosition, Time.deltaTime);
        }

        // ������һ֡�����Xλ��
        lastPlayerX = playerTransform.position.x;
    }
}
