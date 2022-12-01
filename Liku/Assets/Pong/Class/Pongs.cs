using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public abstract class Pongs : MonoBehaviour
{

    // 퐁들의 부모가 되는 추상클래스입니다.
    // 퐁들은 체력, 부상, 공격력을 가집니다.
    // 전투동안 퐁들은 부상이라는 에너지를 가지고 전투합니다.
    // 전투시작시 체력에 비례하여 부상이라는 에너지를 얻습니다.
    // 전투종료시 부상에 비례하여 체력이 조정됩니다.

    /// <summary>
    /// 퐁들의 데이터를 가진 스크립터블 오브젝트입니다.
    /// </summary>
    [SerializeField]
    private ScriptablePongs pongsData;


    /// <summary>
    /// 퐁들의 데이타를 가진 프로퍼티입니다
    /// </summary>
    public ScriptablePongs PongsData { get { return pongsData; } set { pongsData = value; } }




}

/// <summary>
/// 퐁들직업 종류에따른 열거형입니다.
/// </summary>
enum PongClass
{
    Flag,
    Shield,
    Spear,
    Bow,
    END
}