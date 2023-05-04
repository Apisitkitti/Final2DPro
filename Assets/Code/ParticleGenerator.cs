using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleGenerator : MonoBehaviour
{
    public GameObject particlePrefab;
    public float particleSpeed = 5f;
    private List<GameObject> particles = new List<GameObject>();

    public void GenerateParticles(int numberOfParticles, float radius, float noiseScale, float noiseSpeed, float particleLifetime)
    {
        // Spawn particles
        for (int i = 0; i < numberOfParticles; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfParticles;
            Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            GameObject particle = Instantiate(particlePrefab, transform.position + pos, Quaternion.identity);
            particles.Add(particle);
            Destroy(particle, particleLifetime);
        }
    }

    public void CreateDeathParticles(Vector3 deathPos, float time, float noiseScale, float noiseSpeed, Color targetColor)
    {
        // Create particles at the enemy's death position
        GenerateParticles(50, 1.0f, 0.5f, 1.0f, 1.0f);

        // Apply Simplex noise to the position of each particle
        float startTime = Time.time;
        for (int i = 0; i < particles.Count; i++)
        {
            GameObject particle = particles[i];
            Vector3 pos = particle.transform.position;
            float noise = Mathf.PerlinNoise((pos.x + startTime * noiseSpeed) * noiseScale, (pos.y + startTime * noiseSpeed) * noiseScale);
            Vector3 noiseVec = new Vector3(Mathf.Cos(noise * 360.0f), Mathf.Sin(noise * 360.0f), 0.0f);
            particle.transform.position = deathPos + noiseVec * particleSpeed * time;
        }

        // Lerp the color of each particle to the target color
        foreach (GameObject particle in particles)
        {
            ParticleSystemRenderer renderer = particle.GetComponent<ParticleSystemRenderer>();
            if (renderer)
            {
                renderer.material.color = targetColor;
            }
        }
    }
}
