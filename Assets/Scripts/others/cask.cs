using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cask : MonoBehaviour
{
    public float redDuration = 0.5f; // ������ʱ��
    public GameObject deathPrefab; // ����ʱ���ɵ�Ԥ����
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

        // ���
        objectRenderer.material.color = Color.red;

        yield return new WaitForSeconds(redDuration);

        // �ָ�ԭ������ɫ
        objectRenderer.material.color = originalColor;

        isColliding = false;
    }
    private void OnDestroy()
    {
        // ����Ԥ����
        Instantiate(deathPrefab, transform.position, Quaternion.identity);
    }
}
