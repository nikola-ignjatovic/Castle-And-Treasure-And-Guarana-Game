using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckingForCollision : MonoBehaviour
{
    public bool Collision;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Collision);
    }
    void Update()
    {
        Debug.Log(Collision);
    }
    // Update is called once per frame

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Collision = true;
    }
}
