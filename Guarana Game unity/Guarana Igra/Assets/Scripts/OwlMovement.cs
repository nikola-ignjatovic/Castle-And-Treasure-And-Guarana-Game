using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class OwlMovement : MonoBehaviour
{
    public float MovementSpeed;
    public float JumpForce;
    public float GuaranaSpeedBoost;
    public float SpeedGuaranaSpeedBoostDuration;
    public float DoubleJumpDuration;
    public GameObject EmptyGameObject;
    public GameObject Laser;
    public GameObject LaserMask;
    public Slider Budometar;
    public float TimerMax;
    public Image image;
    public TMP_Text HealthText;
    public int SecondsOfBlackScreen;
    public int TimerMax2;
    public GameObject SleepingMask;
    public GameObject LaserMask2;
    public GameObject SleepingMask2;
    public DetectSove DC;
    public GameObject guaranaRed;
    public GameObject OK;

    private Rigidbody2D rb;
    private int AmmoNumber = 0;
    private Color tempColor;

    private int CanJump = 1;
    private bool CanDoubleJump = false;
    private bool DidObjectTouchTheGround = true;
    private bool IsSleeping;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(SpawnGuarana());
       // Instantiate(guaranaRed, OK.transform.position, OK.transform.rotation);
        //Instantiate(guaranaRed, OK.transform.position, OK.transform.rotation);
        HealthText.text = "X" + PlayerPrefs.GetInt("Health");
        Budometar.value = 1;
        rb = GetComponent<Rigidbody2D>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
        StartCoroutine(SliderDecrease());
    }

    // Update is called once per frame
    void Update()
    {
        HealthText.text = "X" + PlayerPrefs.GetInt("Health");
        if (PlayerPrefs.GetInt("Health") == 0)
        {
            SceneManager.LoadScene("EndScene");
        }
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1 - Budometar.value);

        if (AmmoNumber <= 0 || IsSleeping)
        {
            LaserMask.active = false;
            LaserMask2.active = false;
        }
        else
        {
            LaserMask.active = true;
            LaserMask2.active = true;
        }
        if (!IsSleeping)
        {
            if (Input.GetKeyDown(KeyCode.H)) //shooting
            {
                if (AmmoNumber > 0)
                {
                    Instantiate(Laser, EmptyGameObject.transform.position, EmptyGameObject.transform.rotation);
                    AmmoNumber--;
                }
            }

            float MoveSpeedo = Input.GetAxis("Horizontal") * MovementSpeed;
            rb.velocity = new Vector2(MoveSpeedo, rb.velocity.y);
            if (MoveSpeedo > 0)
            {
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (MoveSpeedo < 0)
            {
                this.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (CanJump > 0)
                {
                    DidObjectTouchTheGround = false;
                    CanJump -= 1;
                    rb.velocity = new Vector2(rb.velocity.x, JumpForce);
                }
            }// Jumping
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") //If Can jump
        {
            if (!DidObjectTouchTheGround)
            {
                if (CanDoubleJump)
                    CanJump += 2;
                else
                    CanJump += 1;
                DidObjectTouchTheGround = true;
            }
        }
        if (collision.gameObject.name == "Sod")
        {
            PlayerPrefs.SetInt("Health", PlayerPrefs.GetInt("Health") - 1);
            this.gameObject.transform.position = new Vector3(-13, 0, 1);
            Budometar.value = 1;
        }
        if (collision.gameObject.tag == "Door1")
        {
            SceneManager.LoadScene("Level2");
        }
        if (collision.gameObject.tag == "Door2")
        {
            SceneManager.LoadScene("Level3");
        }
        if (collision.gameObject.tag == "Door3")
        {
            SceneManager.LoadScene("BullScene");
        }
        if (collision.gameObject.name == "ZenskaSovica")
        {
            SceneManager.LoadScene("VictoryScene");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RedGuarana")
        {
            Destroy(collision.gameObject);
            Budometar.value = 1f;
            AmmoNumber += 5;
        }
        if (collision.gameObject.tag == "GreenGuarana")
        {
            Destroy(collision.gameObject);
            Budometar.value = 1f;
            MovementSpeed += GuaranaSpeedBoost;
            StartCoroutine(ResetMovmentSpeed());

        }
        if (collision.gameObject.tag == "BlueGuarana")
        {
            Destroy(collision.gameObject);
            Budometar.value = 1f;
            CanDoubleJump = true;
            StartCoroutine(RemoveDoubleJump());
            if (CanJump > 0)
                CanJump++;
        }
        if (collision.gameObject.name == "RedBull")
        {
            PlayerPrefs.SetInt("Health", PlayerPrefs.GetInt("Health") - 1);
            DC.Walls = false;
            DC.a = 0;
            this.gameObject.transform.position = new Vector3(-13, 0, 1);
            Budometar.value = 1;
        }
    }

    IEnumerator ResetMovmentSpeed()
    {
        yield return new WaitForSeconds(SpeedGuaranaSpeedBoostDuration);
        MovementSpeed -= GuaranaSpeedBoost;
    }
    IEnumerator RemoveDoubleJump()
    {
        yield return new WaitForSeconds(DoubleJumpDuration);
        CanDoubleJump = false;
    }

    IEnumerator AwakeOwl()
    {
        IsSleeping = true;
        this.gameObject.transform.rotation = Quaternion.Euler(0, 0, -90);
        LaserMask.active = false;
        LaserMask2.active = false;
        SleepingMask.active = true;
        SleepingMask2.active = true;
        PlayerPrefs.SetInt("Health", PlayerPrefs.GetInt("Health") - 1);
        yield return new WaitForSeconds(SecondsOfBlackScreen);
        while (Budometar.value < 1)
        {
            Budometar.value += Time.deltaTime / TimerMax2;
            yield return null;
        }
        this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        SleepingMask.active = false;
        SleepingMask2.active = false;
        IsSleeping = false;
        StartCoroutine(SliderDecrease());
    }
    private IEnumerator SliderDecrease()
    {
        while (Budometar.value > 0)
        {
            Budometar.value -= Time.deltaTime / TimerMax;

            yield return null;
        }
        StartCoroutine(AwakeOwl());
    }
  public IEnumerator SpawnGuarana()
    {
        yield return new WaitForSeconds(5);
        Instantiate(guaranaRed,OK.transform.position,OK.transform.rotation);
        StartCoroutine(SpawnGuarana());
    }
}
