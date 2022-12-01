using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 간단하게 정리한 장비입니다
/// </summary>
public class SimpleEquip : MonoBehaviour
{
    /// <summary>
    /// 장비를 장착해서 증가하는 체력과 공격력입니다
    /// </summary>
    public float PlusHp;

    public float PlusAttack;

    /// <summary>
    /// 장비의 타입입니다
    /// </summary>
    public int ItemType;

    /// <summary>
    /// 장비의 장착 부위입니다 0 = 머리 1 = 무기
    /// </summary>
    public int ItemType2;

    /// <summary>
    /// 아이템의 이름입니다
    /// </summary>
    public string IName;

    /// <summary>
    /// 아이템의 설명입니다
    /// </summary>
    public string IText;

    /// <summary>
    /// 아이템의 등급입니다 0 = 흰색,
    /// 1 = 파랑색, 2 = 빨강색
    /// </summary>
    public int Rare;

    /// <summary>
    /// UI매니저입니다
    /// </summary>
    public GameObject MapManager;

    /// <summary>
    /// 자신이 속해있는 퐁 유아이입니다
    /// </summary>
    public PongUI mypongui;

    /// <summary>
    /// 자신이 장비한 퐁의 숫자입니다
    /// </summary>
    public int Equipint;

    /// <summary>
    /// 디폴트 유아이 매니저입니다
    /// </summary>
    public DefaultUIManager @default;

    private void Awake()
    {
        // 등장시 맵매니저에 접근합니다
        MapManager = GameObject.FindGameObjectWithTag("MapImage");
        @default = GetComponentInParent<DefaultUIManager>();

    }

    private void Start()
    {
        mypongui = GetComponentInParent<PongUI>();
    }

    private void OnMouseOver()
    {

        // 맵 매니저의 호버링항목을 트루로 만들어 설명서가 표기되도록합니다
        MapManager.GetComponent<StoryManager>().HoveringB = true;

        // 맵 매니저의 텍스트를 다르게 띄워줍니다
        MapManager.GetComponent<StoryManager>().NameB = IName;
        MapManager.GetComponent<StoryManager>().TextB = IText;


    }

    private void Update()
    {
        // 만약 펼쳐져있지 않다면 콜라이더를 끕니다
        if(mypongui.toglebool == false)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }

        // 게임매니저에 자신의 데이터를 넣습니다
        if(ItemType2 == 0)
        {
            //GameManager.G_M.Equips1[Equipint] = gameObject;
        }
        else if(ItemType2 == 1)
        {
            //GameManager.G_M.Equips2[Equipint] = gameObject;

        }

    }


    /// <summary>
    /// 파티 위치를 바꿀때 쓰는 교환함수입니다
    /// </summary>
    /// <param name="PickChange">반대편의 장비 위치입니다</param>
    public void ChangeEquipP(int PickChange)
    {
        // 내가 이동할 부모 트랜스폼입니다
        Transform gameObject2 = @default.pongUIs[PickChange].transform;

        // 현재 자신의 부모 트랜스폼입니다
        Transform parG = transform.parent;

        // 상대측의 데이터입니다

        // 나 자신을 이동시킵니다
        switch (ItemType2)
        {
            case 0:// 머리장비인 경우

                // 상대의 장비가 있을때만 실시합니다
                if (gameObject2.GetChild(0).GetChild(1).GetChild(0).childCount != 0)
                {
                    // 상대의 장비를 자신의 트랜스폼으로 옮겨옵니다
                    Transform transforms = gameObject2.GetChild(0).GetChild(1).GetChild(0).GetChild(0);
                    transforms.SetParent(parG);
                    // 좌표를 수정해 줍니다
                    transforms.localPosition = new Vector3(0, 0, 0);

                    Debug.Log(name);
                }

                // 상대측으로 트랜스폼을 교체합니다
                transform.SetParent(gameObject2.GetChild(0).GetChild(1).GetChild(0));
                // 좌표를 수정합니다
                transform.localPosition = new Vector3(0,0,0);
                


                break;

            case 1:// 무기장비인 경우

                // 상대의 장비가 있을때만 실시합니다
                if (gameObject2.GetChild(0).GetChild(0).GetChild(0).childCount != 0)
                {
                    // 상대의 장비를 자신의 트랜스폼으로 옮겨옵니다
                    Transform transforms = gameObject2.GetChild(0).GetChild(0).GetChild(0).GetChild(0);
                    transforms.SetParent(parG);
                    // 좌표를 수정해 줍니다
                    transforms.localPosition = new Vector3(0, 0, 0);
                }

                // 상대측으로 트랜스폼을 교체합니다
                transform.SetParent(gameObject2.GetChild(0).GetChild(0).GetChild(0));
                // 좌표를 수정합니다
                transform.localPosition = new Vector3(0, 0, 0);

                break;
        }






    }

}
