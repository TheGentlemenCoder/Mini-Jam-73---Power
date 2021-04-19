using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    private float startDelay = 2.0f;
    private float startDelaySide = 4.0f;
    private float spawnInterval = 1.5f;
    private float spawnIntervalSide = 4.0f;
    private float spawnRangeX = 20;
    private float spawnRangeZ = 7;
    private float spawnPosZ = 20;
    private int spawnPosSideX = 30;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);
        InvokeRepeating("SpawnRandomAnimalFromSide", startDelaySide, spawnIntervalSide);
    }



    void SpawnRandomAnimal()
    {
        int animalIndex = Random.Range(0, animalPrefabs.Length);
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);
        Instantiate(animalPrefabs[animalIndex], spawnPos, animalPrefabs[animalIndex].transform.rotation);
    }

    void SpawnRandomAnimalFromSide()
    {
        int randBool = randomBoolean();
        int animalIndex = Random.Range(0, animalPrefabs.Length);
        Vector3 spawnPos = new Vector3(randomBooleanSpawn(randBool), 0, Random.Range(0, -spawnRangeZ));
        Instantiate(animalPrefabs[animalIndex], spawnPos, animalPrefabs[animalIndex].transform.rotation * Quaternion.Euler(0f, 90 * randBool, 0f));
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
    } //credit to cupsster, edited by me

}

