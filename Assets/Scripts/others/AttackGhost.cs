using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackGhost : MonoBehaviour
{
    void Start()
    {
        Invoke("clear", 3f);
    }
    void clear()
    {
        ObjectPool.Instance.PushObject(gameObject);
    }
    
}
