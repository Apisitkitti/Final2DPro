using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Behavior : MonoBehaviour
{
    #region Public Variables
    public Transform rayCast;
    public LayerMask raycastMask;
    public float rayCastLength;
    public float attackDistance;
    public float moveSpeed;
    public float timer;
    public int attackDamage = 40;
    public float attackRate = 2f;
     public float attackRange = 0.5f;
    
    #endregion

    #region Private Variables
    private RaycastHit2D hit;
    private Animator anim;
    private float distance;
    private bool attackMode;
    private bool inRange;
    private bool cooling;
    private float intTimer;
    [SerializeField] GameObject target; 
    
    
    [SerializeField]private Transform attackPoint;
   
    #endregion
    void Awake()
    {
      intTimer = timer;
      anim = GetComponent<Animator>();  
    }

    void Update()
    {
        if(!attackMode)
        {
            Move();
        }
        if(inRange)
        {
            hit  = Physics2D.Raycast(rayCast.position,Vector2.left,rayCastLength,raycastMask);
            RaycastDebugger();
        } 
         if(hit.collider != null )
         {
            EnemyLogic();
         }

         else if(hit.collider == null)
         {
            inRange = false;
         }
         if(inRange == false)
         {
            anim.SetBool("Run",false);
            StopAttack(); 
         }
    }
   
    void OnTriggerEnter2D(Collider2D trig)
    {
        if(trig.gameObject.tag == "Player")
        {
           target = trig.gameObject;
           inRange = true;
        }

    }
    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position,target.transform.position);
        if(distance<attackDistance)
        {
            Move();
            StopAttack();
        }
        else if(attackDistance<=distance && cooling == false)
        {
            Attack();
        }
        if(cooling)
        {
            CoolDown();
            anim.SetBool("Attack",false);
        }

    }
    void Move()
    {
        anim.SetBool("Run",true); 
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            Vector2 targetPosition = new Vector2(target.transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position,-targetPosition,moveSpeed*Time.deltaTime);
        }
    }
    void Attack()
    {
        timer = intTimer;
        attackMode = true;

        anim.SetBool("Run",false);
        anim.SetBool("Attack",true);
        Vector3 attackPos = attackPoint.position;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPos, attackRange, raycastMask);

    // Loop through all enemies in range and deal damage
    foreach (Collider2D hitEnemy in hitEnemies)
    {
        if (hitEnemy.CompareTag("Player"))
        {
            hitEnemy.GetComponent<Player>().TakeDamage(attackDamage);
        }
    }

    }
    void CoolDown()
    {
        timer -= Time.deltaTime;
        if(timer <= 0 && cooling &&  attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }
    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack",false);
    }
    
    void RaycastDebugger()
    {
        if(distance> attackDistance)
        {
            Debug.DrawRay(rayCast.position,Vector2.left * rayCastLength,Color.red);
        }
        else if(attackDistance> distance)
        {
            Debug.DrawRay(rayCast.position,Vector2.left * rayCastLength,Color.green);
        }
    } 
    public void TriggerCooling()
    {
    
      cooling = true;
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
