using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    bool isMoveLef;
    bool isMoveRight;
    float horizontalMove;
    bool ground = false;
    public Transform attackPoint;

    private Rigidbody2D rb;
    private Animator anim;
    SpriteRenderer spriteRenderer; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();   
    }

    public void OnCollisionEnter(Collision other)
    {
        ground = true;
    }

    public void PointerDownLeft()
    {
        isMoveLef = true;
        anim.SetBool("Run",isMoveLef);
        spriteRenderer.flipX = true;
        UpdateAttackPointFlip();
    }

    public void PointerDownRight()
    {
        isMoveRight = true;
        anim.SetBool("Run",isMoveRight);
        spriteRenderer.flipX = false;
        UpdateAttackPointFlip();
    }

    public void PointerUpLeft()
    {
        isMoveLef = false;
        anim.SetBool("Run",isMoveLef);
    }

    public void PointerUpRight()
    {
        isMoveRight = false;
        anim.SetBool("Run",isMoveRight);
    }

    private void Update() 
    {
        MovePlayer();   
    }

    private void MovePlayer()
    {
        if(isMoveLef) 
        {
            horizontalMove = -speed;
        } 
        else if(isMoveRight)
        {
            horizontalMove = speed;
        } 
        else
        {
            horizontalMove = 0;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalMove,rb.velocity.y);
        if(ground == false)
        {
            anim.SetBool("Fall",ground);
        }
    }     

    private void UpdateAttackPointFlip()
    {
        Vector3 localPos = attackPoint.localPosition;
        localPos.x = spriteRenderer.flipX ? -Mathf.Abs(localPos.x) : Mathf.Abs(localPos.x);
        localPos.y = spriteRenderer.flipY ? -Mathf.Abs(localPos.y) : Mathf.Abs(localPos.y);
        attackPoint.localPosition = localPos;
    }
}
