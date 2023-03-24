using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    Animator anim;
     float nextGuardTime = 0f;
     public float GuardkRate = 2f;
    void Start()
    {
       anim = GetComponent<Animator>(); 
    }

    public void PointerDownGuard()
    {
        if(Time.time>=nextGuardTime)
        {
            anim.SetBool("Block",true);
            nextGuardTime = Time.time+1f/GuardkRate;
        }
      
    }
    public void PointerUpGuard()
    {
      anim.SetBool("Block",false);
    }
}
