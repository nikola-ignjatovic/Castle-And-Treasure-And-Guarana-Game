using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRangeOfAttackPlayer : MonoBehaviour
{
    public string nameOfAttackedObject;
    public string enemyTag;
    private Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.gameObject.GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (rigidbody.IsSleeping())
        {
            rigidbody.WakeUp();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.name == "Enemy0")
            {
                Debug.Log(collision.gameObject.name);
                enemyTag = collision.gameObject.tag;
                nameOfAttackedObject = "Enemy0";
            }
            else if(collision.gameObject.name=="Enemy1")
            {
                Debug.Log(collision.gameObject.name);
                enemyTag = collision.gameObject.tag;
                nameOfAttackedObject = "Enemy1";
            }
            else if (collision.gameObject.name == "Enemy2")
            {
                Debug.Log(collision.gameObject.name);
                enemyTag = collision.gameObject.tag;
                nameOfAttackedObject = "Enemy2";
            }
            else if (collision.gameObject.name == "Enemy3")
            {
                Debug.Log(collision.gameObject.name);
                enemyTag = collision.gameObject.tag;
                nameOfAttackedObject = "Enemy3";
            }
            else
            {

            }
        }
        else if (collision.gameObject.tag == "DestructibleObsticle")
        {
            enemyTag = collision.gameObject.tag;
            nameOfAttackedObject = collision.gameObject.name;
        }
        else
        {

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemyTag = "";
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
