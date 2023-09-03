using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDestroyMap : MonoBehaviour
{
    public int damage;
    public float range;
    public LayerMask destroyAble;
    public Transform point;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Environmental"))
        {
            Collider2D[] objects = Physics2D.OverlapCircleAll(point.position, range, destroyAble);
            for (int i = 0; i < objects.Length; i++)
            {
                Debug.Log(123);
                objects[i].GetComponent<DestroyMap>().health -= damage;
            }
        }
    }
    public void OnDrawGizmos()//画图形，不需要调用
    {
        Gizmos.DrawWireSphere(point.position, range);
    }

}
