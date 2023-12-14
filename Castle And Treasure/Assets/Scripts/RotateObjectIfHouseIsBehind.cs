using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjectIfHouseIsBehind : MonoBehaviour
{
    public EnemyController EnemyControllerScript;
    public GameObject InSwordRangeDetector;
    private Rigidbody2D m_body;
    public InRangeOfAttackEnemy InRangeOfAttackEnemy;

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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "House" || collision.gameObject.name=="Player") // Rotating if house is behind or if player try to sneak up on him
        {
            if (InRangeOfAttackEnemy.nameOfAttackedObject != "House")
            {
                if (EnemyControllerScript.GetComponent<SpriteRenderer>().flipX == true)
                {
                    InSwordRangeDetector.transform.localPosition = new Vector3(0.3f, 0.68f, 0);
                    EnemyControllerScript.GetComponent<SpriteRenderer>().flipX = false;
                }
                else
                {
                    InSwordRangeDetector.transform.localPosition = new Vector3(-0.3f, 0.68f, 0);
                    EnemyControllerScript.GetComponent<SpriteRenderer>().flipX = true;
                }
                this.gameObject.transform.localPosition = new Vector3(-this.gameObject.transform.localPosition.x, this.gameObject.transform.localPosition.y, 0);
            }
        }
        else
        {

        }
    }
}
