// Alex Jalonen - 3/19/22
// This script controls the danger that the swarm presents to the player, will move to individual Bees in second pass

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class SwarmDanger : MonoBehaviour
{
    #region Internal Ref
    GameObject swarmDiameterGraphic;
    CircleCollider2D circleCollider;
    #endregion

    #region Inspector Set Var
    public float swarmDiameter = 2f;
    private float prevSwarmDiameter = 2f;
    #endregion

    private void Awake()
    {
        swarmDiameterGraphic = transform.Find("SwarmRadius").gameObject;
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.radius = swarmDiameter / 2f;
        prevSwarmDiameter = swarmDiameter;
    }

    private void Update()
    {
        if(prevSwarmDiameter != swarmDiameter)
        {
            prevSwarmDiameter = swarmDiameter;
            swarmDiameterGraphic.transform.localScale = new Vector2(swarmDiameter, swarmDiameter);
            circleCollider.radius = swarmDiameter / 2f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            var playerStats = collision.gameObject.GetComponent<PlayerStats>();
            playerStats.CurrentHealth--;
        }        
    }
}
