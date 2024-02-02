using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int health = 150;

    public int damageOnCollision = 9;

    public bool isInvulnerable = false;

    public GameObject objectBoss;

    public int MidLife=80;

    public Animator animator;

    public GameObject[] doors = new GameObject[4];


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth.instance.TakeDamage(damageOnCollision);
            //Debug.Log("coup corps à corps");
        }
    }

    public void TakeDamage(int damageSword)
    {
        if (!isInvulnerable)
        {
            isInvulnerable = true;
            StartCoroutine(ResetInvulnerability());

            health = health - damageSword;

            Debug.Log("Pv Boss restant : "+ health.ToString());
            if (health<=MidLife)
            {
                animator.SetTrigger("IsMidLife");
            }
            if (health<=0)
            {
                objectBoss.SetActive(false);
                doors[0].SetActive(false);
                doors[1].SetActive(true);
                doors[2].SetActive(false);
                doors[3].SetActive(true);
            }
        }
        else{
            Debug.Log("le boss est invulnérable!!!!!!!!!!!!!!!!!! : "+ health.ToString());
        }

    }

    private IEnumerator ResetInvulnerability()
    {
        yield return new WaitForSeconds(0.5f);
        isInvulnerable = false;
    }

}
