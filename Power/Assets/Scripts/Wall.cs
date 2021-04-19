using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public float rotationSpeed = 0.1f;
    public bool randomSpeed = true;
    private int rotationDirection = 1;
    // Start is called before the first frame update
    void Start()
    {
        if (randomSpeed)
        {
            rotationSpeed = Random.Range(0.001f, 1.0f);
            rotationDirection = randomBooleanSpawn(randomBoolean());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.GameIsPaused)
            transform.Rotate(0, 0, rotationSpeed * rotationDirection);
    }

    int randomBooleanSpawn(int rBool)
    {
        return rBool * rotationDirection;
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
