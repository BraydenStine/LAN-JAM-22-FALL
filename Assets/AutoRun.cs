using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AutoRun : MonoBehaviour
{
    [SerializeField] private PlayerMovement pm;
    

    private void OnTriggerEnter(Collider other)
    {
        pm.SwapDirection();
    }
}