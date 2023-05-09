using UnityEngine;
using static OpenSimplex2S;
public class Enemy_Behavior : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 10;
    public int health = 100;
    public GameObject player;
    public string spawnerTag; // Add spawnerTag property
    public GameObject deathEffectPrefab;
    public float deathEffectDuration = 2f;
    public float deathEffectNoiseScale = 0.1f; // Add a noise scale for the death effect

    private Rigidbody2D rb;
    private Animator anim;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if(PauseMenu.PauseCheck == false)
        {
        if (player != null)
        {
            // Move towards the player
            Vector2 direction = (player.transform.position - transform.position).normalized;
            rb.velocity = direction * speed;
            anim.SetBool("Run", true);

            // Face and flip towards the player
            if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (direction.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                // Deal damage to the player
                player.TakeDamage(damage);

                anim.SetBool("Run", false);
                anim.SetBool("Death", true);
                GetComponent<Collider2D>().enabled = false;

                // Instantiate the death effect prefab
                GameObject deathEffect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);

                // Add Simplex noise to the death effect
                SpriteRenderer[] renderers = deathEffect.GetComponentsInChildren<SpriteRenderer>();
                foreach (SpriteRenderer renderer in renderers)
                {
                    Vector3 noiseOffset = new Vector3(Random.Range(0f, 100f), Random.Range(0f, 100f), Random.Range(0f, 100f));
                    Vector3 position = renderer.transform.localPosition;
                    position += new Vector3(
                    (float)OpenSimplex2S.Noise2((long)(position.x + noiseOffset.x), (position.y + noiseOffset.y) * deathEffectNoiseScale, (position.z + noiseOffset.z) * deathEffectNoiseScale) - 0.5f,
                    (float)OpenSimplex2S.Noise2((long)(position.y + noiseOffset.y), (position.z + noiseOffset.z) * deathEffectNoiseScale, (position.x + noiseOffset.x) * deathEffectNoiseScale) - 0.5f,
                    (float)OpenSimplex2S.Noise2((long)(position.z + noiseOffset.z), (position.x + noiseOffset.x) * deathEffectNoiseScale, (position.y + noiseOffset.y) * deathEffectNoiseScale) - 0.5f
                );

                    renderer.transform.localPosition = position;
                }

                // Destroy the death effect after a duration
                Destroy(deathEffect, deathEffectDuration);

                // Destroy the enemy GameObject
                Destroy(gameObject);
            }
        }
    }
}
