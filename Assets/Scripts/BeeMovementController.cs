// Alex Jalonen 3-19-22
// treats bees as boids

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BeeMovementController : MonoBehaviour
{
    #region Internal Ref
    Rigidbody2D rb;
    [HideInInspector]
    public BeeManager manager;
    #endregion

    #region Inspector Set Var
    public float swarmPullMag = 5f;
    public float avoidCenterMag = 3f;
    public float outsideRadiusVelocity = 3f;
    public float seperationRadius = .3f;
    public float seperationMag = 1f;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var vectorToSwarmCenter = (manager.gameObject.transform.position - transform.position);
        var distanceToSwarmCenter = vectorToSwarmCenter.magnitude;
        var dirToSwarmCenter = vectorToSwarmCenter.normalized;

        if(distanceToSwarmCenter < manager.swarmDanger.swarmDiameter/4f)
        {
            rb.AddForce(-dirToSwarmCenter * avoidCenterMag);
        }

        if(distanceToSwarmCenter > manager.swarmDanger.swarmDiameter / 2f)
        {
            var correctionVelocity = dirToSwarmCenter * outsideRadiusVelocity * Time.deltaTime;
            transform.position = new Vector2(transform.position.x + correctionVelocity.x, transform.position.y + correctionVelocity.y);
        }
        else
        {
            var otherBees = manager.list;
            foreach(var bee in otherBees)
            {
                if (bee == gameObject) continue;

                var vectorFromBee = (transform.position - bee.transform.position);

                if(vectorFromBee.magnitude < seperationRadius)
                {      
                    rb.AddForce(vectorFromBee.normalized * seperationMag);
                }
            }
        }



        rb.AddForce(dirToSwarmCenter * swarmPullMag);
    }
}
