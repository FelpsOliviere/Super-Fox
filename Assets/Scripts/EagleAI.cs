using UnityEngine;
using System;

public class EagleAI : MonoBehaviour
{
    public enum State { Patrolling, Chasing, Attacking, Recovering }

    public State currentState;
    public Transform playerTransform;
    public float patrolSpeed;
    public float chaseSpeed;
    public float attackRange;
    public float attackCooldown;
    //PlayerMovement player;

    private Vector3 patrolPoint;
    private float timeSinceLastAttack;

    void Start()
    {
        currentState = State.Patrolling;
        patrolPoint = transform.position;
        //player = FindAnyObjectByType<PlayerMovement>();
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Patrolling:
                Patrol();
                break;
            case State.Chasing:
                Chase();
                break;
            case State.Attacking:
                Attack();
                break;
            case State.Recovering:
                Recover();
                break;
        }
    }

    void Patrol()
    {
        if (Vector3.Distance(transform.position, patrolPoint) < 0.5f)
        {
            patrolPoint = UnityEngine.Random.insideUnitSphere * 10 + transform.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, patrolPoint, patrolSpeed * Time.deltaTime);

        if (Vector3.Distance(playerTransform.position, transform.position) < attackRange)
        {
            currentState = State.Chasing;
        }
    }

    void Chase()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, chaseSpeed * Time.deltaTime);

        if (Vector3.Distance(playerTransform.position, transform.position) > attackRange)
        {
            currentState = State.Patrolling;
        }

        if (CanAttack())
        {
            currentState = State.Attacking;
        }
    }

    public bool CanAttack()
    {
        // Verifique se o jogador está na frente da águia
        if (Vector3.Dot(transform.forward, playerTransform.position - transform.position) < 0)
        {
            return false;
        }

        // Verifique se há obstáculos entre a águia e o jogador
        RaycastHit hit;
        if (Physics.Raycast(transform.position, playerTransform.position - transform.position, out hit, attackRange))
        {
            if (hit.collider.gameObject != playerTransform.gameObject)
            {
                return false;
            }
        }

        return true;
    }

    void Attack()
    {
        // Ataque da águia

        timeSinceLastAttack = 0;
        currentState = State.Recovering;
    }

    void Recover()
    {
        timeSinceLastAttack += Time.deltaTime;

        if (timeSinceLastAttack > attackCooldown)
        {
            currentState = State.Patrolling;
        }
    }
}