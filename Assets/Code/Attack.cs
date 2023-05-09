using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Animator anim;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int attackDamage = 40;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    public LayerMask enemyLayers;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PointerUpAttack()
    {
        anim.SetBool("Attack", false);
    }

    public void PointerDownAttack()
    {
        
            Attacker();
        
       
    }

    public void Attacker()
{
    anim.SetBool("Attack", true);
    Vector3 attackPos = attackPoint.position;

    if (transform.localScale.x < 0) // check if player is facing left
    {
        attackPos -= new Vector3(2 * attackPoint.localPosition.x, 0, 0); // adjust attack point position
    }

    // Cast a circle to find all enemies within range
    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPos, attackRange, enemyLayers);

    // Loop through all enemies in range and deal damage
    foreach (Collider2D hitEnemy in hitEnemies)
    {
        if (hitEnemy.CompareTag("Enemy"))
        {
            hitEnemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
        if(hitEnemy.CompareTag("boss"))
        {
            hitEnemy.GetComponent<Boss>().TakeDamage(attackDamage);
        }
    }
}


    void OnDrawGizmosSelected() 
    {
        if (attackPoint == null) 
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}