using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {

            if (collision.GetComponent<PlayController>().Defenseing == false)
            {
                collision.GetComponent<API>().GetHit(damage);
            }
            //�Ѳ��Կ��У�����Ϊbuff���֣�buffʱ�䣬buff��ֵ
            //Buff bleedingBuff = new Buff(Buff.BuffType.Bleeding, 3f, 0.5f);
            //collision.gameObject.GetComponent<PlayController>().ApplyBuff(bleedingBuff);
        }

    }

}
