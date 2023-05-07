using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OpenSimplex2S;


public class Enemy : MonoBehaviour
{
    #region public
    public float speed = 1.0f;
    public float scale = 1.0f;
    public float time = 1.0f;
    public float Health = 100.0f;
    public int amount = 1;
    public int point = 1;
    
    public Animator anim;
    public ParticleSystem particlePrefab;
    ScoreManager score;
    [SerializeField]ActiveAgent spawn;
    #endregion

    [SerializeField] SpriteRenderer render;

    private float startTime;
    private Vector3 deathPos;
    private bool isDead = false;
    
    

    private void Start()
    {
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        score = GetComponent<ScoreManager>();
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
    StartCoroutine(DeathEffect(amount));
    GetComponent<Collider2D>().enabled = false;
    Destroy(gameObject,2.0f);
    ScoreManager.score += point*spawn.currentWave+1;
}


    private IEnumerator DeactivateAfterAnimation()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
    }

    private IEnumerator DeathEffect(int amount)
{
    float elapsedTime = 0.0f;
    float noiseScale = 0.5f;
    float noiseSpeed = 1.0f;
    Color originalColor = GetComponent<SpriteRenderer>().color;
    Color targetColor = new Color(Random.value, Random.value, Random.value); // generate a random color

    Vector3 deathPos = transform.position;

    while (elapsedTime < 1.5f)
    {
        elapsedTime += Time.deltaTime;
        float t = elapsedTime / 1.5f;

        // Apply Simplex noise to the position of the enemy
        float noise = (float)OpenSimplex2S.Noise2(0,(deathPos.x + Time.time * noiseSpeed) * noiseScale, (deathPos.y + Time.time * noiseSpeed) * noiseScale);
        Vector3 noiseVec = new Vector3(Mathf.Cos(noise * 360.0f), Mathf.Sin(noise * 360.0f), 0.0f);
        transform.position = deathPos + noiseVec * speed * elapsedTime;

        // Lerp the color of the enemy sprite to the target color
        GetComponent<SpriteRenderer>().color = Color.Lerp(originalColor, targetColor, t);

        // Generate particles
        for (int i = 0; i < amount; i++)
        {
            Vector3 position = transform.position + new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f); // generate a random position
            Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)); // generate a random rotation
            UnityEngine.ParticleSystem particle = Instantiate(particlePrefab, position, rotation); // instantiate a particle prefab
            particle.Play(); // start the particle system
            Destroy(particle.gameObject, particle.main.duration); // destroy the particle system after it finishes playing
        }

        yield return null;
    }
}







}
