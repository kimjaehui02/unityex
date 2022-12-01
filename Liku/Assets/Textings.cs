using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 텍스트가 있는 창을 띄울떄 사용합니다
/// </summary>
public class Textings : MonoBehaviour
{

    /// <summary>
    /// 아이템의 이름입니다
    /// </summary>
    public string IName;

    /// <summary>
    /// 아이템의 설명입니다
    /// </summary>
    public string IText;

    /// <summary>
    /// UI매니저입니다
    /// </summary>
    public GameObject MapManager;

    private void Awake()
    {
        // 등장시 맵매니저에 접근합니다
        MapManager = GameObject.FindGameObjectWithTag("MapImage");



    }

    private void Update()
    {
        if(MapManager == null)
        {
            MapManager = GameObject.FindGameObjectWithTag("MapImage");
        }
    }

    private void OnMouseOver()
    {

        // 맵 매니저의 호버링항목을 트루로 만들어 설명서가 표기되도록합니다
        MapManager.GetComponent<StoryManager>().HoveringB = true;

        // 맵 매니저의 텍스트를 다르게 띄워줍니다
        MapManager.GetComponent<StoryManager>().NameB = IName;
        MapManager.GetComponent<StoryManager>().TextB = IText;


    }
}
