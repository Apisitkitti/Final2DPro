using UnityEngine;

public class Enemy_Behavior : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 10;
    public int health = 100;
    public GameObject player;
    public string spawnerTag; // Add spawnerTag property

    private Rigidbody2D rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Deal damage to the player
            collision.gameObject.GetComponent<Player>().TakeDamage(damage);
            anim.SetBool("Run", false);
            anim.SetBool("Death",true);
            Destroy(gameObject);
        }
    }
}
