using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class Menu : MonoBehaviour
{
    private GameObject lastSelect;
    void Start()
    {
        lastSelect = new GameObject();
    }
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelect);
        }
        else
        {
            lastSelect = EventSystem.current.currentSelectedGameObject;
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
