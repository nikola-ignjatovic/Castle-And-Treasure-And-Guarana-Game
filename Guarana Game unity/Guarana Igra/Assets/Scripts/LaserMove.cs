using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMove : MonoBehaviour
{
    public float MovementSpeed;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * MovementSpeed;
        StartCoroutine(DestroyAfterFiveSec());
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="DestroyableWall")
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.name!="Sova")
        {
            Destroy(this.gameObject);
        }
    }
    IEnumerator DestroyAfterFiveSec()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
}
