using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopTrigger : MonoBehaviour
{
    public string pnjName;
    public Item[] itemsToSell;
    
    private bool isInRange;
    private bool isTalking;
    public Animator animator;
    private Text interact;

    private void Awake()
    {
        interact = GameObject.FindGameObjectWithTag("interactUI").GetComponent<Text>();
        animator = GameObject.FindGameObjectWithTag("DialogueUI").GetComponent<Animator>();
        isTalking = false;
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E) && !isTalking)
        {
            PauseMenu.instance.enabled=false;
            ShopManager.instance.OpenShop(itemsToSell,pnjName);
            isTalking = true;
            interact.enabled = false;
            PlayerMove.instance.animator.SetFloat("Speed", 0);
            PlayerMove.instance.enabled = false;
        }
        if (isInRange && Input.GetKeyDown(KeyCode.Space)&& isTalking)
        {
            ShopManager.instance.CloseShop();
        }
        
        if(animator.GetBool("IsOpen")==false)
        {
            isTalking = false;
            PlayerMove.instance.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
            if (!isTalking)
            {
                interact.enabled = true;
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("IsOpen",false);
            isInRange = false;
            interact.enabled = false;
            ShopManager.instance.CloseShop();
        }
    }
}
