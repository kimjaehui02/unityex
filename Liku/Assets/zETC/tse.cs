using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class tse : MonoBehaviour
{

    [SerializeField]
    private Sprite sprite;

    private void Awake()
    {
        ppap tsset = gameObject.AddComponent<ppap>();
        tsset.Chages(sprite);

    }

}
