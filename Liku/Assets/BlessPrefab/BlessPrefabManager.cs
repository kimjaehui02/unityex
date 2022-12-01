using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 블레스 프리펩입니다 
/// </summary>
public class BlessPrefabManager : MonoBehaviour
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
    /// UI매니저입니다
    /// </summary>
    public GameObject MapManager;

    private void Awake()
    {
        // 등장시 맵매니저에 접근합니다
        MapManager = GameObject.FindGameObjectWithTag("MapImage");

        // 맵 매니저의 호버링항목을 트루로 만들어 설명서가 표기되도록합니다
        MapManager.GetComponent<StoryManager>().HoveringB = true;

    }

    private void OnMouseOver()
    {

        // 맵 매니저의 호버링항목을 트루로 만들어 설명서가 표기되도록합니다
        MapManager.GetComponent<StoryManager>().HoveringB = true;

        // 맵 매니저의 텍스트를 다르게 띄워줍니다
        MapManager.GetComponent<StoryManager>().NameB = IName;
        MapManager.GetComponent<StoryManager>().TextB = IText;


    }

    /// <summary>
    /// 시작시 작동하는 함수입니다
    /// </summary>
    public void Aand()
    {

    }

    /// <summary>
    /// 종료시 작동하는 함수입니다
    /// </summary>
    public void andZ()
    {

    }
}
