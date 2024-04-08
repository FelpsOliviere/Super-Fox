using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesativeCollider : MonoBehaviour
{
    BoxCollider2D boxCollider2D;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        player = FindAnyObjectByType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y > transform.position.y + 1)
        {
            boxCollider2D.enabled = true;
        }
        else
        {
            boxCollider2D.enabled = false;
        }
    }
}
