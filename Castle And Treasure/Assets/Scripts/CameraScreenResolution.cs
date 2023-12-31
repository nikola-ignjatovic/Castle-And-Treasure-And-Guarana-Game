﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraScreenResolution : MonoBehaviour
{
    public float fWidth = 9.0f;  // Desired width 

    void Start()
    {
        float fT = fWidth / Screen.width * Screen.height;
        fT = fT / (2.0f * Mathf.Tan(0.5f * Camera.main.fieldOfView * Mathf.Deg2Rad));
        Vector3 v3T = Camera.main.transform.position;
        v3T.z = -fT;
        transform.position = v3T;
    }
}