using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    MeshRenderer myrenderer;
    Rigidbody rb;
    [SerializeField] float timeToWait = 3f;
    
    void Start()
    {
        myrenderer = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
        
        myrenderer.enabled = false;
        rb.useGravity = false;
        
    }

    void Update()
    {
        if (Time.time > timeToWait)
        {
            myrenderer.enabled = true;
            rb.useGravity = true;
        }
    }
}
