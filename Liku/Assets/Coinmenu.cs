using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coinmenu : MonoBehaviour
{
    // 상점의 메뉴에 붙어있는 스크립트 입니다.

    /// <summary>
    /// 가지고있는 아이템입니다
    /// </summary>
    public GameObject GameObject;



    private void OnMouseDown()
    {
        Sell();
    }

    /// <summary>
    /// 판매될 때 사용합니다
    /// </summary>
    public void Sell()
    {
        if(GameManager.G_M.GetMoney() > GameObject.GetComponent<Itembase>().Money)
        {

        }
    }


}
