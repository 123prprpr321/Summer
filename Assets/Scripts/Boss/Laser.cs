using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform player;
    public float projectileSpeed;
    public float destroyDelay;
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
       
        Vector3 direction = player.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * projectileSpeed;

        Destroy(gameObject, destroyDelay);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && collision.GetType().ToString() == "UnityEngine.CircleCollider2D")
        {
            collision.GetComponent<API>().GetHit(1f);
            Debug.Log("Hurt");
        }
        if (collision.CompareTag("Player") && collision.GetComponent<PlayController>().Defenseing == true)
        {
            Vector3 direction = player.position - transform.position;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = direction.normalized * -projectileSpeed;
        }
    }
}
