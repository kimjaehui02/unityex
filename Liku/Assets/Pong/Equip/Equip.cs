using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Equip : Itembase
{
    // 장비의 스탯들입니다.

    // 장비체력
    /// <summary>
    /// 
    /// </summary>
    private float EquipHp;

    // 장비공격력
    private float EquipAttack;


}





public class Itembase : ScriptableObject
{
    /// <summary>
    /// 아이템의 등급입니다 0 = 흰색, 1 = 파랑색, 2 = 빨강색
    /// </summary>
    public int Rare;

    /// <summary>
    /// 아이템의 이름입니다
    /// </summary>
    public string IName;

    /// <summary>
    /// 아이템의 설명입니다
    /// </summary>
    public string IText;

    /// <summary>
    /// 아이템의 가격입니다
    /// </summary>
    public int Money;

    /// <summary>
    /// 아이템의 이미지입니다
    /// </summary>
    public Sprite GetSprite;



}



