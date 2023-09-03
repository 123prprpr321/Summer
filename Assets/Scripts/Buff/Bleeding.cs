using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleeding : MonoBehaviour
{
    private bool isBleedingApplied = false;
    private float timeBetweenBuffs = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.BoxCollider2D" && !isBleedingApplied)
        {
            Buff bleedingBuff = new Buff(Buff.BuffType.Bleeding, 5f, 1f);
            collision.gameObject.GetComponent<PlayController>().ApplyBuff(bleedingBuff);
            isBleedingApplied = true;

            Invoke(nameof(ResetBleedingApplied), timeBetweenBuffs);
        }
    }

    private void ResetBleedingApplied()
    {
        isBleedingApplied = false;
    }
}
