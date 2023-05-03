using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    public float speed = 1.0f;
    public float scale = 1.0f;
    public float time = 1.0f;

    private float startTime;

    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        float t = (Time.time - startTime) / time;
        float noise = Mathf.PerlinNoise(transform.position.x * scale, transform.position.y * scale);
        transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t);
        transform.position += new Vector3(Mathf.Cos(noise * 360.0f) * speed, Mathf.Sin(noise * 360.0f) * speed, 0.0f) * Time.deltaTime;
    }
}
