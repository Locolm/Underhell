using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public bool gamePause= false;

    public GameObject pauseMenu;


    public static PauseMenu instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PauseMenu dans la sc�ne");
            return;
        }

        instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePause)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
        
    }

    void Paused()
    {
        PlayerMove.instance.enabled = false;
        //activer menu
        pauseMenu.SetActive(true);
        //arr�ter le temps
        Time.timeScale = 0 ;
        //changer le status du jeu
        gamePause = true;
    }

    public void Resume()
    {
        PlayerMove.instance.enabled = true;
        //activer menu
        pauseMenu.SetActive(false);
        //arr�ter le temps
        Time.timeScale = 1;
        //changer le status du jeu
        gamePause = false;
    }

    public void LoadMainMenu()
    {
        Resume();
        SceneManager.LoadScene("MainMenu");
        //reset le hasSwitched de l'audio
        AudioManager.instance.hasSwitched = false;
    }
}
