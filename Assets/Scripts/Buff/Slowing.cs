using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slowing : MonoBehaviour
{
    private bool isSlowingApplied = false;
    private float timeBetweenBuffs = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.BoxCollider2D" && !isSlowingApplied)
        {
            Buff slowingBuff = new Buff(Buff.BuffType.Slowing, 5f, 0.6f);
            collision.gameObject.GetComponent<PlayController>().ApplyBuff(slowingBuff);
            isSlowingApplied = true;

            Invoke(nameof(ResetSlowingApplied), timeBetweenBuffs);
        }
    }

    private void ResetSlowingApplied()
    {
        isSlowingApplied = false;
    }
}
