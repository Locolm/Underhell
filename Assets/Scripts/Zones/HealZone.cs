using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealZone : MonoBehaviour
{
    public int gain;
    public PlayerHealth playerHealth;

    private Text interact;

    private bool isInteracting = false;

    private bool isInRange = false;

    void Awake()
    {
        interact = GameObject.FindGameObjectWithTag("interactUI").GetComponent<Text>();
    }

    public void Update()
    {
        if(isInRange && Input.GetKeyDown(KeyCode.E) && !isInteracting)
        {
            LoadAndSaveData.instance.SavePlayerData();
            SceneManager.LoadScene("LevelSelect");
            //reset le hasSwitched de l'audio
            AudioManager.instance.hasSwitched = false;
        }
        else if(isInRange)
        {
            LoadAndSaveData.instance.SavePlayerData();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
            InvokeRepeating("HealPlayer", 0.25f, 0.25f); // Appeler HealPlayer toutes les secondes
            if (!isInteracting)
            {
                interact.enabled = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
            CancelInvoke("HealPlayer"); // Annuler les appels de HealPlayer
            isInteracting = false;
            interact.enabled = false;
        }
    }

    private void HealPlayer()
    {
        playerHealth.GainHealth(gain);
    }
}
