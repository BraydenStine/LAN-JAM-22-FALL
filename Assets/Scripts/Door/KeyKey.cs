using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyKey : MonoBehaviour
{
    public KeyManager KM;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            KM.GetKey();
            gameObject.SetActive(false);
        }
    }
}
