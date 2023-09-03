using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class WallHide : MonoBehaviour
{
    public Tilemap wallTilemap;
    public Color targetColor;
    public float transitionSpeed = 1f;

    private Color initialColor;
    private bool isPlayerInside;

    private void Start()
    {
        if (wallTilemap != null)
        {
            initialColor = wallTilemap.color;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            isPlayerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            isPlayerInside = false;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            isPlayerInside = true;
        }
    }

    private void Update()
    {

        if (wallTilemap != null)
        {
            if (isPlayerInside)
            {
                wallTilemap.color = Color.Lerp(wallTilemap.color, targetColor, transitionSpeed * Time.deltaTime);
            }
            else
            {
                wallTilemap.color = Color.Lerp(wallTilemap.color, initialColor, transitionSpeed * Time.deltaTime);
            }
        }
    }
}
