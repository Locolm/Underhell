using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentSceneManager : MonoBehaviour
{
    public bool inGame=true;

    public Vector3 respawnPoint;

    public bool switchMusic;
    public int[] indexMusic;

    public static CurrentSceneManager instance;

    public int levelToUnlock;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de CurrentSceneManager dans la sc�ne");
            return;
        }

        instance = this;
        if (inGame)
        {
            respawnPoint = GameObject.FindGameObjectWithTag("Player").transform.position;
        }
    }
}
