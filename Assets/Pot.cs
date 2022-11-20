using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Pot : MonoBehaviour
{
    [SerializeField]private BoxCollider2D _collider;
    private PlayerMovement pm;
    private bool breakable= false;
    public Sprite _breakSprite;
    public SpriteRenderer _Renderer;
    public GameObject key;



    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
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
        yield return new WaitForSeconds(1.5f);
        _collider.enabled = true;
        breakable = true;
        _Renderer.sprite = _breakSprite;
        key.SetActive(true);
    }
}
