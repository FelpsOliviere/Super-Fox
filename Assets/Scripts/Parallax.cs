using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] Vector3 startPos;
    private float repeatWidth;
    [SerializeField] float moveSpeed;
    [SerializeField] float playerMoveSpeed;
    [SerializeField] Rigidbody2D player;
    [SerializeField] bool sky;
    [SerializeField] float horizontalParalaxSpeed;
    [SerializeField] float verticalParalaxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider2D>().size.x / 10;
    }

    // Update is called once per frame
    void Update()
    {
        playerMoveSpeed = player.velocity.x / horizontalParalaxSpeed;

        //ParalaxY();
        if (!sky)
        {
            ParalaxX();
        }
        Sky();
    }

    void ParalaxX()
    {
        if (transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }
        else if (transform.position.x > startPos.x + repeatWidth)
        {
            transform.position = startPos;
        }

        if (player.transform.position.x > startPos.x)
        {
            transform.position = new Vector3(transform.position.x - playerMoveSpeed * Time.deltaTime, transform.position.y , transform.position.z); // Ajustar a posição diretamente
        }
    }

    void ParalaxY()
    {
        if (transform.position.y < startPos.y - 1)
        {
            transform.position = startPos;
        }
        else if (transform.position.y > startPos.y + 1)
        {
            transform.position = startPos;
        }

        //transform.position = new Vector3(transform.position.x, (transform.position.y + player.transform.position.y) * verticalParalaxSpeed, transform.position.z); // Ajustar a posição diretamente
        if (player.transform.position.y > startPos.y)
        {
        }
    }

    void Sky()
    {
        if (sky)
        {
            transform.position = new Vector3(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y, transform.position.z); // Ajustar a posição diretamente
        }
        else
        {
            // ... outras partes do código ...
            if (player.transform.position.x > startPos.x)
            {
                transform.position = new Vector3(transform.position.x - playerMoveSpeed * Time.deltaTime, transform.position.y, transform.position.z); // Ajustar a posição diretamente
            }
        }
    }
}
