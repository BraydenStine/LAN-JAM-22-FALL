// Alex Jalonen - 3/19/22
// Swarm controller pushes the swarm center towards the player using rb2d

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SwarmController : MonoBehaviour
{
    #region Internal Ref
    Rigidbody2D rb;
    GameObject player;
    #endregion

    #region Inspector Set Var
    public float playerPullMagnitude = 5;
    public float outerRadius = 20f;

    public float avoidCenterMag = 5f;
    public float outsideRadiusVelocity = 3f;
    public float breakingMag = 1f;

    public bool ignoreSeperation = false;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        var vectorToPlayer = (player.transform.position - transform.position);
        var dirToPlayer = vectorToPlayer.normalized;
        var distanceToPlayer = vectorToPlayer.magnitude;

        if (distanceToPlayer < 1f)
        {
            rb.AddForce(-dirToPlayer * avoidCenterMag);
        }

        if (distanceToPlayer > outerRadius)
        {
            var correctionVelocity = dirToPlayer * outsideRadiusVelocity * Time.deltaTime;
            rb.velocity = new Vector2(rb.velocity.x + correctionVelocity.x, rb.velocity.y + correctionVelocity.y);
        }

        if (distanceToPlayer < 5)
        {
            rb.drag = .1f;
        }
        else if (distanceToPlayer < outerRadius)
        {
            rb.drag = ((1-(distanceToPlayer / outerRadius))*breakingMag) + .05f;
        }
        else if(distanceToPlayer > outerRadius*2)
        {
            rb.drag = 2f;
        }
        else
        {
            rb.drag = .05f;
        }

        rb.AddForce(dirToPlayer * playerPullMagnitude);

        if(!ignoreSeperation)
        {
            var swarms = FindObjectsOfType<SwarmController>();
            foreach(var swarm in swarms)
            {
                if (this == swarm) continue;
                var diff = transform.position - swarm.transform.position;

                if(diff.magnitude < 3f)
                {
                    rb.AddForce(diff.normalized * ((1 - (diff.magnitude / 3f)) * 3));
                }
            }
        }

    }


}
