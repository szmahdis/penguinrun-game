using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        //Set Cursor to be visible
        Cursor.visible = true;
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("PenguinRun");
    }


 public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
