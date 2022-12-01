using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effsc : MonoBehaviour
{

    /// <summary>
    /// 이 이펙트가 존재할 수 있는 시간입니다.
    /// </summary>
    [SerializeField]
    private float TimeE;
    
    /// <summary>
    /// 이펙트가 존재한 시간입니다
    /// </summary>
    [SerializeField]
    private float TimeER;

    // Update is called once per frame
    void Update()
    {
        // 시간을 측정합니다
        TimeER += Time.deltaTime;

        // 존재한 시간이 일정값을 넘으면 비활성화합니다
        if (TimeER > TimeE)
        {
            TimeER = 0;
            gameObject.SetActive(false);
        }
    }

}
