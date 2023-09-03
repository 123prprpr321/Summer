using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilePlatform : MonoBehaviour
{
    public float Speed;
    public float waitTime;
    public Transform moveNext;
    public Transform left;
    public Transform right;
    public float startWaitTime;

    private Transform playerTransform;

    private int P;
    void Start()
    {
        P = 1;
        waitTime = startWaitTime;
        moveNext.position = left.position;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform.parent;
    }


    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveNext.position, Speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, moveNext.position) < 0.1f)
        {
            if (waitTime <= 0 && P == 1)
            {
                moveNext.position = right.position;
                waitTime = startWaitTime;
                P = 0;
            }
            else if (waitTime <= 0 && P == 0)
            {
                moveNext.position = left.position;
                waitTime = startWaitTime;
                P = 1;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.parent = gameObject.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.parent = playerTransform;
        }
    }
}
