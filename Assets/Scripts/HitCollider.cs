using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour
{
    Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
       enemy = GetComponentInParent<Enemy>(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemy.life--;
        }
    }
}
