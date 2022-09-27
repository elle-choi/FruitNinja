using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // using built-in Unity UI
using UnityEngine.SceneManagement;


public class StartMenu : MonoBehaviour
{
    public void StartGame(int level)
    {
        SceneManager.LoadScene(1);
    }


    public void EndGame()
    {
        Application.Quit();
    }

}
