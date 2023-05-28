using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSpawnner : MonoBehaviour
{
    public GameObject fireball;
    public GameObject[] FireballSpawners;
    public int minNumberOfFireballsPerWave = 3;
    public int maxNumberOfFireballsPerWave = 6;
    public float timeBetweenWaves = 5f;
    public float timeBetweenFireballs = 1f;
    public GameObject player;
    public int maxNumberOfFireballsToSpawn = 2;

    private int fireballsSpawned = 0;
    private int numberOfFireballsToSpawn = 0;
    private float waveTimer = 0f;
    private float fireballTimer = 0f;
    [SerializeField] float delaydrop = 2;

    private bool isSpawningEnabled = true; // Flag to control the spawning

    private bool shouldRestartSpawning = false; // Flag to indicate if spawning should be restarted

    void Start()
{
    waveTimer = timeBetweenWaves;
    StartCoroutine(EnablePlayerRigidbody());

    // Start spawning fireballs again
    fireballsSpawned = 0;
    numberOfFireballsToSpawn = 0;
    fireballTimer = 0f;
    isSpawningEnabled = true;
}

    IEnumerator EnablePlayerRigidbody()
    {
        if (PauseMenu.PauseCheck == false)
        {
            yield return new WaitForSeconds(delaydrop);
            player.GetComponent<BoxCollider2D>().enabled = true;
            player.GetComponent<Rigidbody2D>().simulated = true;
        }
        else
        {
            player.GetComponent<BoxCollider2D>().enabled = false;
            player.GetComponent<Rigidbody2D>().simulated = false;
        }
    }

    void Update()
    {
        if (PauseMenu.PauseCheck == false)
        {
            // Check if spawning is enabled
            if (!isSpawningEnabled)
                return;

            // Check if it's time to spawn a new wave
            if (waveTimer <= 0f && fireballsSpawned == 0)
            {
                // Generate a random number of fireballs for the next wave
                numberOfFireballsToSpawn = Random.Range(minNumberOfFireballsPerWave, maxNumberOfFireballsPerWave + 1);

                // Reset counters
                fireballsSpawned = 0;
                waveTimer = timeBetweenWaves;

                // Start spawning fireballs
                fireballTimer = 0f;
            }
            else
            {
                waveTimer -= Time.deltaTime;
            }

            // Check if we've spawned the maximum number of fireballs
            if (fireballsSpawned >= maxNumberOfFireballsToSpawn)
            {
                StartCoroutine(EnablePlayerRigidbody());
                return;
                
            }

            // Check if it's time to spawn a new fireball
            if (fireballTimer <= 0f && fireballsSpawned < numberOfFireballsToSpawn)
            {
                if (FireballSpawners.Length > 0)
                {
                    // Select a random spawn point
                    int index = Random.Range(0, FireballSpawners.Length);
                    GameObject spawnPoint = FireballSpawners[index];

                    // Spawn the fireball at the position and rotation of the spawn point            
                    Instantiate(fireball, spawnPoint.transform.position, spawnPoint.transform.rotation);
                    fireball.GetComponent<FireBall>().player = GameObject.FindGameObjectWithTag("Player").transform;

                    // Increment the counter for the number of fireballs spawned
                    fireballsSpawned++;

                    // Reset the timer for the next fireball spawn
                    fireballTimer = timeBetweenFireballs;
                }
            }
            else
            {
                fireballTimer -= Time.deltaTime;
            }
        }
    }

    public void RestartSpawning()
    {
        fireballsSpawned = 0;
        waveTimer = 0f;
        shouldRestartSpawning = true;
        isSpawningEnabled = true;
    }

    private void LateUpdate()
    {
        if (shouldRestartSpawning)
        {
            waveTimer = timeBetweenWaves;
            shouldRestartSpawning = false;
        }
    }
}
