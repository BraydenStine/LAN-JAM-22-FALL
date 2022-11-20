using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Pot : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField, Range(0.5f, 1f)] private float delayTime;
    private PlayerMovement pm;
    private Rigidbody2D body;
    private AudioSource audio;
    private bool breakable = false;
    public Sprite _breakSprite;
    public SpriteRenderer _Renderer;
    public GameObject key;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (!breakable)
                StartCoroutine(Falling());
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (breakable)
        {
            //break
        }
    }

    IEnumerator Falling()
    {
        _collider.enabled = false;
        yield return new WaitForSeconds(delayTime);
        _collider.enabled = true;
        breakable = true;
        audio.pitch = Random.Range(0.8f, 1.2f);
        audio.Play();
        _Renderer.sprite = _breakSprite;
        key.SetActive(true);
        yield return new WaitForSeconds(1f);
        _Renderer.enabled = false;
        body.isKinematic = true;
        _collider.enabled = false;
    }
}