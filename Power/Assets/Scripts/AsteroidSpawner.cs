using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject[] asteroidPrefabs;
    private float spawnInterval = 0.5f;
    private float spawnRangeY = 40;
    private int spawnPosSideX = 40;

    void Start()
    {
        InvokeRepeating("SpawnRandomAsteroid", 0.5f, spawnInterval);

    }

    void SpawnRandomAsteroid()
    {
        int randbool = randomBoolean();
        int asteroidIndex = Random.Range(0, asteroidPrefabs.Length);
        Vector3 spawnPos = new Vector3(randomBooleanSpawn(randbool), Random.Range(-spawnRangeY, spawnRangeY), 0);
        Instantiate(asteroidPrefabs[asteroidIndex], spawnPos, asteroidPrefabs[asteroidIndex].transform.rotation);
    }
    int randomBooleanSpawn(int rBool)
    {
        return rBool * spawnPosSideX;
    }

    int randomBoolean()
    {
        if (Random.value >= 0.5)
        {
            return 1;
        }
        return -1;
    }
}