using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHit : MonoBehaviour
{
    public int damage;
    public bool affectPlayer=false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Référence à l'objet enemy
            Enemy enemy = collision.GetComponent<Enemy>();

            // Vérifie si l'objet enemy a un script Enemy attaché
            if (enemy != null)
            {
                // Infliger les dégâts à l'enemy
                enemy.TakeDamage(damage);
            }
            else
            {
                // Référence à l'objet boss
                BossHealth boss = collision.GetComponent<BossHealth>();

                // Vérifie si l'objet boss a un script BossHealth attaché
                if (boss != null)
                {
                    // Infliger les dégâts au boss
                    boss.TakeDamage(damage);
                }
            }
        }
        else if (collision.CompareTag("Player")&&affectPlayer)
        {
            PlayerHealth.instance.TakeDamage(damage);
        }
    }
}
