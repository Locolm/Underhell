using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    private bool isInRange;
    private bool isTalking;
    public Animator animator;
    private Text interact;

    public static DialogueTrigger instance;

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
            TriggerDialogue();
            isTalking = true;
            interact.enabled = false;
            PlayerMove.instance.animator.SetFloat("Speed", 0);
            PlayerMove.instance.enabled = false;
        }
        if (isInRange && Input.GetKeyDown(KeyCode.Space))
        {
            DialogueManager.instance.DisplayNextSentence();
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
        }
    }

    void TriggerDialogue()
    {
        DialogueManager.instance.StartDialogue(dialogue);
    }
}
