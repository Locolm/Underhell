using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    public bool canMove = true;
    public Rigidbody2D myRigidbody;
    public float jumpStrength; //force du saut
    public float moveSpeed; //vitesse de déplacement horizontal

    private bool isJumping = false; //vérifie si le joueur peut sauter plutôt canjump, à changer
    [HideInInspector]
    public bool isClimbing = false;

    private float horizontalMovement; //déplacement horizontal du joueur
    private float verticalMovement;

    private Vector3 velocity = Vector3.zero;
    public int hp;

    public Animator animator;
    public Animator maskAnimator;
    public SpriteRenderer spriteRenderer;
    public CapsuleCollider2D playerColliderinteract;
    public CapsuleCollider2D playerColliderhitbox;


    public Transform playerSpawn;

    public float maxJumpForce;

    // Variables pour le système de gravité
    private float timeSinceLastGrounded = 0f;
    private float defaultGravityScale;
    public float gravityScaleMultiplier = 2f;
    public float maxTimeSinceLastGrounded = 1.5f;
    private float currentGravityScale=4.0f;

    //variables pour le coup d'épée
    public GameObject hitRight;
    public GameObject hitLeft;
    public float hitDuration = 0.15f;
    public float hitCooldown = 0.5f;

    private bool canFlip= true;

    private bool isHitOnCooldown = false;
    private bool isFacingRight = true;

    //relatif items
    public float CooldownItem = 0.1f;
    private bool isItemOnCooldown=false;

    //dash
    public float dashSpeed = 10f;
    public float dashDuration = 0.4f;
    public float dashCooldown = 1f;
    public TrailRenderer trailRenderer;
    [SerializeField] private bool canDash = true;
    private bool isDashing = false;

    //fire balls
    public Transform firePosition;
    public GameObject projectile;

    private void Dash()
    {
        isDashing = true;
        canDash = false;
        trailRenderer.emitting = true;
        Vector2 dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), 0f).normalized;
        if (dashDirection == Vector2.zero)
        {
            dashDirection = (isFacingRight ? Vector2.right : Vector2.left);
        }

        myRigidbody.velocity = dashDirection* dashSpeed;
        animator.SetBool("IsDashing", true);
        maskAnimator.SetBool("IsDashing", true);
        Invoke(nameof(StopDash), dashDuration);
        Invoke(nameof(ResetDash), dashCooldown);
    }

    private void StopDash()
    {
        isDashing = false;
        myRigidbody.velocity = Vector2.zero;
        trailRenderer.emitting = false;
        animator.SetBool("IsDashing", false);
        maskAnimator.SetBool("IsDashing", false);
    }

    private void ResetDash()
    {
        canDash = true;
        
    }

    public static PlayerMove instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerMove dans la scène");
            return;
        }

        instance = this;
    }

    void Start()
    {
        hp = 100;
        defaultGravityScale = myRigidbody.gravityScale;
    }

    void FixedUpdate()
    {
        maskAnimator.SetInteger("IdMask", Inventory.instance.Mask());
        if (canMove)
        {
            if (!isItemOnCooldown)
            {
                if (Input.GetKey(KeyCode.C)) //nextitem
                {
                    Inventory.instance.GetNextItem();
                }
                if (Input.GetKey(KeyCode.W)) //previousitem
                {
                    Inventory.instance.GetPreviousItem();
                }
                if (Input.GetKey(KeyCode.X)) //useitem
                {
                    Inventory.instance.ConsumeItem();
                }
                isItemOnCooldown = true;
                Invoke("ResetCooldownItem", CooldownItem);
            }

            if (canDash && (Input.GetKey(KeyCode.F)|| Input.GetKeyDown(KeyCode.F)))
            {
                Dash();
                DisableHit();
            }


            if (Input.GetKeyDown(KeyCode.Space))
            {
                EventSystem.current.SetSelectedGameObject(null); // Lâcher le focus des boutons
            }

            //les attaques:
            ThrowFireBall();

            if (Input.GetKey(KeyCode.T) && !isHitOnCooldown &&!isClimbing &&!isDashing)
            {
                canFlip = false;
                if (isFacingRight)
                {
                    hitRight.SetActive(true);
                }
                else
                {
                    hitLeft.SetActive(true);
                }

                animator.SetTrigger("Attack");
                maskAnimator.SetTrigger("Attack");

                Invoke("DisableHit", hitDuration);
                isHitOnCooldown = true;
                Invoke("ResetCooldown", hitCooldown);
            }

            currentGravityScale=4.0f;
            horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.fixedDeltaTime;
            verticalMovement = Input.GetAxis("Vertical") * moveSpeed*0.7f * Time.fixedDeltaTime;

            // Vérifier si le joueur est sur le sol
            if (isJumping == false)
            {
                timeSinceLastGrounded += Time.deltaTime;
                if (isClimbing)
                {
                    timeSinceLastGrounded = 0f;
                }
            }
            else
            {
                timeSinceLastGrounded = 0f;
            }
            if (timeSinceLastGrounded > maxTimeSinceLastGrounded)
            {
                float t = (timeSinceLastGrounded - maxTimeSinceLastGrounded) / (maxTimeSinceLastGrounded * (gravityScaleMultiplier - 1));
                currentGravityScale = defaultGravityScale + t * (defaultGravityScale * gravityScaleMultiplier - defaultGravityScale);
                if (currentGravityScale>20.0f){currentGravityScale=20.0f;}            
            }
            if (isClimbing)
                {
                currentGravityScale = 4.0f;
                }
            if (hp==0)
            {
                currentGravityScale = 0.0f;
            }
            myRigidbody.gravityScale = currentGravityScale;
            
            if (Input.GetButton("Jump") && isJumping&&!isClimbing)
            {
                isJumping = false;
                Vector2 jumpForce = new Vector2(0f, jumpStrength);
                if (jumpForce.magnitude > maxJumpForce) // Vérifier si la force du saut dépasse la limite maximale
                {
                    jumpForce = jumpForce.normalized * maxJumpForce; // Limiter la force du saut à la valeur maximale
                }
                myRigidbody.AddForce(jumpForce);
            }

            Move(horizontalMovement, verticalMovement);

            //envoie vitesse à l'animator

            Flip(myRigidbody.velocity.x);

            float characterVelocity = Mathf.Abs(myRigidbody.velocity.x);
            animator.SetFloat("Speed", characterVelocity);
            animator.SetBool("isClimbing", isClimbing);
            maskAnimator.SetFloat("Speed", characterVelocity);
            maskAnimator.SetBool("isClimbing", isClimbing);
        }
    }

    private void Move(float _horizontalMovement, float _verticalMovement)
    {
        if (hp > 0 && !isClimbing)
        {
            Vector3 targetVelocity = new Vector2(_horizontalMovement, myRigidbody.velocity.y);
            myRigidbody.velocity = Vector3.SmoothDamp(myRigidbody.velocity, targetVelocity, ref velocity, .05f);
        }
        else if(hp>0 && isClimbing)
        {
            Vector3 targetVelocity = new Vector2(0, _verticalMovement);
            myRigidbody.velocity = Vector3.SmoothDamp(myRigidbody.velocity, targetVelocity, ref velocity, .05f);
        }

    }

    private void DisableHit()
    {
        hitRight.SetActive(false);
        hitLeft.SetActive(false);
        canFlip = true;
    }

    private void ResetCooldown()
    {
        isHitOnCooldown = false;
    }

    private void ResetCooldownItem()
    {
        isItemOnCooldown = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
            myRigidbody.gravityScale = defaultGravityScale;
        }
        if (collision.gameObject.CompareTag("Mur"))
        {
            DisableHit();
        }

    }

    private void Flip(float _velocity)
    {
        if (canFlip)
        {
            if (_velocity > 0.1f)
            {
                spriteRenderer.flipX = false;
                isFacingRight = true;
                maskAnimator.SetBool("IsLookingR", isFacingRight);
            }
            else if (_velocity < -0.1f)
            {
                spriteRenderer.flipX = true;
                isFacingRight = false;
                maskAnimator.SetBool("IsLookingR", isFacingRight);
            }
        }
    }

    public int statusPlayer()
    {
        return hp;
    }

    public void ResetMovement()
    {
        // Remettre les mouvements horizontaux et verticaux à zéro
        horizontalMovement = 0f;
        verticalMovement = 0f;

        // Remettre la vitesse du Rigidbody à zéro
        myRigidbody.velocity = Vector3.zero;

        // Remettre la gravité du Rigidbody à sa valeur par défaut
        myRigidbody.gravityScale = defaultGravityScale;

        // Remettre isJumping et isClimbing à faux
        isJumping = false;
        isClimbing = false;
    }

    public void RespawnPlayer()
    {
        if (hp == 0)
        {
            playerSpawn.position = transform.position;
        }
    }

    public void ThrowFireBall()
    {
        if (Input.GetKey(KeyCode.R) && !isHitOnCooldown && !isClimbing && !isDashing && PlayerHealth.instance.UseMana(20, 2.0f))
        {
            // Calculer le décalage en fonction de isFacingRight
            float xOffset = isFacingRight ? 0.5f : -0.5f;

            // Créer le point de spawn du projectile en utilisant le décalage
            Vector3 spawnPosition = new Vector3(firePosition.position.x + xOffset, firePosition.position.y, firePosition.position.z);

            // Instancier le projectile à la nouvelle position
            GameObject newProjectile = Instantiate(projectile, spawnPosition, firePosition.rotation);

            // Obtenir le script Projectile du nouveau projectile
            Projectile projectileScript = newProjectile.GetComponent<Projectile>();

            // Déterminer la direction du projectile en fonction de isFacingRight
            Vector2 direction = isFacingRight ? Vector2.right : Vector2.left;

            // Appliquer la direction au projectile
            projectileScript.SetDirection(direction);

            isHitOnCooldown = true;
            Invoke("ResetCooldown", hitCooldown);
        }
    }
}