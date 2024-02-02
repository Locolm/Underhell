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
            // R�f�rence � l'objet enemy
            Enemy enemy = collision.GetComponent<Enemy>();

            // V�rifie si l'objet enemy a un script Enemy attach�
            if (enemy != null)
            {
                // Infliger les d�g�ts � l'enemy
                enemy.TakeDamage(damage);
            }
            else
            {
                // R�f�rence � l'objet boss
                BossHealth boss = collision.GetComponent<BossHealth>();

                // V�rifie si l'objet boss a un script BossHealth attach�
                if (boss != null)
                {
                    // Infliger les d�g�ts au boss
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
