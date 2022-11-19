using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goLeft : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x - (20 * Time.deltaTime), transform.position.y);
    }
}
