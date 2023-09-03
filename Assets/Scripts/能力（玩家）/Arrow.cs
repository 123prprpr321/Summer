using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D rb;
    bool hasHit;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasHit == false)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)//ÃüÖÐÂß¼­,²åÈëµØÍ¼
    {
        if (collision.collider.tag == "Enemy" || collision.collider.tag == "FlyEnemy")
        {
            hasHit = true;
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            Destroy(gameObject, 0.1f);
        }
        else
        {
            hasHit = true;
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            Destroy(gameObject, 2f);
        }
    }
}
