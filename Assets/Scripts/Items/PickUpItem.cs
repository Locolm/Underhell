using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpItem : MonoBehaviour
{
    private Text interact;
    private bool isInRange;

    public Item item;
    public AudioSource SoundPick;


    void Awake()
    {
        interact = GameObject.FindGameObjectWithTag("interactUI").GetComponent<Text>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInRange)
        {
            TakeItem();
        }
    }

    void TakeItem()
    {
        Inventory.instance.AddToContent(item);
        //Inventory.instance.SortContent();
        Inventory.instance.UpdateInventoryUI();
        SoundPick.Play();
        interact.enabled = false;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
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
