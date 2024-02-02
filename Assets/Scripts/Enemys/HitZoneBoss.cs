using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitZoneBoss : MonoBehaviour
{

    public int damage;
    public GameObject SelfHitZone;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SelfHitZone.SetActive(false);
            PlayerHealth.instance.TakeDamage(damage);
        }
    }
}
