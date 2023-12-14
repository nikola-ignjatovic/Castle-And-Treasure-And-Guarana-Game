using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthResetScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Health", 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
