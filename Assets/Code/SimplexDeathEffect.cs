using UnityEngine;

public class SimplexDeathEffect : MonoBehaviour
{
    public float duration = 1.0f;
    public float scale = 1.0f;
    public float speed = 1.0f;
    public float amplitude = 1.0f;
    public float frequency = 1.0f;

    private float time;

    private void Start()
    {
        time = Time.time;
    }

    private void Update()
    {
        float x = (transform.position.x * scale) + (Time.time * speed);
        float y = (transform.position.y * scale) + (Time.time * speed);

        float noise = Mathf.PerlinNoise(x, y);
        noise = ((noise * 2.0f) - 1.0f) * amplitude;

        Vector3 pos = transform.position;
        pos.y += noise;

        transform.position = pos;

        if (Time.time > (time + duration))
        {
            Destroy(gameObject);
        }
    }
}
