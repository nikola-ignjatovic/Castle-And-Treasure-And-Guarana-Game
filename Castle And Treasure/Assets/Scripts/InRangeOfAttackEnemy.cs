using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRangeOfAttackEnemy : MonoBehaviour
{
    public EnemyController EnemyControllerScript;
    public string nameOfAttackedObject;
    private Rigidbody2D m_body;
    public string enemyTag;

    void Start()
    {
        m_body = this.gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (m_body.IsSleeping())
        {
            m_body.WakeUp();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            nameOfAttackedObject = "Player";
            EnemyControllerScript.Attack();
        }
        else if(collision.gameObject.name == "House")
        {
            nameOfAttackedObject = "House";
            EnemyControllerScript.Attack();
        }
        else if (collision.gameObject.tag == "DestructibleObsticle")
        {
            enemyTag = collision.gameObject.tag;
            nameOfAttackedObject = collision.gameObject.name;
            EnemyControllerScript.Attack();
        }
        else
        {

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            nameOfAttackedObject = "";
        }
        else if (collision.gameObject.name=="House")
        {
            nameOfAttackedObject = "";
        }
        else if (collision.gameObject.tag == "DestructibleObsticle")
        {
            enemyTag = "";
            nameOfAttackedObject = "";
        }
        else
        {

        }
    }
}
