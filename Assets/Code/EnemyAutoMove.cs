using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAutoMove : MonoBehaviour
{
    public Transform player;
   
    public float attackRange = 0.5f;
    public int attackDamage = 40;
    public LayerMask PlayerLayers;

    private Rigidbody2D rb;
    public Animator anim;
    private bool isAttacking = false;

    public BoxCollider2D boxCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
    }

    void Update()
    {
        float distance = Vector2.Distance(player.position, transform.position);
        if (distance <= attackRange)
        {
            Attacker();
        }
    }

    void Attacker()
    {
        isAttacking = true;
        anim.SetBool("Attack", true);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (isAttacking && other.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeDamage(attackDamage);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isAttacking = false;
            anim.SetBool("Attack", false);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
