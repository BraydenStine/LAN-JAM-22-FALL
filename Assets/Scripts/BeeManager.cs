// Alex Jalonen - 3-19-22
// manages public list of bees

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeManager : MonoBehaviour
{
    RandomBee randomBee;
    #region Inspector Set Var
    public GameObject beePrefab;

    public int BeeMinCount = 5;
    #endregion

    #region Internal Ref
    public List<GameObject> list;
    [HideInInspector]
    public SwarmDanger swarmDanger;
    #endregion

    private void Awake()
    {
        list = new List<GameObject>();
        swarmDanger = transform.parent.GetComponent<SwarmDanger>();
    }

    private void Update()
    {
        if(list.Count < BeeMinCount)
        {
            var bee = Instantiate(beePrefab, transform);
            bee.GetComponent<SpriteRenderer>().sprite = RandomBee.sprites[Random.Range(0,RandomBee.sprites.Length)];
            var rand = Random.insideUnitCircle;

            bee.transform.position = new Vector2(bee.transform.position.x + rand.x, bee.transform.position.y + rand.y);

            var beeMove = bee.GetComponent<BeeMovementController>();
            if(beeMove != null)
            {
                beeMove.manager = this;
            }
        }

        if(list.Count < transform.childCount)
        {
            //get list of children
            var children = transform.GetComponentsInChildren<Transform>();
            foreach(var item in children)
            {
                if(!list.Contains(item.gameObject))
                {
                    list.Add(item.gameObject);
                }
            }
        }
    }
}
