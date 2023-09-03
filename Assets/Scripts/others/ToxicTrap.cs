using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicTrap : MonoBehaviour
{
    public GameObject DbPrefab;
    void Fire()
    {
        GameObject bullet = ObjectPool.Instance.GetObject(DbPrefab);
        bullet.transform.position = transform.position + new Vector3(0f, 1.8f, 0f);
    }
}
