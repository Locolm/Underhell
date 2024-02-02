using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    private Text interact;
    private bool isInRange;
    private bool isOpened=false;

    public Animator animator;
    public int coinsToAdd;
    public AudioSource SoundChest;


    void Awake()
    {
        interact = GameObject.FindGameObjectWithTag("interactUI").GetComponent<Text>();
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)&& isInRange && !isOpened)
        {
            OpenChest();
            isOpened = true;
            interact.enabled = false;
        }
    }

    void OpenChest()
    {
        animator.SetTrigger("OpenChest");
        Inventory.instance.AddCoins(coinsToAdd);
        SoundChest.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&&!isOpened)
        {
            interact.enabled = true;
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interact.enabled = false;
            isInRange = false;
        }
    }
}
