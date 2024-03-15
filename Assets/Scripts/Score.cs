using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score;
    [SerializeField] GameObject scoreCanva;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] float speed;
    [SerializeField] bool stopFollow;
    [SerializeField] float alpha;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopFollow)
        {
            scoreCanva.transform.position = transform.position;
            scoreText.alpha = 0;
        }
        else
        {
            alpha += Time.deltaTime;
            scoreText.alpha = alpha;
            scoreCanva.transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            stopFollow = true;
            GameManager.Instance.score += score;
            StartCoroutine(Timer(1));
        }
    }

    IEnumerator Timer(int time)
    {
        yield return new WaitForSeconds(time);
        stopFollow = false;
        StopAllCoroutines();
    }

}
