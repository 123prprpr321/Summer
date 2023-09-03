using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Observe : MonoBehaviour
{
    public GameObject boss;
    private void Start()
    {
        Wizard.BossDeathEvent += HandleBossDeath;
    }

    private void OnDestroy()
    {
        Wizard.BossDeathEvent -= HandleBossDeath;
    }

    private void HandleBossDeath()
    {
        Debug.Log(123);
        Invoke("GoToNextLevel", 5f);
        DestroyBossObject();
    }

    private void GoToNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    private void DestroyBossObject()
    {
        Destroy(boss);
    }
}
