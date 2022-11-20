using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AutoRun : MonoBehaviour
{
    [SerializeField] private PlayerMovement pm;
    



    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        pm.SwapDirection();
    }
}