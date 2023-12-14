using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform b;
    private Transform g;
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        g = this.gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        g.transform.position = new Vector3(b.transform.position.x, 0, -10);
        g.transform.position = Vector3.Lerp(g.transform.position, new Vector3(g.transform.position.x,b.transform.position.y+5,g.transform.position.z), time);
    }

}
