using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public Slider slider;

    [SerializeField]
    private GameObject player, thrusterTop, thrusterRight, thrusterLeft, thrusterBottom, shield, spawnPoint = null;
    private Rigidbody2D playerRb;
    private Vector3 spawnPos;
    [SerializeField]
    private int poweredSystem = 1;
    [SerializeField]
    private float health = 100.0f;
    private float dangerZone = 25.0f;

    //Movement Variables
    private int thrustForce = 1;

    //O2 Variables
    private float consumptionRate = 3.0f;
    private float o2Level;
    private bool isScaling;
    private bool isRespawning;


    void Start()
    {
        player = transform.parent.gameObject;
        isRespawning = false;
        playerRb = GetComponent<Rigidbody2D>();
        o2Level = health;
        slider.value = health;
        //poweredSystem = 1;
        isScaling = false;
    }

    // Update is called once per frame
    void Update()
    {
        //PLAYER CURRENT ACTIVE SYSTEM
        switch (poweredSystem)
        {
            //DEATHSTATE
            case 0:
                break;

            //PLAYER MOVEMENT
            case 1:

                if (Input.GetKey(KeyCode.W))
                {
                    playerRb.AddForce(Vector3.up * thrustForce, ForceMode2D.Force);
                    thrusterBottom.GetComponent<SpriteRenderer>().enabled = true;
                }
                else
                    thrusterBottom.GetComponent<SpriteRenderer>().enabled = false;

                if (Input.GetKey(KeyCode.S))
                {
                    playerRb.AddForce(Vector3.down * thrustForce, ForceMode2D.Force);
                    thrusterTop.GetComponent<SpriteRenderer>().enabled = true;
                }
                else
                    thrusterTop.GetComponent<SpriteRenderer>().enabled = false;

                if (Input.GetKey(KeyCode.A))
                {
                    playerRb.AddForce(Vector3.left * thrustForce, ForceMode2D.Force);
                    thrusterRight.GetComponent<SpriteRenderer>().enabled = true;
                }
                else
                    thrusterRight.GetComponent<SpriteRenderer>().enabled = false;

                if (Input.GetKey(KeyCode.D))
                {
                    playerRb.AddForce(Vector3.right * thrustForce, ForceMode2D.Force);
                    thrusterLeft.GetComponent<SpriteRenderer>().enabled = true;
                }
                else
                    thrusterLeft.GetComponent<SpriteRenderer>().enabled = false;

                break;

            //OXYGEN REFILL
            case 2:
                o2Level = o2Level + consumptionRate * Time.deltaTime * 10;
                break;

            case 3:
                shield.SetActive(true);

                if (!isScaling)
                    StartCoroutine(ScaleOverTime());

                break;
        }

        O2Depletion();

        //end UPDATE
    }

    private void O2Depletion()
    {
        //02 LEVEL DEPLETION
        o2Level = o2Level - consumptionRate * Time.deltaTime;

        if (o2Level > 100.0)
            o2Level = 100.0f;

        SetHealth(o2Level);

        if (o2Level < dangerZone)
        {
            HealthFlash();
        }
        else if (o2Level >= dangerZone && !slider.gameObject.activeSelf) //BUGCATCHANDFIX
        {
            slider.gameObject.SetActive(true);
        }

        //DEATHSTATE
        if (o2Level <= 0)
        {
            if (!isRespawning)
            {
                Destroy(player);
                GameManager.instance.Respawn();
            }
        }
    }

    public void ActivateThrusters()
    {
        poweredSystem = 1;
        SystemReset();
    }

    public void ActivateOxygen()
    {
        poweredSystem = 2;
        SystemReset();
    }

    public void ActivateShield()
    {
        poweredSystem = 3;
        SystemReset();
    }

    private void SystemReset()
    {
        if (poweredSystem != 1)
        {
            thrusterBottom.GetComponent<SpriteRenderer>().enabled = false;
            thrusterTop.GetComponent<SpriteRenderer>().enabled = false;
            thrusterRight.GetComponent<SpriteRenderer>().enabled = false;
            thrusterLeft.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (poweredSystem != 3)
            CheckShield();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Asteroid" && poweredSystem != 3)
        {
            TakeDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Collectable")
        {
            GameManager.instance.driveNumber++;
            Destroy(other.gameObject);
        }

        if (other.tag == "Victory")
        {
            Debug.Log("You Win!");
            SceneManager.LoadScene(2);
        }
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }

    private void TakeDamage()
    {
        o2Level -= 20;
    }

    private void HealthFlash()
    {
        if (Time.fixedTime % 0.9 < 0.2)
        {
            slider.gameObject.SetActive(false);
        }
        else
        {
            slider.gameObject.SetActive(true);
        }
    }

    private void CheckShield()
    {
        if (shield.activeSelf)
        {
            shield.SetActive(false);
            shield.transform.localScale = new Vector3(0.7f, 0.7f);
        }
    }

    private IEnumerator ScaleOverTime()
    {
        Vector3 originalScale = new Vector3(0.7f, 0.7f);
        Vector3 fullScale = new Vector3(1.0f, 1.0f);
        float currentTime = 0.0f;
        float time = 0.1f;
        isScaling = true;

        do
        {
            shield.transform.localScale = Vector3.Lerp(originalScale, fullScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);
        isScaling = false;
    }
}
