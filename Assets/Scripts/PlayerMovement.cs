using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] BoxCollider2D crouchedBoxCollider;
    [SerializeField] CapsuleCollider2D capsuleCollider;

    [SerializeField] AudioSource jumpAudioSource;
    [SerializeField] AudioSource hitAudioSource;
    [SerializeField] AudioSource deathAudioSource;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Animator animator;

    Vector2 movementDirection;

    [SerializeField] int alpha;

    [SerializeField] bool isGrounded;
    [SerializeField] bool canClimb;


    float moveSpeed = 12f;
    float fallingVelocity = 0.3f;
    float jumpHeight = 14f;
    float flipOffset = 0.1f;
    float horizontalInput;
    float verticalInput;

    bool canJump;
    bool canMove;
    bool canCrouch;
    bool isCrouched;
    bool isClimbing;
    [SerializeField] bool canMove2;

    public int life;
    public int cherrys;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        canMove2 = true;

        cherrys = GameManager.Instance.cherrys;
        life = GameManager.Instance.lifes;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (canMove2)
        {
            Movement();
            Jump();
            Crouch();
            Climb();
            TakeDamage();
        }

        if (GameManager.Instance.levelComplete)
        {
            LevelComplete();
        }
        Debug.Log(rb.velocity.x + " " + rb.velocity.y);
        IsDead(GameManager.Instance.isDead);
    }

    public void LevelComplete()
    {
        rb.bodyType = RigidbodyType2D.Static;
        animator.SetFloat("Idle_Run", 1);
        animator.SetBool("Falling", false);
        animator.SetBool("Jumping", false);
        spriteRenderer.flipX = false;
        transform.Translate(Vector2.right * Time.deltaTime * moveSpeed);
    }

    void IsDead(bool isDead)
    {
        if (isDead)
        {
            canMove2 = false;
            animator.SetTrigger("Dead");
            capsuleCollider.enabled = false;
            crouchedBoxCollider.enabled = false;
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
    }

    void Climb()
    {
        if (Input.GetKeyDown(KeyCode.W) && canClimb)
        {
            isClimbing = true;
        }

        if (isClimbing)
        {
            movementDirection = new Vector2(horizontalInput, verticalInput);
            transform.Translate(movementDirection * moveSpeed * Time.deltaTime);

            rb.gravityScale = 0;
            rb.velocity = new Vector2(horizontalInput * moveSpeed * Time.deltaTime, 0);

            if (horizontalInput > flipOffset)
            {
                spriteRenderer.flipX = false;
            }
            else if (horizontalInput < -flipOffset)
            {
                spriteRenderer.flipX = true;
            }

            if (movementDirection == Vector2.zero)
            {
                animator.SetFloat("Animation_Speed", 0);
            }
            else
            {
                animator.SetFloat("Animation_Speed", 1);
            }

            canMove = false;

            // Need to make a code who check if the player is climbing a bindweeb and change his movement type
        }
        else
        {
            if (!isCrouched)
            {
                canMove = true;
            }

            rb.gravityScale = 3;
        }

        animator.SetBool("Climbing", isClimbing);
    }

    void Movement()
    {
        if (canMove)
        {
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

            if (rb.velocity.x > flipOffset)
            {
                spriteRenderer.flipX = false;
                animator.SetFloat("Idle_Run", rb.velocity.x);
            }
            else if (rb.velocity.x < -flipOffset)
            {
                spriteRenderer.flipX = true;
                animator.SetFloat("Idle_Run", -rb.velocity.x);
            }
            else
            {
                animator.SetFloat("Idle_Run", rb.velocity.x);
            }
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && canJump || Input.GetButtonDown("Jump") && isClimbing)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight + rb.velocity.y);
            jumpAudioSource.Play(0);
            canJump = false;
            canCrouch = false;
            isClimbing = false;
        }

        if (rb.velocity.y > fallingVelocity)
        {
            animator.SetBool("Jumping", true);
            animator.SetBool("Falling", false);
        }
        else if (rb.velocity.y < -fallingVelocity)
        {
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", true);
        }
        else
        {
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", false);
        }
    }

    void Crouch()
    {
        if (canCrouch && !isClimbing)
        {
            if (Input.GetKey(KeyCode.S))
            {
                capsuleCollider.enabled = false;
                crouchedBoxCollider.enabled = true;
                canMove = false;
                canJump = false;
                isCrouched = true;
            }
            else
            {
                capsuleCollider.enabled = true;
                crouchedBoxCollider.enabled = false;
                canMove = true;
                canJump = true;
                isCrouched = false;
            }

            animator.SetBool("Crouch", isCrouched);
        }
    }

    void TakeDamage()
    {
        switch (alpha)
        {
            case 1:
                LeanTween.alpha(gameObject, .3f, 0.3f);
                break;
            case 2:
                LeanTween.alpha(gameObject, 1, 0.3f);
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            canJump = true;
            canCrouch = true;
            isClimbing = false;
            canClimb = false;
        }

        if (collision.gameObject.CompareTag("WinPoint"))
        {
            canMove2 = false;
            rb.velocity = Vector2.zero;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bindweed")/* && !canClimb*/)
        {
            canClimb = true;
        }

        if (collision.gameObject.CompareTag("Cherry"))
        {
            GameManager.Instance.cherrys += collision.GetComponent<Cherry>().cherrys;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!GameManager.Instance.gameOver)
            {
                deathAudioSource.Play();
            }
            animator.SetTrigger("Hit");
            rb.velocity = new Vector2(0, 14);
            GameManager.Instance.lifes--;
            GameManager.Instance.isDead = true;
        }

        if (collision.gameObject.CompareTag("OutOfWorld"))
        {
            if (!GameManager.Instance.gameOver)
            {
                deathAudioSource.Play();
            }
            rb.velocity = new Vector2(0, 14);
            GameManager.Instance.lifes--;
            GameManager.Instance.isDead = true;
        }

        if (collision.name == "Hitbox")
        {
            rb.velocity = new Vector2(rb.velocity.x, 14);
            //GameManager.Instance.score += collision.GetComponent<Score>().score;
            hitAudioSource.Play(0);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bindweed") && canClimb)
        {
            isClimbing = false;
            canClimb = false;
        }
    }
}