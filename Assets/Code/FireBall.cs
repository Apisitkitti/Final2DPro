using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public int dam = 10;
    public Transform player;
    

    void Update()
    {
        if(PauseMenu.PauseCheck == true)
        {
            gameObject.GetComponent<Rigidbody2D>().simulated = false;
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().simulated = true;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                // Deal damage to the player
                player.TakeDamage(dam);
            }
            // Destroy the FireBall on collision with the player
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
