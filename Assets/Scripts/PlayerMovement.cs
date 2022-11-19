using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D _body;
    SpriteRenderer _rend;
    BoxCollider2D _box;
    PlayerStats _stats;

    [SerializeField]
    float moveSpeed = 6f;
    [SerializeField]
    float maxSpeed = 10f;
    [SerializeField]
    float jumpForce = 9f;

    [SerializeField]
    float airDampening = 100f;

    [SerializeField]
    bool grounded = false;
    bool isJumping = false;


    float directionX = 0f;
    float directionY = 0f;

    [SerializeField]
    float jumpTime = 0.35f;
    float jumpTimeCounter;

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _box = GetComponent<BoxCollider2D>();
        _rend = GetComponentInChildren<SpriteRenderer>();
        _stats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_stats.CurrentHealth <= 0) return;

        directionY = _body.velocity.y;
        if (grounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;

            directionY = jumpForce;
        }
        else if(isJumping && Input.GetButton("Jump"))
        {
            if (jumpTimeCounter > 0)
            {
                directionY = jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
                isJumping = false;
        }
        if (Input.GetButtonUp("Jump"))
            isJumping = false;
    }

    private void FixedUpdate()
    {
        if (_stats.CurrentHealth <= 0)
        {
            _body.velocity = new Vector2();
            return;
        }

        directionX = Input.GetAxis("Horizontal") * Time.deltaTime * 100f;


        //checks direction to get the player to flip
        if (directionX < 0)
            _rend.flipX = false;
        else if(directionX > 0)
            _rend.flipX = true;


        //Does a box cast down to check if grounded for jumping
        Vector3 max = _box.bounds.max;
        Vector3 min = _box.bounds.min;
        Vector2 corner1 = new Vector2(max.x-0.2f, min.y-0.1f);
        Vector2 corner2 = new Vector2(min.x-0.2f, min.y-0.2f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

        grounded = false;

        //if the box was able to collide whit anything, its grounded
        if (hit)
        {
            grounded = true;
        }

        if (grounded) //can move normally on the ground
            directionX *= moveSpeed;
        else
            directionX *= (moveSpeed /airDampening);


        _body.velocity = new Vector2(Mathf.Clamp(directionX, -maxSpeed, maxSpeed),
            Mathf.Clamp(directionY, -maxSpeed * 10f, maxSpeed * 10f));
    }
}
