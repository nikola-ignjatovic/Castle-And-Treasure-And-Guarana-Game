using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    private float m_delayToIdle = 0.0f;
    private bool m_grounded;
    private Animator m_animator;
    private Rigidbody2D m_body2d;

    private int m_currentAttack = 0;
    private float m_timeSinceAttack = 0.0f;

    private GameObject thisEnemy;
    private int DifficultyModifier;
    public float speed = 2.5f;
    public float speedSave;
    private int AttackDelay = 1;
    private int EnemyHealth = 2;
    private int currentPathIndex;
    public bool isMoving;
    private List<Vector3> pathVectorList;

    private GameObject InSwordRangeDetector;
    private GameObject checkIfHouseIsBehindDetector;

    private CharacterMovement OnDamaged; // Will be called when enemy (this object) damages Player
    private InRangeOfAttackEnemy WhoIsDamaged; // Will be used to decide who did enemy hit
    private int x;
    private MapGenerator NumberOfEnemiesScript;
    private int numberOfEnemies;
    private HouseController OnHouseDamaged;
    private GameObject child0;
    private GameObject child1;
    private InRangeOfAttackPlayer InRangeOfAttackPlayerScript;

    void Start()
    {
        InRangeOfAttackPlayerScript = GameObject.Find("InRangeOfAttackPlayerSprite").GetComponent<InRangeOfAttackPlayer>();
        child0 = this.gameObject.transform.GetChild(0).gameObject;
        child1 = this.gameObject.transform.GetChild(1).gameObject;
        checkIfHouseIsBehindDetector = this.gameObject.transform.GetChild(1).gameObject;
        NumberOfEnemiesScript = GameObject.Find("GameController").GetComponent<MapGenerator>();

        OnHouseDamaged = GameObject.Find("House").GetComponent<HouseController>();
        OnDamaged = GameObject.Find("Player").GetComponent<CharacterMovement>();

        isMoving = false;
        InSwordRangeDetector = this.gameObject.transform.GetChild(0).gameObject;
        WhoIsDamaged = this.gameObject.transform.GetChild(0).gameObject.GetComponent<InRangeOfAttackEnemy>();
        DifficultyModifier = PlayerPrefs.GetInt("Difficulty");


        // Setting Stats of AI based on modifiers
        speed = speed * (DifficultyModifier / 1.5f);
        speedSave = speed;
        AttackDelay = AttackDelay / DifficultyModifier;
        EnemyHealth = EnemyHealth * DifficultyModifier;

        m_animator = this.gameObject.GetComponent<Animator>();

        m_body2d = this.gameObject.GetComponent<Rigidbody2D>();

        m_grounded = true;
        m_animator.SetBool("Grounded", m_grounded);

        thisEnemy = this.gameObject;


    }

    void Update()
    {
        m_timeSinceAttack += Time.deltaTime;
        // Run Animation
        if (isMoving)
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
            {
                m_animator.SetInteger("AnimState", 0);
            }
        }
        HandleMovement();
    }
    public void HandleMovement()
    {
        if (pathVectorList != null)
        {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            if (Vector3.Distance(transform.position, targetPosition) > 1f)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;
                float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                thisEnemy.transform.position = transform.position + moveDir * speed * Time.deltaTime;

                //Rotating Sprite and SideTracker
                if (moveDir.x>0)
                {
                    checkIfHouseIsBehindDetector.transform.localPosition = new Vector3(-0.76f, 0.64f, 0);
                    InSwordRangeDetector.transform.localPosition= new Vector3(0.3f, 0.68f, 0);
                    //EnemySideController.RotateRight();
                    GetComponent<SpriteRenderer>().flipX = false;
                }
                else if (moveDir.x<0)
                {
                    checkIfHouseIsBehindDetector.transform.localPosition = new Vector3(0.76f, 0.64f, 0);
                    InSwordRangeDetector.transform.localPosition = new Vector3(-0.3f, 0.68f, 0);
                    //EnemySideController.RotateLeft();
                    GetComponent<SpriteRenderer>().flipX = true;
                }
            }
            else
            {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count)
                {
                    StopMoving();
                }
            }
        }
        else
        {

        }
    }
    public void Attack()
    {
        if (m_timeSinceAttack > 1f/DifficultyModifier)
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

            if (WhoIsDamaged.nameOfAttackedObject == "Player") // Hurting/Killing Player if he is damaged
            {
                OnDamaged.HurtOrDieFunction();
                WhoIsDamaged.nameOfAttackedObject = "";
            }
            else if (WhoIsDamaged.nameOfAttackedObject == "House") // Hurting/Killing Player if he is damaged
            {
                OnHouseDamaged.HouseHurtOrDieFunction();
                WhoIsDamaged.nameOfAttackedObject = "";
            }
            else if (WhoIsDamaged.enemyTag == "DestructibleObsticle")
            {
                GameObject.Find(WhoIsDamaged.nameOfAttackedObject).GetComponent<DestructibleObsticleController>().DestroyDestructibleObsticle();
                WhoIsDamaged.enemyTag = "";
                WhoIsDamaged.nameOfAttackedObject = "";
            }

            // Reset timer
            m_timeSinceAttack = 0.0f;
        }
    }
    private void StopMoving()
    {
        pathVectorList = null;
        isMoving = false;
        speed = 0;
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }
    public void SetTargetPosition(Vector3 targetPosition)
    {
        currentPathIndex = 0;
        pathVectorList = PathFinding.Instance.FindPath(GetPosition(), targetPosition);
        if (pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
            pathVectorList = PathFinding.Instance.FindPath(GetPosition(), targetPosition);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBlade")
        {
            if (EnemyHealth > 1)
            {
                Hurt();
            }
            else
            {
                Death();
            }
        }
        else
        {

        }
    }
    public void HurtOrDieFunction()
    {
        if (m_animator.GetBool("IdleBlock") == true)
        {

        }
        else
        {
            if (EnemyHealth > 1)
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
        EnemyHealth--;
        m_animator.SetTrigger("Hurt");
    }
    private void Death()
    {
        numberOfEnemies = NumberOfEnemiesScript.amountOfEnemies;
        NumberOfEnemiesScript.amountOfEnemies--;
        if (numberOfEnemies == 1)
        {
            m_animator.SetTrigger("Death");
            Destroy(InSwordRangeDetector);
            Destroy(child0);
            Destroy(child1);
            Destroy(this.gameObject.GetComponent<BoxCollider2D>());
            Destroy(this.gameObject.GetComponent<Testing>());
            InRangeOfAttackPlayerScript.enemyTag = "";
            InRangeOfAttackPlayerScript.nameOfAttackedObject = "";
            StartCoroutine(VictorySceenWait());
            
        }
        else
        {
            m_animator.SetTrigger("Death");
            Destroy(InSwordRangeDetector);
            Destroy(child0);
            Destroy(child1);
            Destroy(this.gameObject.GetComponent<BoxCollider2D>());
            Destroy(this.gameObject.GetComponent<Testing>());
            InRangeOfAttackPlayerScript.enemyTag = "";
            InRangeOfAttackPlayerScript.nameOfAttackedObject = "";
            Destroy(this.gameObject.GetComponent<EnemyController>());        
            
        }
    }

    private IEnumerator VictorySceenWait()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("VictoryScene");
    }

}
