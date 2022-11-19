// Alex Jalonen 3-19-22
// controls player health and collections

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    private bool invulnerable = false;
    KeyManager keyManager;
    AudioSource audioSource;
    public AudioClip realEnd;
    public AudioClip tadaEnd;
    public AudioClip ouchSound;

    public int CurrentHealth
    {
        get
        {
            return currentHealth;
        }
        set
        {
            if (invulnerable) return;
            
            if (value >= 3)
                HealthDisplay.SetHeartColor(healthDisplay.heartThree, Color.red);
            else
                HealthDisplay.SetHeartColor(healthDisplay.heartThree, Color.black);

            if (value >= 2)
                HealthDisplay.SetHeartColor(healthDisplay.heartTwo, Color.red);
            else
                HealthDisplay.SetHeartColor(healthDisplay.heartTwo, Color.black);

            if (value >= 1)
                HealthDisplay.SetHeartColor(healthDisplay.heartOne, Color.red);
            else
                HealthDisplay.SetHeartColor(healthDisplay.heartOne, Color.black);

            if (value <= 0)
            {
                deathDisplay.SetActive(true);
                SetInvulnerable(5f);
                Invoke(nameof(ResetPlayer), 5f);

                var image = deathDisplay.GetComponent<Image>();
                image.color = Color.clear;
                image.DOColor(deathBG, 2f);

                var text = deathDisplay.GetComponentInChildren<TextMeshProUGUI>();
                text.color = Color.clear;
                text.DOColor(Color.red, 2f);
                if(value <0)
                {
                    audioSource.pitch = 1f;
                    audioSource.volume = 70f;
                    audioSource.PlayOneShot(tadaEnd);
                }
                else
                {
                    audioSource.pitch = 1.5f;
                    audioSource.PlayOneShot(realEnd);
                }
                
            }
            else 
            {
                if (value < currentHealth)
                {
                    audioSource.pitch = Random.Range(0.75f, 2f);
                    audioSource.PlayOneShot(ouchSound);
                    SetInvulnerable(invulnerableTimer);
                }
            }

            currentHealth = value;
        }
    }
    int currentHealth = 0;

    public int startingHealth = 3;
    public float invulnerableTimer = 3f;
    public HealthDisplay healthDisplay;
    public GameObject deathDisplay;
    public Color deathBG;

    public Vector3 startingPos;

    private void Start()
    {
        CurrentHealth = startingHealth;
        startingPos = transform.position;
        keyManager = FindObjectOfType<KeyManager>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F12))
        {
            ResetPlayer();
        }
    }


    private void SetInvulnerable(float timer)
    {
        invulnerable = true;
        Invoke(nameof(Vulnerable), timer);

        var sprite = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        if(sprite != null)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, .5f);
        }
    }

    private void Vulnerable()
    {
        invulnerable = false;

        var sprite = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
        }
    }


    private void ResetPlayer()
    {
        //reset goes here
        invulnerable = false;
        transform.position = startingPos;
        deathDisplay.SetActive(false);
        CurrentHealth = 3;

        // reset bees and keys
        // keyManager.Restart();
        SceneManager.LoadScene(1);
    }
}
