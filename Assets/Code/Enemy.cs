using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float Health;
    
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        anim.SetTrigger("TakeDam");

        
        if(Health <=0)
        {
            Die();
        }
    }
    public void Die()
    {
        anim.SetBool("Death",true);

        StartCoroutine(DeactivateAfterAnimation());
    }

    private IEnumerator DeactivateAfterAnimation()
    {
      yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
    }

}
