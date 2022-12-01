using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{

    [SerializeField]
    private Button Myself;

    private void Awake()
    {
        Myself.onClick.AddListener(testse);
    }

    private void testse()
    {
        Debug.Log(1233);
    }

}
