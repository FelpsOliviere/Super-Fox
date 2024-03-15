using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : MonoBehaviour
{
    AudioSource cherrySFX;
    public int cherrys = 1;

    // Start is called before the first frame update
    void Start()
    {
        cherrySFX = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cherrySFX.Play(0);
            StartCoroutine(Timer(0.3f));
        }
    }

    IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
