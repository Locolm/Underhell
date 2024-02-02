using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public GameObject GameOverMenu;

    public static GameOver instance;

    public Animator fadeSystem;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de GameOver dans la sc�ne");
            return;
        }

        instance = this;
    }

    public void OnPlayerDeath()
    {
        GameOverMenu.SetActive(true);

    }

    public void RetryButton()
    {
        //recharge la sc�ne du spawner
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //SceneManager.LoadScene("lvl2"); //respawn depuis le spawner de playerprefs
        //replace le joueur sur le spawner
        // R�activer les mouvements du joueurs
        PlayerHealth.instance.Respawn();
        GameOverMenu.SetActive(false);

    }

    public IEnumerator loadSpawnScene(string nameScene)
    {
        PlayerMove.instance.enabled = false;

        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(nameScene);

        PlayerMove.instance.enabled = true;
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
        //reset le hasSwitched de l'audio
        AudioManager.instance.hasSwitched = false;
    }

    public void QuitButton()
    {
        Application.Quit();
        Debug.Log("End");
    }

}
