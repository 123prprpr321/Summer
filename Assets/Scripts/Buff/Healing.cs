using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    private bool isHealingApplied = false;
    private float timeBetweenBuffs = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.BoxCollider2D" && !isHealingApplied)
        {
            Buff healingBuff = new Buff(Buff.BuffType.Healing, 5f, 1f);
            collision.gameObject.GetComponent<PlayController>().ApplyBuff(healingBuff);
            isHealingApplied = true;

            Invoke(nameof(ResetHealingApplied), timeBetweenBuffs);
        }
    }

    private void ResetHealingApplied()
    {
        isHealingApplied = false;
    }
}
