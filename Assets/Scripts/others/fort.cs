using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fort : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public Animator anim;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {

            anim.Play("fort");

        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            anim.Play("fort");

        }
    }

    void Fire()
    {
        GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab);
        bullet.transform.position = transform.position + new Vector3(-0.5f, 0.25f, 0f);
        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.left * bulletSpeed;
    }

  
    void destroyself()
    {
        Destroy(gameObject);
    }

}
