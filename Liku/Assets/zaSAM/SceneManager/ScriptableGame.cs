using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveData", menuName = "Scriptable SaveData/SaveData", order = int.MaxValue)]

public class ScriptableGame : ScriptableObject
{


    /// <summary>
    /// 퐁들을 담아두는 리스트입니다. 
    /// </summary>
    public List<int> PongsParty;

    /// <summary>
    /// 돈의 보유량입니다.
    /// </summary>
    public int HaveMoney;

    /// <summary>
    /// 가로줄입니다 최대 7개있습니다
    /// </summary>
    public int MGA;

    /// <summary>
    /// 세로줄입니다 단일맵1개를 나타냅니다
    /// </summary>
    public int MSE;

    /// <summary>
    /// 랜덤시드입니다
    /// </summary>
    public int RANDAMSEED;

}
