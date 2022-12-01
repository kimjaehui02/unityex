using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatLists : MonoBehaviour
{
    /// <summary>
    /// 함수의 타입이 됩니다
    /// </summary>
    public int number;

    /// <summary>
    /// 채팅메뉴입니다
    /// </summary>
    public GameObject chatmenu;

    /// <summary>
    /// 여러 공격시스템이 들어있습니다
    /// </summary>
    public ANSManager ANSManager;

    /// <summary>
    /// 랜덤채팅으로 나오는 내용의 로직입니다
    /// </summary>
    public void RandChatLogic(bool pick)
    {
        Debug.Log(number);

        switch(number)
        {
            case 0:
                Case0(pick);
                break;
            case 1:
                Case1(pick);
                break;
            case 2:
                Case2(pick);
                break;
            case 3:
                Case3(pick);
                break;
            case 4:
                Case4(pick);
                break;
            case 5:
                Case5(pick);
                break;
        }


    }

    /// <summary>
    /// 발굴이벤트 25퍼로 100 100퍼로25
    /// </summary>
    /// <param name="pick"></param>
    public void Case0(bool pick)
    {
        if(pick == true)
        {
            // 25퍼 확률로 100원
            int rand = Random.Range(0,4);
            if(rand == 0)
            {
                GameManager.G_M.PlusMoney(100);
            }
        }
        else
        {
            // 무조건 25원
            GameManager.G_M.PlusMoney(25);
        }

        chatmenu.SetActive(false);
    }


    /// <summary>
    /// 분실이벤트 동전찾기
    /// </summary>
    /// <param name="pick"></param>
    public void Case1(bool pick)
    {
        if (pick == true)
        {
            if(GameManager.G_M.RandManager(2))
            {
                GameManager
                    .G_M
                    .PongsParty[Random.Range(0, GameManager.G_M.PongsParty.Count)]
                    .PongsData
                    .SetHp(
                    GameManager
                    .G_M
                    .PongsParty[Random.Range(0, GameManager.G_M.PongsParty.Count)]
                    .PongsData
                    .GetHp()*0.9f
                    );
            }

            GameManager.G_M.PlusMoney(30);
        }
        else
        {
            GameManager.G_M.MoneyDown(-20);
        }

        chatmenu.SetActive(false);
    }

    /// <summary>
    /// 함정 이벤트
    /// </summary>
    /// <param name="pick"></param>
    public void Case2(bool pick)
    {


        // 한명이 막아섭니다
        if (pick == true)
        {
            int Rand = Random.Range(0, GameManager.G_M.PartyCount());
            Debug.Log(Rand);
            Debug.Log(GameManager.G_M.GetPongs(Rand).name);
            Debug.Log(Rand);
            ANSManager.EToDamage(
                GameManager.G_M.GetPongs(Rand)
                , GameManager.G_M.GetPongs(Rand).PongsData.GetMaxHp()*0.25f
                , Rand);

        }
        // 모두가 나눠입습니다
        else
        {
            for(int i = 0; i < GameManager.G_M.PartyCount(); i++)
            {
                ANSManager.EToDamage(
    GameManager.G_M.GetPongs(i)
    , GameManager.G_M.GetPongs(i).PongsData.GetMaxHp() * 0.1f
    , i);

            }
        }

        chatmenu.SetActive(false);
    }

    /// <summary>
    /// 조우 이벤트
    /// </summary>
    /// <param name="pick"></param>
    public void Case3(bool pick)
    {
        if (pick == true)
        {
            // 파티원이 최대일경우 동작하지 않습니다
            if(GameManager.G_M.PartyCount() == 4)
            {
                return;
            }
            // 파티원이 최대가 아니라면 없는 파티원중에 랜덤으로 하나 고릅니다
            GameManager.G_M.RandAddParty();
        }
        else
        {
            // 파티원을 추가시키지 않습니다
        }

        chatmenu.SetActive(false);
    }
    public void Case4(bool pick)
    {
        if (pick == true)
        {

        }
        else
        {

        }

        chatmenu.SetActive(false);
    }

    /// <summary>
    /// 추가전투 더 강한 전투를 하거나 그냥 지나칩니다
    /// </summary>
    /// <param name="pick"></param>
    public void Case5(bool pick)
    {
        // 강한적과 전투합니다
        if (pick == true)
        {

        }
        // 그냥지나칩니다
        else
        {

        }

        chatmenu.SetActive(false);
    }


}
