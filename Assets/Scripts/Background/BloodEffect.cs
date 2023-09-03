using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffect : MonoBehaviour
{
    public float timeToDestroy = 0.6f;
    void Start()
    {
        Invoke("returnToPool", timeToDestroy);
    }
    void returnToPool()
    {
        ObjectPool.Instance.PushObject(gameObject);
    }
}
