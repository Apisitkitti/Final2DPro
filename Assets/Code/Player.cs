using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OpenSimplex2S;

public class Player : MonoBehaviour
{
    public float playerHealth = 100.0f;
    public Animator anim;
    public GameObject deathUI;
    public GameObject qw;
    public GameObject deathEffectPrefab;
    public float deathEffectDuration = 1.5f;
    public int deathEffectParticleCount = 50;
    public float deathEffectNoiseScale = 0.5f;
    public float deathEffectNoiseSpeed = 1.0f;

    private bool isDead = false;
    private bool isPaused = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isPaused)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }

    public void TakeDamage(float damage)
    {
        playerHealth -= damage;
        anim.SetTrigger("TakeDam");

        if (playerHealth <= 0 && !isDead)
        {
            isDead = true;
            Die();
        }
        if (playerHealth <= 20)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }

    }

    private void Die()
    {
        anim.SetBool("Death", true);
        StartCoroutine(DeathEffect());
        StartCoroutine(DeactivateAfterAnimation());
        deathUI.SetActive(true);
        qw.SetActive(false);

        isPaused = true;
    }

    private IEnumerator DeactivateAfterAnimation()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
    }

    private IEnumerator DeathEffect()
    {
        float elapsedTime = 0.0f;
        Color originalColor = GetComponent<SpriteRenderer>().color;
        Vector3 deathPos = transform.position;

        while (elapsedTime < deathEffectDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / deathEffectDuration;

            // Apply Simplex noise to the position of the player
            float noise = (float)OpenSimplex2S.Noise2(0, deathPos.x * deathEffectNoiseScale, deathPos.y * deathEffectNoiseScale);
            Vector3 noiseVec = new Vector3(Mathf.Cos(noise * 360.0f), Mathf.Sin(noise * 360.0f), 0.0f);
            transform.position = deathPos + noiseVec * elapsedTime;

            // Generate particles with random colors
            for (int i = 0; i < deathEffectParticleCount; i++)
            {
                Color targetColor = new Color(Random.value, Random.value, Random.value);
                Vector3 position = transform.position + new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f); // generate a random position
                Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)); // generate a random rotation
                GameObject particle = Instantiate(deathEffectPrefab, position, rotation); // instantiate a particle prefab
                particle.GetComponent<ParticleSystem>().startColor = targetColor; // set the color of the particle system
                Destroy(particle, deathEffectDuration); // destroy the

        }

        yield return null;
    }

    // Disable the game object after the death effect
    gameObject.SetActive(false);
}




}
