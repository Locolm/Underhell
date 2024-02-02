using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;
    private Rigidbody2D rgb;
    public GameObject ImpactEffect;

    // Méthode pour initialiser la direction du projectile
    public void SetDirection(Vector2 direction)
    {
        rgb = GetComponent<Rigidbody2D>();
        rgb.velocity = direction.normalized * projectileSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Vérifier si la collision n'est pas avec le joueur
        if (!collision.gameObject.CompareTag("Player"))
        {
            // Instancier l'effet d'impact
            Instantiate(ImpactEffect, transform.position, Quaternion.identity);

            // Détruire le projectile
            Destroy(gameObject);
        }
    }
}
