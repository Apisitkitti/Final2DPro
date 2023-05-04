using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAgent : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public int numberOfEnemies = 10;
    public float spawnDelay = 1f;
    public float waveDelay = 5f;
    public int numberOfWaves = 3;
    public int enemyDamageIncreasePerWave = 1;
    public float enemySpeedIncreasePerWave = 0.1f;

    private int enemiesRemaining;
    private bool isSpawning;
    private int currentWave = 0;
    private bool playerIsDead = false;

    void Start()
    {
        isSpawning = true;
        StartCoroutine(SpawnEnemies());
    }
    void Update()
    {
        if (playerIsDead)
        {
            isSpawning = false;
        }
    }

    IEnumerator SpawnEnemies()
    {
        int waveNumber = 1;

        while (isSpawning)
        {
            // Spawn enemies for the current wave from both spawn points
            for (int i = 0; i < numberOfEnemies; i++)
            {
                if (playerIsDead)
                {
                    break; // Stop spawning if the player is dead
                }
                Transform spawnTransform = i % 2 == 0 ? spawnPoint1 : spawnPoint2; // Alternate between spawn points
                GameObject newEnemyObject = Instantiate(enemyPrefab, spawnTransform.position, Quaternion.identity);
                Enemy_Behavior newEnemyBehavior = newEnemyObject.GetComponent<Enemy_Behavior>();

                newEnemyBehavior.spawnerTag = gameObject.tag; // Assign spawner tag to new enemy
                newEnemyBehavior.player = GameObject.FindGameObjectWithTag("Player");
                newEnemyBehavior.damage += enemyDamageIncreasePerWave * currentWave; // Increase damage based on current wave
                newEnemyBehavior.speed += enemySpeedIncreasePerWave * currentWave; // Increase speed based on current wave

                newEnemyObject.transform.position = new Vector3(newEnemyObject.transform.position.x, newEnemyObject.transform.position.y, 0);
                yield return new WaitForSeconds(spawnDelay);
            }

            // Wait until all enemies from this spawner have been defeated before starting a new wave
            while (true)
            {
                int enemiesFromSpawner = 0;
                foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    if (enemy.GetComponent<Enemy_Behavior>().spawnerTag == gameObject.tag)
                    {
                        enemiesFromSpawner++;
                    }
                }

                if (enemiesFromSpawner == 0)
                {
                    if (currentWave >= numberOfWaves)
                    {
                        isSpawning = false; // Stop spawning enemies if the maximum number of waves has been reached
                        break;
                    }
                    else
                    {
                        currentWave++;
                        waveNumber++;
                        numberOfEnemies++; // Increase the number of enemies for the next wave by 1
                        enemiesRemaining = numberOfEnemies * (waveNumber + currentWave);
                        yield return new WaitForSeconds(waveDelay); // Wait before starting a new wave
                        break;
                    }
                }

                yield return null;
            }
        }
    }
    public void PlayerDied()
    {
        playerIsDead = true;
    }
}
