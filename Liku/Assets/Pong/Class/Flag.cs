using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : Pongs
{
    // 플래그는 퐁들의 파티에 근간이 되는 핵심입니다.
    // 특수장비로는 깃발이 있습니다.

    /// <summary>
    /// 특수장비인 깃발입니다.
    /// </summary>
    [SerializeField]
    public FlagEquip FlagEquip;

    private void Awake()
    {

    }

}
