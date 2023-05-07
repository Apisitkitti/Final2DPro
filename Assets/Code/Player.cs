using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OpenSimplex2S;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float playerHealth = 100.0f;
    public Animator anim;
    public GameObject deathUI;
    public GameObject qw;
    public GameObject Healthbar;
    public float deathEffectDuration = 1.5f;
    public int deathEffectParticleCount = 50;
    public float deathEffectNoiseScale = 0.5f;
    public float deathEffectNoiseSpeed = 1.0f;
    public Slider healthSlider;


    private bool isDead = false;
    private bool isPaused = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
{
    if (deathUI.activeSelf) // Check if the DeathUI canvas is active
    {
        Time.timeScale = 0.0f; // If yes, pause the game
    }
    else
    {
        Time.timeScale = 1.0f; // Otherwise, resume the game
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
        healthSlider.value = playerHealth;
    }

    private void Die()
    {
        anim.SetBool("Death", true);
        StartCoroutine(DeathEffect());
        StartCoroutine(DeactivateAfterAnimation());
        deathUI.SetActive(true);
        qw.SetActive(false);
        Healthbar.SetActive(false);
        

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
                
        }

        yield return null;
    }

    // Disable the game object after the death effect
    gameObject.SetActive(false);
}




}
