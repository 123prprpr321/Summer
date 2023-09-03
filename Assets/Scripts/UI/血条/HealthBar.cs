using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image HealthPoint;   //health
    public Image HealthBack;    //background
    public Text healthText;     //text

    private PlayController player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayController>();
    }

    void Update()
    {
        HealthPoint.fillAmount = (float)player.currentHealth / (float)player.health;    //控制血条变化

        healthText.text = player.currentHealth.ToString() + "/" + player.health.ToString();

        if (HealthBack.fillAmount > HealthPoint.fillAmount) //血条缓冲
        {
            HealthBack.fillAmount -= 0.00135f;
        }
        else
        {
            HealthBack.fillAmount = HealthPoint.fillAmount;
        }
    }
}

