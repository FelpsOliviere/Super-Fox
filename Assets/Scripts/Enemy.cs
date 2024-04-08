using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject patrolPosition;
    [SerializeField] GameObject activatorCollider;
    [SerializeField] GameObject enemy;

    [SerializeField] BoxCollider2D hitCollider;
    [SerializeField] CircleCollider2D circleCollider2D;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] bool flipX;


    AudioController audioController;
    GameObject player;
    SpriteRenderer spriteRenderer;
    Vector3 patrolPivot;

    public bool moveArround;
    [SerializeField] bool willStandby;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] bool xy;

    [SerializeField] int timerTime = 3;

    public bool pursuit;
    public bool pounce;
    public bool pouncing;
    public bool shadow;
    public bool canPounce;

    float time = 0f;

    public bool changeSide;
    public int life = 3;

    public bool standby;
    public bool dead;

    public Transform patrolPoint1;
    public Transform patrolPoint2;

    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioController = FindObjectOfType<AudioController>();
        player = FindObjectOfType<PlayerMovement>().gameObject;
        patrolPivot = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (pursuit)
        {
            Pursuit(xy);
        }

        if (pounce)
        {
            Pounce();
        }

        Life();
        Hitbox();

        if (!dead)
        {
            if (!standby && !pursuit)
            {
                if (willStandby)
                {
                    MoveToPatrolPosition();
                }

                if (moveArround)
                {
                    if (changeSide)
                    {
                        Vector2 lookDirection = (patrolPoint2.position - transform.position).normalized;
                        transform.Translate(lookDirection * moveSpeed * Time.deltaTime);

                        if (transform.position.x >= patrolPoint2.position.x)
                        {
                            spriteRenderer.flipX = true;
                        }
                        else
                        {
                            spriteRenderer.flipX = false;
                        }
                    }

                    if (!changeSide)
                    {
                        Vector2 lookDirection2 = (patrolPoint1.position - transform.position).normalized;
                        transform.Translate(lookDirection2 * moveSpeed * Time.deltaTime);

                        if (transform.position.x <= patrolPoint1.position.x)
                        {
                            spriteRenderer.flipX = false;
                        }
                        else
                        {
                            spriteRenderer.flipX = true;
                        }

                    }

                    if (willStandby)
                    {
                        time += Time.deltaTime;
                        StartCoroutine(StandbyTimer(timerTime));
                    }
                    else
                    {
                        standby = false;
                    }
                }

                activatorCollider.SetActive(false);
                animator.SetBool("Standby", standby);
            }
            else if (standby && !pursuit)
            {
                time = 0;
                moveArround = false;

                BackToPosition();
            }
        }
    }

    public void Dead()
    {
        Destroy(enemy);
    }

    void Pursuit(bool xy)
    {
        Vector2 playerPos = (player.transform.position - transform.position).normalized;

        if (player.transform.position.x < transform.position.x && !xy)
        {
            transform.Translate(Vector2.left * moveSpeed * 2 * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.right * moveSpeed * 2 * Time.deltaTime);
        }

        if (xy)
        {
            transform.Translate(playerPos * moveSpeed * 2 * Time.deltaTime);
        }

        moveArround = false;

        if (transform.position.x >= player.transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    void Pounce()
    {
        Vector2 playerPos = (player.transform.position - transform.position).normalized;
        Vector2 patrolPivotLocation = (patrolPivot - transform.position).normalized;
        shadow = true;

        if (canPounce)
        {
            if (time > 2)
            {
                transform.Translate(playerPos * moveSpeed * 7 * Time.deltaTime);
                pouncing = true;
            }
            else
            {
                transform.position = new Vector2(playerPos.x, transform.position.y);
                time += Time.deltaTime;
                pouncing = false;
            }
        }
        else
        {
            transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);

            if (transform.position.y > patrolPivot.y)
            {
                canPounce = true;
            }
        }

        animator.SetBool("Pounce", pouncing);

        moveArround = false;

        if (transform.position.x >= player.transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    void MoveToPatrolPosition()
    {
        if (transform.position != patrolPosition.transform.position && !moveArround)
        {
            Vector2 lookDirection = (patrolPosition.transform.position - transform.position).normalized;
            transform.Translate(lookDirection * moveSpeed * Time.deltaTime);
            moveArround = false;

            if (transform.position.x < patrolPosition.transform.position.x + 0.1f)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
        }
        else
        {
            moveArround = true;
        }
    }

    void Hitbox()
    {
        if (player.transform.position.y > transform.position.y)
        {
            circleCollider2D.enabled = false;
            hitCollider.enabled = true;
        }
        else
        {
            circleCollider2D.enabled = true;
            hitCollider.enabled = false;
        }
    }

    void Life()
    {
        if (life <= 0 && !dead)
        {
            circleCollider2D.enabled = false;
            dead = true;
            audioController.enemyDeathSFX.Play(0);
            hitCollider.enabled = false;
            animator.SetTrigger("Dead");
        }
    }

    void BackToPosition()
    {
        Vector2 lookDirection = (patrolPivot - transform.position).normalized;

        if (transform.position != patrolPivot)
        {
            if (transform.position.x < patrolPivot.x + 0.1f)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }

            transform.Translate(lookDirection * moveSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("Standby", standby);
            activatorCollider.SetActive(true);
        }
    }

    IEnumerator StandbyTimer(int time)
    {
        yield return new WaitForSeconds(time);
        standby = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canPounce = false;
            time = 0;
        }
    }
}
