using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameras : MonoBehaviour
{
    public float m_fHeight;
    public float m_fWidth;

    // Use this for initialization
    void Start()
    {
        GetComponent<Camera>().aspect = m_fHeight / m_fWidth;
    }
}