using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cask : MonoBehaviour
{
    public float redDuration = 0.5f; // 变红持续时间
    public GameObject deathPrefab; // 死亡时生成的预制体
    private Renderer objectRenderer;
    private Color originalColor;
    private bool isColliding = false;
    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalColor = objectRenderer.material.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack") && !isColliding)
        {
            StartCoroutine(ChangeColorAndDie());
        }
    }

    private IEnumerator ChangeColorAndDie()
    {
        isColliding = true;

        // 变红
        objectRenderer.material.color = Color.red;

        yield return new WaitForSeconds(redDuration);

        // 恢复原来的颜色
        objectRenderer.material.color = originalColor;

        isColliding = false;
    }
    private void OnDestroy()
    {
        // 生成预制体
        Instantiate(deathPrefab, transform.position, Quaternion.identity);
    }
}
