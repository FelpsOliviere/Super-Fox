using System.Collections;
using System.Collections.Generic;
using System.Globalization;

using Unity.VisualScripting;
using UnityEngine;

public class TutorialCharacter : MonoBehaviour
{
    [SerializeField] bool jump;
    [SerializeField] bool move;
    [SerializeField] bool crouch;
    [SerializeField] bool run;

    [SerializeField] Transform posPoint1;
    [SerializeField] Transform posPoint2;

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    int index;
    float fallingVelocity = 0.3f;
    float moveSpeed = 7f;
    float timer;

    Vector3 initalPos;

    bool changeSide;
    bool canCrouch;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        initalPos = new Vector3(transform.position.x, transform.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (jump)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            //boxCollider.enabled = true;
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Static;
            //boxCollider.enabled = false;
        }

        if (!jump && !move && !crouch)
        {
            transform.position = initalPos;
            animator.SetFloat("Idle_Run", 0);
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", false);
            animator.SetBool("Crouch", false);
        }

        Jump();
        Move();
        Crouch();
        Run();
    }

    void Jump()
    {
        if (jump && index == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 14f);
            index = 1;
        }

        if (transform.position.y <= initalPos.y)
        {
            index = 0;
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

    void Move()
    {
        if (move)
        {
            if (changeSide)
            {
                Vector2 lookDirection = (posPoint1.position - transform.position).normalized;
                transform.Translate(lookDirection * moveSpeed * Time.deltaTime);

                spriteRenderer.flipX = true;
            }

            if (!changeSide)
            {
                Vector2 lookDirection2 = (posPoint2.position - transform.position).normalized;
                transform.Translate(lookDirection2 * moveSpeed * Time.deltaTime);

                spriteRenderer.flipX = false;
            }

            if (transform.position.x <= posPoint1.position.x && changeSide)
            {
                changeSide = false;
            }
            else if (transform.position.x >= posPoint2.position.x && !changeSide)
            {
                changeSide = true;
            }

            animator.SetFloat("Idle_Run", 1);
        }
    }

    void Crouch()
    {
        if (crouch)
        {
            timer += Time.deltaTime;

            if (timer >= 1 && canCrouch)
            {
                timer = 0;
                canCrouch = false;
            }
            else if (timer >= 1 && !canCrouch)
            {
                timer = 0;
                canCrouch = true;
            }

            animator.SetBool("Crouch", canCrouch);
        }
    }

    void Run()
    {
        if (run)
        {
            moveSpeed = 12f;
            animator.SetFloat("Animation_Speed", 2);
        }
        else
        {
            moveSpeed = 7f;
            animator.SetFloat("Animation_Speed", 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && jump)
        {
            //index = 0;
        }
    }
}
