using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pongs Data", menuName = "Scriptable Object/Pongs Data", order = int.MaxValue)]

public class ScriptablePongs : ScriptableObject
{
    /// <summary>
    /// 전투가능유무입니다
    /// </summary>
    public bool BattleAble;

    /// <summary>
    /// 퐁들의 최대체력입니다.
    /// </summary>
    [SerializeField]
    private float MaxHp;

    /// <summary>
    /// 퐁들의 다른걸 더해주지 않은 기본최대체력입니다
    /// </summary>
    [SerializeField]
    private float DefualtMaxHp;

    /// <summary>
    /// 퐁들의 체력입니다.
    /// </summary>
    [SerializeField]
    private float PongHp;

    #region Hp프로퍼티

    public float GetHp()
    {
        return PongHp;
    }

    public void SetHp(float index)
    {
        PongHp = index;
    }

    public float GetMaxHp()
    {

        return MaxHp;
    }

    public float GetDefualtMaxHp()
    {
        return DefualtMaxHp;
    }

    #endregion

    /// <summary>
    /// 퐁들의 공격력입니다.
    /// </summary>
    [SerializeField]
    private float PongAttack;

    #region Attack프로퍼티

    public float GetAttack()
    {
        return PongAttack;
    }

    public void SetAttack(float index)
    {
        PongAttack = index;
    }

    #endregion


    /// <summary>
    /// 퐁의 타입입니다.
    /// </summary>
    [SerializeField]
    private int PongType;

    #region PongType프로퍼티

    public int GetPongType()
    {
        return PongType;
    }

    public void SetPongType(int index)
    {
        PongType = index;
    }

    #endregion

    /// <summary>
    /// 머리장비입니다
    /// </summary>
    public HeadEquip headEquip;





}
