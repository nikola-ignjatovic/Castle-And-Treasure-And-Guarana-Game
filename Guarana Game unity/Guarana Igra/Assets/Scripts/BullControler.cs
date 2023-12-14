using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullControler : MonoBehaviour
{
    public float Speed;
    private Rigidbody2D rb;
    private int k;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        k = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Charge()
    {
        Debug.Log(transform.right * Speed);
        rb.velocity = transform.right * Speed;
    }
    public void Rotate()
    {
        rb.velocity = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.Euler(0, 180 * k, 0);
        k++;
        k %= 2;
    }
}
