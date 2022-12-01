using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;


public class MainSceneManager : MonoBehaviour
{
    /// <summary>
    /// 메인씬의 퐁들을 관리합니다.
    /// </summary>
    public List<GameObject> MainPong;

    /// <summary>
    /// 체인지용 버튼입니다.
    /// </summary>
    [SerializeField]
    private List<GameObject> ChangeButton;

    /// <summary>
    /// 체력바들입니다
    /// </summary>
    [SerializeField]
    private List<GameObject> HpBar;

    /// <summary>
    /// 선택한 순번입니다.
    /// </summary>
    [SerializeField]
    private int PickChange;

    /// <summary>
    /// 메인메뉴의 오브젝트입니다
    /// </summary>
    public GameObject MainObj;

    /// <summary>
    /// 메인 유아이입니다
    /// </summary>
    public StoryManager StoryManager;

    /// <summary>
    /// 이 스크립트의 트윈들입니다
    /// </summary>
    public List<Tween> GetTweens;

    /// <summary>
    /// 선택유무입니다.
    /// </summary>
    [SerializeField]
    private bool PickCheck;



    private void Awake()
    {
        GetTweens = new List<Tween>();
    }

    private void Start()
    {
        StoryManager = GameObject.FindGameObjectWithTag("MapImage").GetComponent<StoryManager>();
        MainMenu();

    }

    private void Update()
    {
        MainPongManage();
        PickUImange();

    }

    /// <summary>
    /// UI를 오브젝트의 위치에 맞게 조절합니다
    /// </summary>
    private void PickUImange()
    {
        // 교체버튼조절
        for (int i = 0; i < MainPong.Count; i++)
        {
            ChangeButton[i].transform.position =
                Camera.main.WorldToScreenPoint(MainPong[i].transform.position + new Vector3(0, -2f, 0));

            // 체력바
            if (MainPong[i].activeSelf == true)
            {
                HpBar[i].transform.position =
                    Camera.main.WorldToScreenPoint(MainPong[i].transform.position + new Vector3(0, -1.5f, 0));


                // 체력바를 붙여줍니다
                HpBar[i].transform.position =
    Camera.main.WorldToScreenPoint(MainPong[i].transform.position + new Vector3(0, -1.5f, 0));

                // 체력바의 수치를 조절해줍니다
                HpBar[i].GetComponent<Scrollbar>().size =
                    GameManager.G_M.GetPongs(i).PongsData.GetHp() /
                    GameManager.G_M.GetPongs(i).PongsData.GetMaxHp();

                // 체력바의 숫자도 조절합니다
                HpBar[i].GetComponent<HpBar>().Hptext(GameManager.G_M.GetPongs(i).PongsData.GetHp(),
                                                      GameManager.G_M.GetPongs(i).PongsData.GetMaxHp());
            }

        }



    }

