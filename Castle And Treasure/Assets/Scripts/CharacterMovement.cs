using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterMovement : MonoBehaviour
{
    public float m_speed = 3.5f;
    public float m_rollForce = 6.0f;
    [SerializeField] bool m_noBlood = false;

    private float speedSave;
    private int Health = 5;
    private TMP_Text HealthText;
    private bool m_grounded;
    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private bool m_rolling = false;
    private int m_facingDirection = 1;
    private int m_currentAttack = 0;
    private float m_timeSinceAttack = 0.0f;
    private float m_delayToIdle = 0.0f;

    private int alreadyBlocking;

    private EnemyController OnDamaged; // Will be called when Player damages Enemy
    private InRangeOfAttackPlayer WhoIsDamaged; // Will be used to see what is damaged
    private GameObject InSwordRangeDetector;


    private Button AttackButton;
    private MyButton BlockButton;
    public bool attackButtonState;
    public bool blockButtonState;


    protected Joystick joystick;
    protected FloatingJoystick FloatingJoystickScript;

    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        FloatingJoystickScript = FindObjectOfType<FloatingJoystick>();
        speedSave = m_speed;

        InSwordRangeDetector = this.gameObject.transform.GetChild(1).gameObject;
        WhoIsDamaged = this.gameObject.transform.GetChild(1).gameObject.GetComponent<InRangeOfAttackPlayer>();

        HealthText = GameObject.Find("HealthText").GetComponent<TMP_Text>();

        HealthText.text = "" + Health;

        m_animator = this.gameObject.GetComponent<Animator>();

        m_body2d = this.gameObject.GetComponent<Rigidbody2D>();

        m_grounded = true;
        m_animator.SetBool("Grounded", m_grounded);

        BlockButton = GameObject.Find("BlockButton").GetComponent<MyButton>();
        AttackButton = GameObject.Find("AttackButton").GetComponent<Button>();
        AttackButton.onClick.AddListener(ChangeAttackButtonState);
    }

    void Update()
    {
        m_body2d.velocity = new Vector2(joystick.Horizontal * m_speed,joystick.Vertical*m_speed);
        if (m_body2d.IsSleeping())
        {
            m_body2d.WakeUp();
        }
        // Increase timer that controls attack combo
        m_timeSinceAttack += Time.deltaTime;

        float inputY = Input.GetAxis("Vertical");

        // Swapping direction of sprite
        float inputX = Input.GetAxis("Horizontal");

        if (joystick.Horizontal > 0) // Swap joystick.Horizontal with inputX for keyboard movement
        {
            InSwordRangeDetector.transform.localPosition = new Vector3(0.41f, 0.68f, 0);
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
        }

        else if (joystick.Horizontal < 0)
        {
            InSwordRangeDetector.transform.localPosition = new Vector3(-0.41f, 0.68f, 0);
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;
        }

        // Moving
        //m_body2d.velocity = new Vector2(inputX * m_speed, inputY * m_speed);  KeyBoard controlls :)

        Debug.Log(blockButtonState);
        if (BlockButton.buttonPressed==true)
        {
            if (alreadyBlocking != 1)
            {
                m_animator.SetTrigger("Block");
                m_animator.SetBool("IdleBlock", true);
                m_speed = 0f;
                alreadyBlocking = 1;
            }
        }

        else
        {
            m_animator.SetBool("IdleBlock", false);
            m_speed = speedSave;
            alreadyBlocking = 0;
        }

        // Attacking
         if (attackButtonState==true && m_timeSinceAttack > 0.5f)
        {
            m_currentAttack++;

            // Loop back to one after third attack
            if (m_currentAttack > 3)
                m_currentAttack = 1;

            // Reset Attack combo if time since last attack is too large
            if (m_timeSinceAttack > 1.0f)
                m_currentAttack = 1;

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            m_animator.SetTrigger("Attack" + m_currentAttack);

            if (WhoIsDamaged.enemyTag == "Enemy")
            {
                GameObject.Find(WhoIsDamaged.nameOfAttackedObject).GetComponent<EnemyController>().HurtOrDieFunction();
                WhoIsDamaged.enemyTag = "";
                WhoIsDamaged.nameOfAttackedObject = "";
            }
            if (WhoIsDamaged.enemyTag == "DestructibleObsticle")
            {
                GameObject.Find(WhoIsDamaged.nameOfAttackedObject).GetComponent<DestructibleObsticleController>().DestroyDestructibleObsticle();
                WhoIsDamaged.enemyTag = "";
                WhoIsDamaged.nameOfAttackedObject = "";
            }

            // Reset timer
            attackButtonState = false;
            m_timeSinceAttack = 0.0f;
        }


        // Run Animation
        else if (m_body2d.velocity.magnitude > Mathf.Epsilon)
        {
            // Reset timer
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }

        // Idle
        else
        {
            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
            if (m_delayToIdle < 0)
                m_animator.SetInteger("AnimState", 0);
        }
    }

    public void HurtOrDieFunction()
    {
        if (m_animator.GetBool("IdleBlock") == true)
        {

        }
        else
        {
            if (Health > 1)
            {
                Hurt();
            }
            else
            {
                Death();
            }
        }
    }
    private void Hurt()
    {
        Health--;
        m_animator.SetTrigger("Hurt");
        HealthText.text = "" + Health;
    }
    private void Death()
    {
        HealthText.text = "0";
        Destroy(m_body2d);
        Destroy(this.gameObject.GetComponent<BoxCollider2D>());
        m_animator.SetTrigger("Death");
        StartCoroutine(DeathWait());
    }
    private IEnumerator DeathWait()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("LoseScene");
    }
    private void ChangeAttackButtonState()
    {
        attackButtonState = true;
    }
    public void Blocking()
    {
        Debug.Log("aa");
        blockButtonState = true;
    }
    public void NotBlocking()
    {
        Debug.Log("b");
        //blockButtonState = false;
    }
}
