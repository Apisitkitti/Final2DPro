using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    [SerializeField] private float PlayerHealth;
    
    public GameObject DeathUI;
    bool DeathCheck = false;
    private int currentLevel;
    

    Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }
public void TakeDamage(int damage)
    {
        PlayerHealth -= damage;
        anim.SetTrigger("TakeDam");

        
        if(PlayerHealth <=0)
        {
            Die();

        }
        if(PlayerHealth > 0)
        {
            DeathUI.SetActive(false);
        }
    }
    public void Die()
    {
        anim.SetBool("Death",true);

        StartCoroutine(DeactivateAfterAnimation());
        DeathUI.SetActive(true);
         
        
    }

    private IEnumerator DeactivateAfterAnimation()
    {
      yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
    }
    public void Check(bool dead)
    {
        if(dead == true)
        {
            SceneManager.LoadScene(currentLevel);
        }
            
        
            
    }
    

   
}
