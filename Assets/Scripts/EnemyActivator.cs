using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivator : MonoBehaviour
{
    public Enemy enemyScript;
    [SerializeField] bool pursuit;

    private void Start()
    {
        enemyScript = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemyScript.standby = false;

            if (pursuit)
            {
                enemyScript.moveArround = false;
                enemyScript.pursuit = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (pursuit)
            {
                enemyScript.moveArround = true;
                enemyScript.pursuit = false;
            }
        }
    }
}
