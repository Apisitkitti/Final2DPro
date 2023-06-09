using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehavior : MonoBehaviour
{
    public GameObject player;
    public int damage =2;
    
    void Update()
    {
        if(PauseMenu.PauseCheck)
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
    if (collision.gameObject.tag == "Player")
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
    if(collision.gameObject.tag == "Ground")
    {
        Destroy(gameObject);
    }
}
}
