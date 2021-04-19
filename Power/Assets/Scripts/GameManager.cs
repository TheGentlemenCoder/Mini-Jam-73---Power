using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Transform respawnPoint;
    public GameObject playerPrefab;

    public int driveNumber;
    public int winCondition;
    //public GameObject[] coresInLevel;

    private void Awake()
    {
        instance = this;
        driveNumber = 0;
        winCondition = 3;
    }

    public void Respawn()
    {
        Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
    }
}
