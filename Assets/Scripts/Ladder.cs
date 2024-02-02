using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ladder : MonoBehaviour
{

    private bool isInRange;
    private PlayerMove playerMove;
    public BoxCollider2D collider;
    private Text interactUI;

    void Awake()
    {
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        interactUI = GameObject.FindGameObjectWithTag("interactUI").GetComponent<Text>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(isInRange && Input.GetKeyDown(KeyCode.E))
        {
            playerMove.isClimbing = !playerMove.isClimbing;
            collider.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
            interactUI.enabled=true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
            playerMove.isClimbing = false;
            collider.isTrigger = false;
            interactUI.enabled=false;
        }
    }
}
