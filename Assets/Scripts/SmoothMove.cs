using UnityEngine;

public class SmoothMove : MonoBehaviour
{
    public float amplitude = 0.5f; // Amplitude da onda
    public float frequency = 1f; // Frequência da onda
    public float speed = 1f; // Velocidade da onda

    void Update()
    {
        float offset = Mathf.PingPong(Time.time * frequency, 1) - 0.5f;
        transform.position += new Vector3(0, offset * amplitude * speed, 0);

    }
}
