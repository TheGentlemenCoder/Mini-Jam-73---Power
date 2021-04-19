using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryBarrier : MonoBehaviour
{
    void Update()
    {
        if (GameManager.instance.driveNumber >= GameManager.instance.winCondition)
        {
            Destroy(gameObject);
        }

    }
}
