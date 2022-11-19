using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public int KeyCount = 0;
    AudioSource source;
    [SerializeField]
    AudioClip[] clips;

    KeyKey[] keykeykeys;
    DoorController[] doors;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        keykeykeys = FindObjectsOfType<KeyKey>();
        doors = FindObjectsOfType<DoorController>();

    }

    public void Restart()
    {
        foreach( KeyKey key in keykeykeys)
        {
            key.gameObject.SetActive(true);
        }
        foreach(DoorController door in doors)
        {
            door.gameObject.SetActive(true);
        }
        KeyCount = 0;
    }

    public void GetKey()
    {
        source.PlayOneShot(clips[Random.Range(0,3)]);

        ++KeyCount;
    }
}
