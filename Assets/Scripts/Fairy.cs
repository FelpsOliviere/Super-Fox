using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Fairy : MonoBehaviour
{
    public bool startCount;
    public float timer = 10f;
    [SerializeField] Light2D light2d;
    [SerializeField] Light2D light2dOuter;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        startCount = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (startCount)
        {
            time += Time.deltaTime;
        }

        if (time > 5f)
        {
            // Calcular o tempo normalizado (entre 0 e 1) para o lerp
            float lerpTime = Mathf.Clamp01(Time.deltaTime / (timer - 5f));

            light2d.intensity = Mathf.Lerp(light2d.intensity, 0f, lerpTime);
            light2dOuter.intensity = Mathf.Lerp(light2dOuter.intensity, 0f, lerpTime);
        }

        Debug.Log(time);

        if (time >= timer)
        {
            Destroy(gameObject);
        }

        transform.position = new Vector2(transform.position.x, transform.position.y);
    }
}
