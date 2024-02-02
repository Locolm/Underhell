using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public int maxMana = 100;
    public int currentMana;

    public int damageSword = 6;

    public float invincibilityFlashDelay = 0.2f;
    public float invincibilityDelay = 1.2f;
    public bool isInvincible = false;

    public float spellFlashDelay = 0.2f;
    //public float spellDelay = 2.0f;
    public bool canCast = true;

    public SpriteRenderer graphics;
    public HealthBar healthBar;
    public ManaBar manaBar;

    public AudioSource Ouch;

    public static PlayerHealth instance;

    private void Awake()
    {
        if (instance!=null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerHealth dans la scène");
            return;
        }

        instance=this;
    }
    //Pour utiliser des méthodes de PlayerHealth:
    //PlayerHealth.instance.nomdelaméthode();


    // Start is called before the first frame update
    /*    void Start()
        {
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
            currentMana= maxMana;
            manaBar.SetMaxMana(maxMana);
        }*/

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            TakeDamage(10);
            GainMana(10);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (Inventory.instance.coinsCount>=10)
            {
                Inventory.instance.RemoveCoins(10);
                GainHealth(10);
                UseMana(5, 1.0f);
            }
            
        }
    }

    public bool UseMana(int mana, float spellDelay)
    {
        if (currentMana-mana>=0 && canCast)
        {
            currentMana -= mana;
            graphics.color = new Color(0f, 0f, 0.8f, 1f); //bleu foncé
            manaBar.SetMana(currentMana);
            canCast = false;
            StartCoroutine(CastFlash());
            StartCoroutine(HandlerCanCastDelay(spellDelay));
            return true;
        }
        else { return false; }
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                healthBar.SetHealth(currentHealth);
                Die();
                return;
            }
            Ouch.Play();
            graphics.color = new Color(0.8f, 0f, 0f, 1f); //rouge foncé
            healthBar.SetHealth(currentHealth);
            isInvincible = true;
            StartCoroutine(InvicibilityFlash());
            StartCoroutine(HandlerInvincibilityDelay());
        }
    }

    public void Die()
    {
        Debug.Log("Le joueur est mort");
        //bloquer déplacements
        PlayerMove.instance.hp = 0;
        //PlayerMove.instance.enabled = false;
        PlayerMove.instance.ResetMovement();
        //jouer l'animation de la mort
        PlayerMove.instance.animator.SetTrigger("Death");
        //empêcher les interactions physique avec les autres élément de la scène
        PlayerMove.instance.playerColliderinteract.enabled = false;
        PlayerMove.instance.playerColliderhitbox.enabled = false;
        PlayerMove.instance.myRigidbody.gravityScale = 0.0f;
        GameOver.instance.OnPlayerDeath();

    }

    public void Respawn()
    {
        Debug.Log("Le joueur est Re-vivant");
        Inventory.instance.ResetCoins();
        PlayerMove.instance.hp = maxHealth;
        currentHealth = maxHealth;
        currentMana = maxMana;
        healthBar.SetHealth(currentHealth);
        PlayerMove.instance.animator.SetTrigger("Respawn");
        PlayerMove.instance.playerColliderinteract.enabled = true;
        PlayerMove.instance.playerColliderhitbox.enabled = true;
        PlayerMove.instance.myRigidbody.gravityScale = 4.0f;
        GameOver.instance.OnPlayerDeath();
    }


    public void GainHealth(int heal)
    {
        currentHealth += heal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.SetHealth(currentHealth);
    }

    public void GainMana(int mana)
    {
        currentMana += mana;
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
        manaBar.SetMana(currentMana);
    }

    public IEnumerator InvicibilityFlash()
    {
        while (isInvincible)
        {
            yield return new WaitForSeconds(invincibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invincibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invincibilityFlashDelay);
        }
    }

    public IEnumerator HandlerInvincibilityDelay()
    {
        yield return new WaitForSeconds(invincibilityDelay);
        isInvincible = false;
    }

    public IEnumerator CastFlash()
    {
        while (!canCast)
        {
            yield return new WaitForSeconds(spellFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(spellFlashDelay);
        }
    }

    public IEnumerator HandlerCanCastDelay(float spellDelay)
    {
        yield return new WaitForSeconds(spellDelay);
        canCast = true;
    }
}
