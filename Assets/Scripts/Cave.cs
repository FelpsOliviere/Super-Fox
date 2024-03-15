using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Cave : MonoBehaviour
{
    [SerializeField] Transform exitPos;
    [SerializeField] GameplayUI gameplayUI;

    float time;

    // Start is called before the first frame update
    void Start()
    {
        gameplayUI = FindObjectOfType<GameplayUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            gameplayUI.visible = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            time += Time.deltaTime;

            if (time >= 1)
            {
                collision.transform.position = exitPos.position;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            gameplayUI.visible = false;
            time = 0;
        }
    }
}
