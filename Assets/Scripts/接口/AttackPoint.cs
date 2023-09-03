using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPoint : MonoBehaviour
{
    public float damage;

    public float pauseTime = 0.05f;//顿帧长度
    private bool isPaused = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("FlyEnemy"))
        {
            if (this.gameObject.CompareTag("Attack"))
            {
                collision.GetComponent<API>().GetHit(damage);
                if (!isPaused) // 避免重复触发暂停
                {
                    StartCoroutine(PauseGame());
                }
            }
        }
    }
    private IEnumerator PauseGame()//通过简单中断游戏实现
    {
        isPaused = true;
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(pauseTime);
        Time.timeScale = 1f;
        isPaused = false;
    }
}
