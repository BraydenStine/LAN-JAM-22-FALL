using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorController : MonoBehaviour
{
    public KeyManager KM;

    public TMP_Text DoorText;

    public GameObject Door;

    public AudioSource AudioPlayer;

    public int DoorCost;

    // Update is called once per frame
    void Update()
    {
        DoorText.text = KM.KeyCount + "/" + DoorCost;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(KM.KeyCount >= DoorCost)
            {
                KM.KeyCount -= DoorCost;
                AudioPlayer.Play();
                Door.SetActive(false);
            }
        }
    }
}
