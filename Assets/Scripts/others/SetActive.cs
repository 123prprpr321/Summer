using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour
{
    public GameObject targetObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("readyT");
            targetObject.SetActive(true);
            Debug.Log(targetObject.activeSelf);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            targetObject.SetActive(false);
        }
    }
}