    /// <summary>
    /// 퐁들의 이미지를 관리해줍니다.
    /// </summary>
    private void MainPongManage()
    {
        // 게임매니저에 퐁이 없다면 퐁을 비활성화 시킵니다.
        for (int i = 0; i < MainPong.Count; i++)
        {
            // i번째의 퐁이 비어있는가를 확인해줍니다.
            if (GameManager.G_M.GetPongs(i) == null)
            {
                // i번쟤의 퐁파티에 값이 없다면 실제로도 비활성화 해줍니다.
                MainPong[i].SetActive(false);
                ChangeButton[i].SetActive(false);
            }
            // i번째의 퐁이 비어있지 않다면 생김새를 업데이트 해줍니다.
            else
            {
                // 메인씬의 i번째 퐁의 이미지를
                //MainPong[i].transform.GetComponent<SpriteRenderer>().sprite = 
                //    // 파티의i번쨰 퐁의 직업 이미지를 넣습니다.
                //    GameManager.G_M.ClassSprites[GameManager.G_M.GetPongs(i).PongsData.GetPongType()];

                // 대신에 직업이미지 숫자를 붙러와서 서있는 이미지를 불러주는 함수에 집어넣습니다
                MainPong[i].GetComponent<PongManager>().ChageImage(GameManager.G_M.GetPongs(i).PongsData.GetPongType());

                // 그리고 퐁의 선택 유무에 따라서 약간 색을 달리해 줍니다.
                // 특정 퐁이 선택되어 있다면 / 선택받지 못한 퐁이라면 색을 약간 줄입니다.
                if (PickCheck == true && PickChange != i)
                {

                    //MainPong[i].transform.GetComponent<SpriteRenderer>().color = new Color(122 / 255f, 122 / 255f, 122 / 255f, 255 / 255f);
                    // 대신에 스프라이트 모음집을 만들어서 그걸로 색을 변경합니다
                    for(int yy = 0; yy < MainPong[i].GetComponent<PongManager>().MySpirtes.renderers.Count; yy++)
                    {
                        MainPong[i].GetComponent<PongManager>().MySpirtes.renderers[yy].color = new Color(122 / 255f, 122 / 255f, 122 / 255f, 255 / 255f);
                    }

                }
                // 특정 퐁이 선택되어있지 않거나 선택받지 못한 퐁이라면 색을 복원시킵니다.
                else
                {
                    //MainPong[i].transform.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
                    for (int yy = 0; yy < MainPong[i].GetComponent<PongManager>().MySpirtes.renderers.Count; yy++)
                    {

                        MainPong[i].GetComponent<PongManager>().MySpirtes.renderers[yy].color = new Color(255, 255, 255);
                    }
                }
            }
        }
    }

    /// <summary>
    /// 퐁들의 위치를 뒤바꿀때 사용할 함수입니다.
    /// </summary>
    public void PartyMove(int index1)
    {
        // 선택한 퐁의 값을 임시로 담습니다
        Pongs pongs = GameManager.G_M.GetPongs(index1);

        // 선택하지 않은 퐁을 선택한 퐁의 자리에 넣습니다
        GameManager.G_M.SetPongs(index1, GameManager.G_M.GetPongs(index1 + 1 ));

        // 선택한 퐁을 선택하지 않은 퐁의 자리에 넣습니다
        GameManager.G_M.SetPongs(index1 + 1, pongs);
    }

    // 경우의 수 따지기
    // 선택이 되있을때 / 안되있을때

    // 선택이 되있을때 :
    // 1.이미 선택된걸 누르기
    // 2.선택이 안되있는걸 누르기

    // 선택이 안되있을때 :
    // 3.선택이 안되있는걸 누르기

