using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    private Transform player;

    private SpriteRenderer thisSprite;
    private SpriteRenderer playSprite;

    private Color color;

    [Header("时间控制参数")]
    public float activeTime;//显示时间
    public float activeStart;//开始显示的时间点

    [Header("不透明度控制")]
    private float alpha;//每次开始游戏让alpha等于设立的初始值
    public float alphaSet;//不透明度初始值
    public float alphaMultiplier;//不透明度变化速度
    private void OnEnable()//启用时自动调用
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;//获取player 方法：标签搜索 findgameobjectwithtag
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
            //返回对象池
            ShadowPool.instance.ReturnPool(this.gameObject);
        }
    }
}

