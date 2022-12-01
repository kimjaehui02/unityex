using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Scriptable Object/Enemy Data", order = int.MaxValue)]

public class ScriptableEnemy : ScriptableObject
{
    /// <summary>
    /// 최대 체력입니다.
    /// </summary>
    [SerializeField]
    private float MaxHp;

    /// <summary>
    /// 체력입니다.
    /// </summary>
    [SerializeField]
    private float Hp;

    #region Hp프로퍼티

    public float GetHp()
    {
        return Hp;
    }

    public void SetHp(float index)
    {
        Hp = index;
    }

    #endregion

    /// <summary>
    /// 공격력입니다.
    /// </summary>
    [SerializeField]
    private float Attack;

    #region Attack프로퍼티

    public float GetAttack()
    {
        return Attack;
    }

    public void SetAttack(float index)
    {
        Attack = index;
    }

    #endregion

    /// <summary>
    /// 보상금입니다.
    /// </summary>
    [SerializeField]
    private int Reward;

    #region Reward프로퍼티

    public int GetReward()
    {
        return Reward;
    }

    public void SetReward(int index)
    {
        Attack = index;
    }

    #endregion


}
