using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    // 게임매니저의 특징은 여러 데이터를 가지고 씬을 다닙니다.

    // 현재 플랫폼이 pc인지를 체크합니다.
    public bool G_M_PC;

    // 돈입니다.
    public int Money;


    #region 기본 함수

    private void Awake()
    {
        // 이 친구는 계속 남아서 게임을 관리해 줄겁니다.
        DontDestroyOnLoad(gameObject);
    }


    #endregion


    #region 커스텀 함수

    // 이 친구는 금수박사입니다. 하하하!하하하!하하하!윾!
    public void GetMoneyGM(int income)
    {
        // 수입을 내놔랏!
        Money += income;
    }

    #endregion

}
