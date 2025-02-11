using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;



public class MainMenu : MonoBehaviour
{
    public GameObject settingsMenu;
    public void Start()
    {
        if (settingsMenu != null)
        {
            settingsMenu.SetActive(false);
        }
          
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void GoToSettingsMenu()
    {
        SceneManager.LoadScene("SettingsMenu");
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");    
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}

