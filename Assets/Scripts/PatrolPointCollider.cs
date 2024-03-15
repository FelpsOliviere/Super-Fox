using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPointCollider : MonoBehaviour
{
    CircleCollider2D circleCollider;
    Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == enemy.patrolPoint1.gameObject)
        {
            enemy.changeSide = true;
        }

        if (collision.gameObject == enemy.patrolPoint2.gameObject)
        {
            enemy.changeSide = false;
        }
    }
}
