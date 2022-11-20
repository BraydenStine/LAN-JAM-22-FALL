using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    public GameObject BEEZ;

    public PlayerStats Health;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            BEEZ.SetActive(true);
            Health.CurrentHealth = -1;
        }
    }
}
