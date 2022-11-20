using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayWall : MonoBehaviour
{
    private PlayerMovement pm;
    [SerializeField] int orientation;

    private void Awake()
    {
        if (transform.parent.rotation.eulerAngles.z > 0)
            orientation = -1;
        else
        {
            orientation = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (!pm)
                pm = col.gameObject.GetComponent<PlayerMovement>();

            if (pm.Direction == orientation)
                pm.SwapActive = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!pm)
                pm = other.gameObject.GetComponent<PlayerMovement>();

            if (pm.Direction == orientation)
                pm.SwapActive = true;
        }
    }
}