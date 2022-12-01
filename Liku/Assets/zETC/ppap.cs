using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ppap : MonoBehaviour
{


    public void Chages(Sprite index)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = index;
    }
}
