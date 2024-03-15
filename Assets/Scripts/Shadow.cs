using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    [SerializeField] Enemy enemyScript;
    [SerializeField] GameObject player;
    SpriteRenderer spriteRenderer;
    bool shadow;

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
        if (shadow)
        {
            transform.position = new Vector2(player.transform.position.x, player.transform.position.y - 1f);
        }

        Vector2 enemyPos = (enemyScript.gameObject.transform.position - transform.position).normalized;

        float enemyDistance = enemyPos.magnitude;
        Debug.Log(enemyDistance);

        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, enemyDistance);
    }
}
