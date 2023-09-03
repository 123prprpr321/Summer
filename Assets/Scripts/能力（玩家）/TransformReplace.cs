using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformReplace : MonoBehaviour
{
    public float Speed;
    public float destroyDistance;
    private Vector3 startPosition;
    private Rigidbody2D rb;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if (GameObject.FindGameObjectWithTag("Player").transform.localScale.x == 1)
        {
            rb.transform.localScale *= 1;
            rb.velocity = Speed * transform.right;
        }
        if (GameObject.FindGameObjectWithTag("Player").transform.localScale.x == -1)
        {
            rb.transform.localScale *= -1;
            rb.velocity = -Speed * transform.right;
        }
        startPosition = transform.position;
    }

    void Update()
    {
        float distance = (transform.position - startPosition).sqrMagnitude;
        if (distance >= destroyDistance)
        {
            Destroy(gameObject);
            //ObjectPool.Instance.PushObject(gameObject);
        }
    }
}
