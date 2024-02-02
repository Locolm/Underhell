using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad="lvl0";

    public GameObject settingsWindow;

    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void LoadSelectLvl()
    {
        string questsData = PlayerPrefs.GetString("Quests", "0,0,0");
        string[] questsArray = questsData.Split(',');

        int questProgression = int.Parse(questsArray[0]);

        if (questProgression == 0)
        {
            StartGame();
        }
        else
        {
            SceneManager.LoadScene("LevelSelect");
        }
    }

    public void SettingButton()
    {
        settingsWindow.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("End");
    }

    public void CreditScene()
    {
        SceneManager.LoadScene("Credit");
    }

    public void ViewUrl()
    {
        string url = "https://www.worldanvil.com/w/underhell-locolm";
        Application.OpenURL(url);
    }


}
