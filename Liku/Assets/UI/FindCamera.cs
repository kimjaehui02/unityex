using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindCamera : MonoBehaviour
{


    private void Start()
    {
        gameObject.GetComponent<Canvas>().worldCamera =
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
}
