using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ShowCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }  
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void ExitGame()
    {

        Application.Quit();
    }
}
