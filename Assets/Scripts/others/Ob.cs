using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ob : MonoBehaviour
{
    public GameObject bo;
    public GameObject next;
    private void Update()
    {
        if (bo == null)
        {
            next.SetActive(true);
        }
    }
}
