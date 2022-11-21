using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBee : MonoBehaviour
{
    public static Sprite[] sprites;
    // Start is called before the first frame update
    void Awake()
    {
        Object[] temp = Resources.LoadAll("Bees", typeof(Sprite));
        sprites = new Sprite[temp.Length];
        for(int i =0; i<temp.Length;++i)
        {
            sprites[i] = temp[i] as Sprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
