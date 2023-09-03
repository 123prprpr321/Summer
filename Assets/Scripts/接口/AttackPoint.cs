using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPoint : MonoBehaviour
{
    public float damage;

    public float pauseTime = 0.05f;//��֡����
    private bool isPaused = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("FlyEnemy"))
        {
            if (this.gameObject.CompareTag("Attack"))
            {
                collision.GetComponent<API>().GetHit(damage);
                if (!isPaused) // �����ظ�������ͣ
                {
                    StartCoroutine(PauseGame());
                }
            }
        }
    }
    private IEnumerator PauseGame()//ͨ�����ж���Ϸʵ��
    {
        isPaused = true;
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(pauseTime);
        Time.timeScale = 1f;
        isPaused = false;
    }
}
