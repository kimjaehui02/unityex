using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class CoinManager : MonoBehaviour
{


    // 상점을 컨트롤하는 스크립트입니다

    /// <summary>
    /// 상점 창 입니다
    /// </summary>
    public GameObject CoinBox;

    /// <summary>
    /// 상점 창의 켜짐상태 유무입니다
    /// </summary>
    public bool CoinBool;




    // 마우스 버튼을 누르면 작동되게 합니다
    private void OnMouseDown()
    {
        CoinCont();
    }


    /// <summary>
    /// 상점창을 키고 끄는 버튼입니다
    /// </summary>
    public void CoinCont()
    {
        // 상점창이 꺼져있다면 켭니다
        if(CoinBool == false)
        {

        }
        // 상점창이 켜져있다면 끕니다
        else if(CoinBool == true)
        {

        }
    }


}