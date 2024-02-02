using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weakSpot : MonoBehaviour
{
    public GameObject objectToDestroy;
    public int hp = 1;
    public AudioSource OuchMonster;
    public AudioSource MortMonster;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHitBox"))
        {
            hp = hp - 1;
            Debug.Log("hit"+ hp.ToString());
            OuchMonster.Play();
            if (hp <= 0)
            {
                /*Destroy(transform.parent.parent.gameObject);*/
                Destroy(objectToDestroy);
                MortMonster.Play();
            }
        }
        if (collision.CompareTag("Sword"))
        {
            hp = hp - 7;
            Debug.Log("hit" + hp.ToString());
            OuchMonster.Play();
            if (hp <= 0)
            {
                /*Destroy(transform.parent.parent.gameObject);*/
                Destroy(objectToDestroy);
                MortMonster.Play();
            }
        }
    }
}
