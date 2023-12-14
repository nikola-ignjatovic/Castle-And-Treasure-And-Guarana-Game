using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallActivator : MonoBehaviour
{
    private DetectSove detectSove;
    public GameObject ovosranje;
    // Start is called before the first frame update
    void Start()
    {
        detectSove = ovosranje.GetComponent<DetectSove>();
    }

    // Update is called once per frame
    void Update()
    {
        if(detectSove.Walls==true)
        {
            this.gameObject.GetComponent<BoxCollider2D>().enabled= true;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
