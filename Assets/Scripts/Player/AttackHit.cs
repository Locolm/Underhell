using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHit : MonoBehaviour
{
    public bool useDamagePlayer=true;
    public bool affectPlayer = false;
    public int damage;
    public bool affectEnemy = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (useDamagePlayer)
        {
            damage = PlayerHealth.instance.damageSword; // Dégâts à infliger
        }
        if (affectPlayer && collision.CompareTag("Player"))
        {
            PlayerHealth.instance.TakeDamage(damage);
        }
        if (collision.CompareTag("Enemy")&& affectEnemy)
        {
            Debug.Log("boom");
            // Référence à l'objet enemy
            Enemy enemy = collision.GetComponent<Enemy>();

            // Vérifie si l'objet enemy a un script Enemy attaché
            if (enemy != null)
            {
                // Infliger les dégâts à l'enemy
                enemy.TakeDamage(damage);
            }
            else {
                BossHealth boss = collision.GetComponent<BossHealth>();
                Debug.Log("on tape le boss");
                if (boss != null)
                {
                    boss.TakeDamage(damage);
                }
            }
        }
    }

    public void EndAnimationDestroy()
    {
        Destroy(gameObject);
    }
}

