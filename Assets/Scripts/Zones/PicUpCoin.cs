using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicUpCoin : MonoBehaviour
{
    public AudioSource CoinSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Inventory.instance.AddCoins(1);
            Destroy(gameObject);
            CoinSound.Play();
        }
    }
}
