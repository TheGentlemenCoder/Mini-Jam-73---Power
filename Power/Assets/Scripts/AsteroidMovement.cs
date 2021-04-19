using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    private Rigidbody2D asteroidRb;
    public float speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        asteroidRb = GetComponent<Rigidbody2D>();
        asteroidRb.AddForce(Vector3.left * Random.Range(-20, 20) * speed, ForceMode2D.Force);
        asteroidRb.AddForce(Vector3.up * Random.Range(-20, 20) * speed, ForceMode2D.Force);
    }

    // Update is called once per frame
/*    void Update()
    {
        if (asteroidRb.position.x > 50 || asteroidRb.position.y > 50 || asteroidRb.position.x < -50 || asteroidRb.position.y < -50)
        {
            Destroy(gameObject);
        }
    }
    */
}
