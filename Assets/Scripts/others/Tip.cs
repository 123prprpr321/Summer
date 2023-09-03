using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tip : MonoBehaviour
{
    public GameObject ObjectTip;
  
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            ObjectTip.SetActive(true);

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            ObjectTip.SetActive(false);
        }
    }
}
