using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1.0f;
    public float scale = 1.0f;
    public float time = 1.0f;
    public float Health = 100.0f;
    
    public Animator anim;

    [SerializeField] SpriteRenderer render;

    private float startTime;
    private Vector3 deathPos;
    private bool isDead = false;
    
    

    private void Start()
    {
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        startTime = Time.time;
        deathPos = transform.position;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage/2;
        anim.SetTrigger("TakeDam");

        if (Health <= 0 && !isDead)
        {
            isDead = true;
            Die();
        }
    }

    private void Die()
{
    anim.SetBool("Run",false);
    anim.SetBool("Death", true);
    StartCoroutine(DeathEffect());
    Destroy(gameObject);
}


    private IEnumerator DeactivateAfterAnimation()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
    }

    private IEnumerator DeathEffect()
{
    float elapsedTime = 0.0f;
    float noiseScale = 0.5f;
    float noiseSpeed = 1.0f;
     Color originalColor = GetComponent<SpriteRenderer>().color;
    Color targetColor = Color.red; // change to desired color

    while (elapsedTime < time)
    {
        elapsedTime += Time.deltaTime;
        float t = elapsedTime / time;

        // Lerp the scale of the enemy to zero
        transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t);

        // Apply Simplex noise to the position of the enemy
        float noise = Mathf.PerlinNoise((deathPos.x + Time.time * noiseSpeed) * noiseScale, (deathPos.y + Time.time * noiseSpeed) * noiseScale);
        Vector3 noiseVec = new Vector3(Mathf.Cos(noise * 360.0f), Mathf.Sin(noise * 360.0f), 0.0f);
        transform.position = deathPos + noiseVec * speed * elapsedTime;

        // Lerp the color of the enemy sprite to the target color
        render.color = Color.Lerp(originalColor, targetColor, t);

        yield return null;
    }
}

}
