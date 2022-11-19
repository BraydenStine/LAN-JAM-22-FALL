using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public GameObject heartOne;
    public GameObject heartTwo;
    public GameObject heartThree;

    public static void SetHeartColor(GameObject heart, Color color)
    {
        var images = heart.GetComponentsInChildren<Image>();
        foreach(var image in images)
        {
            image.color = color;
        }
    }
}
