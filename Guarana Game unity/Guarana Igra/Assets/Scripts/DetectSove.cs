using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectSove : MonoBehaviour
{
    public bool Cooldown;
    public GameObject bcl;
    private BullControler bc;
    public float cl;
    private bool dash;
    public bool Walls=false;
    public int a;
    public OwlMovement pomeranje;
    // Start is called before the first frame update
    void Start()
    {
        bc = bcl.GetComponent<BullControler>();
        Cooldown = true;
    }

    // Update is called once per frame
    void Update()
    {
        Function();
        Debug.Log(Cooldown);
        if (cl > 0)
        {
            cl -= 1 * Time.deltaTime;
        }

        else
        {
            Cooldown = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (a == 0)
        {
            if (!Walls)
            {
                if (collision.gameObject.name == "Sova")
                {
                    StartCoroutine(pomeranje.SpawnGuarana());
                    Walls = true;
                    a++;
                }
            }
        }
            if (collision.gameObject.name == "Sova")
            {
                dash = true;
            }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.name == "Sova")
        {
            dash = false;
        }
    }
    private void Function()
    {
        if (dash)
        {
            if (Cooldown)
            {
                bc.Charge();
                Cooldown = false;
                cl = 2;
            }
        }
    }

}
