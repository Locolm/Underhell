using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatern : MonoBehaviour
{
    public int damage = 29;
    public int id = 0;
    public GameObject hitLeft;
    public GameObject hitRight;

    public Animator animator;

    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public GameObject player; // Référence au joueur

    public bool isFacingLeft = true;
    public bool canFlip = true;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (animator != null)
        {
            animator.SetInteger("Id", id);
            animator.SetBool("IsFacingR", !isFacingLeft);
        }
        else
        {
            Debug.LogError("Animator n'est pas défini. Assurez-vous de le faire dans l'inspecteur Unity.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canFlip)
        {
            Flip(player.transform.position.x - transform.position.x);
            //Debug.Log((player.transform.position.x - transform.position.x).ToString());
        }
    }

    private void Flip(float distanceFromPlayer)
    {
        if (distanceFromPlayer < -0.01f && isFacingLeft)
        {
            if (id==0)
            {
                spriteRenderer.flipX = false;
            }
            if (id == 11)
            {
                spriteRenderer.flipX = true;
            }
            isFacingLeft = false;
        }
        else if (distanceFromPlayer > 0.01f && !isFacingLeft)
        {
            if (id == 0)
            {
                spriteRenderer.flipX = true;
            }
            if (id == 11)
            {
                spriteRenderer.flipX = false;
            }
            isFacingLeft = true;
        }
        if (id != 11)
        {
            isFacingLeft = !isFacingLeft;
        }
        animator.SetBool("IsFacingR", isFacingLeft);
    }

    public void Attack()
    {
        canFlip = false;
        if (!isFacingLeft)
        {
            hitLeft.SetActive(true);
        }
        if (isFacingLeft)
        {
            hitRight.SetActive(true);
        }
    }

    public void EndAttack()
    {
        hitLeft.SetActive(false);
        hitRight.SetActive(false);
        canFlip = true;
    }

    public void Attack_Phase2()
    {
        canFlip = false;
        if (!isFacingLeft)
        {
            hitLeft.SetActive(true);
        }
        if (isFacingLeft)
        {
            hitRight.SetActive(true);
        }
    }
}
