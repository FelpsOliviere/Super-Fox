using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 initialPos;

    private void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!GameManager.Instance.levelComplete || !GameManager.Instance.isDead)
        {
            if (player.transform.position.x < initialPos.x)
            {
                transform.position = new Vector3(initialPos.x, transform.position.y, transform.position.z);
            }
            else if (player.transform.position.y < initialPos.y - 1.5f)
            {
                transform.position = new Vector3(player.position.x, initialPos.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(player.position.x, player.position.y + 1f, transform.position.z);
            }
        }
    }
}