    /// <summary>
    /// 파티의 위치를 바꿀때 쓰는 함수입니다.
    /// </summary>
    /// <param name="index">누른 버튼의 순서 -1 입니다.</param>
    public void ChangeParty(int index)
    {
        // 디버그용입니다
        //for (int i = 0; i < GameManager.G_M.Equips1.Length; i++)
        //{
        //    if(GameManager.G_M.Equips1[i] == null)
        //    {
        //        continue;
        //    }
        //
        //    Debug.Log(GameManager.G_M.Equips1[i].name + i);
        //}

        // 선택이 안되있을때 :
        if (PickCheck == false)
        {
            // 3.선택이 안되있는걸 누르기
            PickChange = index;
            PickCheck = true;
        }
        // 선택이 되있을때
        else
        {
            // 1.이미 선택된걸 누르기
            if (PickChange == index)
            {
                PickChange = 0;
                PickCheck = false;
            }
            // 2.선택이 안되있는걸 누르기
            else
            {
                // 선택한 퐁의 값을 임시로 담습니다
                Pongs pongs = GameManager.G_M.GetPongs(PickChange);

                // 선택하지 않은 퐁을 선택한 퐁의 자리에 넣습니다
                GameManager.G_M.SetPongs(PickChange, GameManager.G_M.GetPongs(index));

                // 선택한 퐁을 선택하지 않은 퐁의 자리에 넣습니다
                GameManager.G_M.SetPongs(index, pongs);


                // 선택한 퐁의 머리와 무기를 임시로 담습니다 배열의 위치를 바꿔줍니다
                GameObject exHead = GameManager.G_M.Equips1[PickChange];
                GameObject exWea = GameManager.G_M.Equips2[PickChange];

                // 선택하지 않은 장비를 선택한 장비칸에 넣습니다
                GameManager.G_M.Equips1[PickChange] = GameManager.G_M.Equips1[index];
                GameManager.G_M.Equips2[PickChange] = GameManager.G_M.Equips2[index];

                // 선택한 장비를 선택하지 않은 장비칸에 넣습니다
                GameManager.G_M.Equips1[index] = exHead;
                GameManager.G_M.Equips2[index] = exWea;

                // 실제 오브젝트도 위치를 바꿉니다
                DefaultUIManager @default = FindObjectOfType<DefaultUIManager>();

                // index = 나중에 클릭한 상대측
                // PickChange = 먼저 클릭한 우리측

                // 상대측의 장비를 찾아서 배열로 만들어 준다
                SimpleEquip[] @object = new SimpleEquip[@default.pongUIs[index].GetComponentsInChildren<SimpleEquip>().Length];
                @object = @default.pongUIs[index].GetComponentsInChildren<SimpleEquip>();
                
                // 우리측의 장비를 찾아서 배열로 만들어 준다
                SimpleEquip[] @object2 = new SimpleEquip[@default.pongUIs[PickChange].GetComponentsInChildren<SimpleEquip>().Length];
                @object2 = @default.pongUIs[PickChange].GetComponentsInChildren<SimpleEquip>();

                // 상대측의 장비들에 이동하는 함수를 실행시킨다
                for (int i = 0; i < @object.Length; i++)
                {
                    // 상대측의 장비 배열에 우리측으로 이동시키는 함수를 실행한다
                    @object[i].ChangeEquipP(PickChange);

                    //@default.pongUIs[index].GetComponentsInChildren<SimpleEquip>()[1].ChangeEquipP(PickChange);
                }

                // 우리측이 장비에 이동하는 함수를 실행시킨다
                for (int i = 0; i < @object2.Length; i++)
                {
                    // 우리측의 장비 배열에 상대측으로 이동시키는 함수를 실행한다
                    @object2[i].ChangeEquipP(index);

                }


                PickChange = 0;
                PickCheck = false;
            }
        }
    }

    /// <summary>
    /// 배틀씬으로 이동시킵니다.
    /// </summary>
    public void MoveToBattle()
    {



        // 배틀씬으로 이동시킵니다.
        GameManager.G_M.ChangeScene("Battle_S");
    }

    /// <summary>
    /// 메인씬의 메뉴를 소환합니다
    /// </summary>
    public void MainMenu()
    {
        // 일단 여기서 할일이 안끝났으니 이동불가를 걸어놓습니다
        StoryManager.MoveAble = false;

        // 이제 메인화면의 스크립트 창을 띄워놓습니다
        MainObj.SetActive(true);


    }

    /// <summary>
    /// 디버그용입니다.
    /// </summary>
    public void PartyDebug()
    {
        for (int i = 0; i < 4; i++)
        {
            if (GameManager.G_M.GetPongs(i) != null)
            {
                Debug.Log(GameManager.G_M.GetPongs(i).PongsData.GetPongType() + "HP = " + GameManager.G_M.GetPongs(i).PongsData.GetHp());
            }
        }
    }

    public void PartyDebugPlus()
    {
        for (int i = 0; i < 4; i++)
        {
            if (GameManager.G_M.GetPongs(i) != null)
            {
                GameManager.G_M.GetPongs(i).PongsData.SetHp(GameManager.G_M.GetPongs(i).PongsData.GetHp() + i + 1);
            }
        }
    }

}


