using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pounce : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] Shadow shadow;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemy.pounce = true;
            shadow.startFollow = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemy.pounce = false;
            shadow.startFollow = false;
        }
    }
}
