using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ReturnMenu : MonoBehaviour
{
    void REreturn()//¶¯»­Ö¡
    {
        StartCoroutine(LoadLevel());
    }
    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(0);

    }
}
