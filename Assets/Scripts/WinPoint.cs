using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPoint : MonoBehaviour
{
    AudioSource levelComplete;

    // Start is called before the first frame update
    void Start()
    {
        levelComplete = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            levelComplete.Play();
            GameManager.Instance.LevelComplete();
            GameManager.Instance.levelComplete = true;
        }
    }
}
