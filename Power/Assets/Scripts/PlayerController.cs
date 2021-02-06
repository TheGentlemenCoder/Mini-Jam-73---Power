using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D playerRb;
    //private bool isAlive = true;
    public int poweredSystem = 1;

    [SerializeField]
    private GameObject thrusterTop, thrusterRight, thrusterLeft, thrusterBottom;



//Movement Variables
public float horizontalInput, verticalInput;
    public float maxSpeed = 5.0f;
    public float horizontalSpeed = 0.0f;
    public float verticalSpeed = 0.0f;
    public float thrustForce;

//O2 Variables
    public float o2Level = 100.0f;
    public float consumptionRate = 1.0f;


    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        switch (poweredSystem)
        {
            case 0:
                break;
                
        //PLAYER MOVEMENT
            case 1:
        
        if (Input.GetKey(KeyCode.W))
        {
            playerRb.AddForce(Vector3.up * thrustForce, ForceMode2D.Force);
            thrusterBottom.GetComponent<SpriteRenderer>().enabled = true;
                } else
            thrusterBottom.GetComponent<SpriteRenderer>().enabled = false;

        
        if (Input.GetKey(KeyCode.S))
        {
            playerRb.AddForce(Vector3.down * thrustForce, ForceMode2D.Force);
            thrusterTop.GetComponent<SpriteRenderer>().enabled = true;
                } else
            thrusterTop.GetComponent<SpriteRenderer>().enabled = false;
        
        if (Input.GetKey(KeyCode.A))
        {
            playerRb.AddForce(Vector3.left * thrustForce, ForceMode2D.Force);
            thrusterRight.GetComponent<SpriteRenderer>().enabled = true;
                } else
            thrusterRight.GetComponent<SpriteRenderer>().enabled = false;
        
        if (Input.GetKey(KeyCode.D))
        {
            playerRb.AddForce(Vector3.right * thrustForce, ForceMode2D.Force);
            thrusterLeft.GetComponent<SpriteRenderer>().enabled = true;
                } else
            thrusterLeft.GetComponent<SpriteRenderer>().enabled = false;

        break;

        case 2:
                o2Level = o2Level + consumptionRate * Time.deltaTime * 10;

                break;
        }

        
        //02 LEVEL
        o2Level = o2Level - consumptionRate * Time.deltaTime;
        if (o2Level > 100.0)
            o2Level = 100.0f;
        Debug.Log("O2 Level: " + o2Level);
    }
}
