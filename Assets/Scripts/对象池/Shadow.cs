using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    private Transform player;

    private SpriteRenderer thisSprite;
    private SpriteRenderer playSprite;

    private Color color;

    [Header("ʱ����Ʋ���")]
    public float activeTime;//��ʾʱ��
    public float activeStart;//��ʼ��ʾ��ʱ���

    [Header("��͸���ȿ���")]
    private float alpha;//ÿ�ο�ʼ��Ϸ��alpha���������ĳ�ʼֵ
    public float alphaSet;//��͸���ȳ�ʼֵ
    public float alphaMultiplier;//��͸���ȱ仯�ٶ�
    private void OnEnable()//����ʱ�Զ�����
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;//��ȡplayer ��������ǩ���� findgameobjectwithtag
        thisSprite = GetComponent<SpriteRenderer>();
        playSprite = player.GetComponent<SpriteRenderer>();

        alpha = alphaSet;

        thisSprite.sprite = playSprite.sprite;

        transform.position = player.position;
        transform.localScale = player.localScale;
        transform.rotation = player.rotation;

        activeStart = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        alpha *= alphaMultiplier;

        color = new Color(0.5f, 0.5f, 1f, 0.8f);
        thisSprite.color = color;

        if (Time.time >= (activeStart + activeTime))
        {
            //���ض����
            ShadowPool.instance.ReturnPool(this.gameObject);
        }
    }
}

