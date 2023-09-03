using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Db : MonoBehaviour
{
    public float speed = 6f;
    public int _IntHor = 1;
    public float destroyDelay = 5f;
    private float timer = 0f;
    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime * _IntHor);
        timer += Time.deltaTime;
        if (timer >= destroyDelay)
        {
            ObjectPool.Instance.PushObject(gameObject);
        }
    }
    private void OnEnable()
    {
        timer = 0;
    }
}
