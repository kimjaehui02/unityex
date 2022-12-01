using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainStSceenManager : MonoBehaviour
{
    /// <summary>
    /// 배경크리를 관리하기위해 카메라를 받아옵니다
    /// </summary>
    public Camera GetCamera;

    /// <summary>
    /// 배경입니다
    /// </summary>
    public GameObject background;

    /// <summary>
    /// 여러가지 선택지들 입니다
    /// </summary>
    public List<GameObject> Menus;

    private void Start()
    {
        GetCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        float test = GetCamera.orthographicSize * 160;
        background.GetComponent<RectTransform>().sizeDelta=new Vector2(test * GetCamera.aspect, test);

    }

    private void OnDestroy()
    {
        GetCamera = null;
    }

    /// <summary>
    /// 새로운 시작을 하기 위한 함수입니다
    /// </summary>
    public void MoveToPick()
    {
        // 랜덤으로 시드값을 생성합니다
        GameManager.G_M.SetSEED((int)System.DateTime.Now.Ticks);
        // 생성된 시드값을 적용시킵니다
        Random.InitState(GameManager.G_M.GetSEED());

        // 저장된 데이터 역시 초기화를 해줍니다
        for(int i = 0; i < GameManager.G_M.Default.Count; i++)
        {
            // 새로 시작하므로 체력을 초기화합니다
            GameManager.G_M.Default[i].SetHp(GameManager.G_M.Default[i].GetMaxHp());
            // 장비도 비워둡니다
            GameManager.G_M.Default[i].headEquip = null;

        }
        // 그리고 세이브데이터의 퐁들도 비웁니다
        GameManager.G_M.PongsParty.Clear();
        // 돈도 초기화합니다
        GameManager.G_M.SetMoney(100);
        // 맵리스트를 비웁니다
        GameManager.G_M.MapList = new List<List<int>>();
        // 임시리스트도 비웁니다
        GameManager.G_M.OTF.Clear();

        // 현재 위치도 초기화됩니다
        GameManager.G_M.MGA = 100;
        GameManager.G_M.MSE = 100;
        //GameManager.G_M.OKTEst = true;

        //GameManager.G_M.ChangeScene("Story_S");
        GameManager.G_M.ChangeScene("Pick_S");
    }

    /// <summary>
    /// 새로운 시작이 아닌 이어하기를 위한 함수입니다
    /// </summary>
    public void MoveToconti()
    {



    }

}
