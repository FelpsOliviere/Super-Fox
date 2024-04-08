using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    [SerializeField] Enemy enemyScript;
    [SerializeField] GameObject player;
    SpriteRenderer spriteRenderer;
    bool shadow;
    float distance;
    public bool startFollow;

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>().gameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();

        shadow = enemyScript.shadow;
    }

    // Update is called once per frame
    void Update()
    {
        if (startFollow)
        {
            transform.position = new Vector2(player.transform.position.x, player.transform.position.y - 1f);
        }

        distance = Vector2.Distance(enemyScript.gameObject.transform.position, transform.position);

        float enemyDistance = 1 / (distance + 1);

        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, enemyDistance);
    }
}
