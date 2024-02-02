using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiviteBoss : MonoBehaviour
{
    public GameObject[] doors = new GameObject[4];

    public Animator animator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            doors[0].SetActive(true);
            doors[1].SetActive(false);
            doors[2].SetActive(true);
            doors[3].SetActive(false);
            animator.SetTrigger("BossActivated");
            Destroy(gameObject);
        }
    }
}
