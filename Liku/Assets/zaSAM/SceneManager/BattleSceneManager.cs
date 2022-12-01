using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BattleSceneManager : MonoBehaviour
{

    /// <summary>
    /// 지도와 메뉴들입니다
    /// </summary>
    public StoryManager GetManager;

    /// <summary>
    /// 채팅 매니저들입니다
    /// </summary>
    public ChatManager ChatManager;

    /// <summary>
    /// 상인입니다
    /// </summary>
    public GameObject CoinObject;

    /// <summary>
    /// 전투시작 유무입니다
    /// </summary>
    public bool BattleStart;

    /// <summary>
    /// 트윈묶음입니다
    /// </summary>
    public List<Tween> GetTweens;

    #region 프리펩과 리스트

    /// <summary>
    /// 배틀씬에 존재하는 아군 리스트입니다.
    /// </summary>
    public List<GameObject> Party;

    /// <summary>
    /// 배틀씬의 아군 체력바입니다
    /// </summary>
    public List<GameObject> objectsHP;

    /// <summary>
    /// 퐁의 프리펩입니다.
    /// </summary>
    [SerializeField]
    private GameObject PongsPrefab;

    /// <summary>
    /// 적들을 담아두는 스크립트입니다
    /// </summary>
    public MonsterSponer Monsters;


    #endregion

    /// <summary>
    /// 공격과스킬들입니다
    /// </summary>
    public ANSManager ANS;

    /// <summary>
    /// 스크롤바매니저입니다
    /// </summary>
    public GameObject BAM;

    /// <summary>
    /// 맵의 타입입니다 맵매니저에따라 지정받습니다
    /// </summary>
    public int MapType;

    public GameObject ga;

    #region 기본함수

    private void Awake()
    {
        CaseChain();
        GetTweens = new List<Tween>();
    }

    private void Update()
    {

        // 체력바를 붙입니다
        PutHp();


    }

    #endregion

    /// <summary>
    /// 체력바를 붙여줍니다
    /// </summary>
    private void PutHp()
    {
        // HP들을 붙여줍니다

        // 아군의 체력바입니다
        for (int i = 0; i < Party.Count; i++)
        {
            if (Party[i].activeSelf == true)
            {
                // 꺼져있다면 켜줍니다
                if (objectsHP[i].activeSelf == false)
                {
                    objectsHP[i].SetActive(true);
                }
                // 체력바를 붙여줍니다
                objectsHP[i].transform.position =
    Camera.main.WorldToScreenPoint(Party[i].transform.position + new Vector3(0, -1.5f, 0));

                // 체력바의 수치를 조절해줍니다
                objectsHP[i].GetComponent<Scrollbar>().size =
                    GameManager.G_M.GetPongs(i).PongsData.GetHp() /
                    GameManager.G_M.GetPongs(i).PongsData.GetMaxHp();

                // 체력바의 숫자도 조절합니다
                objectsHP[i].GetComponent<HpBar>().Hptext(GameManager.G_M.GetPongs(i).PongsData.GetHp(),
                                                      GameManager.G_M.GetPongs(i).PongsData.GetMaxHp());


            }
            else
            {
                // 대상이 비활성화라면 체력바도 비활성화합니다
                objectsHP[i].SetActive(false);
            }
        }

        // 적군의 체력바입니다
        for (int i = 0; i < Monsters.EnemyList.Count; i++)
        {
            // 대상이 살아있을때만 킵니다
            if (Monsters.EnemyList[i].activeSelf == true)
            {
                // 체력바가 꺼져있다면 켜줍니다
                if (Monsters.objectsHp[i].activeSelf == false)
                {
                    Monsters.objectsHp[i].SetActive(true);
                }
                // 체력바를 붙여줍니다
                Monsters.objectsHp[i].transform.position =
    Camera.main.WorldToScreenPoint(Monsters.EnemyList[i].transform.position + new Vector3(0, -1.5f, 0));

                // 체력바의 수치를 조절해줍니다
                Monsters.objectsHp[i].GetComponent<Scrollbar>().size =
                    Monsters.EnemyList[i].GetComponent<EnemyManager>().GetHp() /
                    Monsters.EnemyList[i].GetComponent<EnemyManager>().GetMaxHp();

                // 체력바의 숫자도 조절합니다
                Monsters.objectsHp[i].GetComponent<HpBar>().Hptext(Monsters.EnemyList[i].GetComponent<EnemyManager>().GetHp(),
                                                      Monsters.EnemyList[i].GetComponent<EnemyManager>().GetMaxHp());

            }
            else
            {
                // 대상이 비활성화라면 체력바도 비활성화합니다
                Monsters.objectsHp[i].SetActive(false);
            }
        }
    }

    /// <summary>
    /// 현재 이동한 맵의 타입에 따라 다른것들을 불러오기위해 조절합니다
    /// </summary>
    private void CaseChain()
    {
        // 값이0이라면 들어있지 않다는 소리이므로 캔슬합니다
        if(GameManager.G_M.MapList == null || GameManager.G_M.MGA == 100)
        {
            return;
        }


        // 현재 존재하는 맵이 어떤 맵인지 찾아봅니다
        MapType = GameManager.G_M.MapList[GameManager.G_M.MGA][GameManager.G_M.MSE];
        //for(int i = 0; i < GameManager.G_M.MapList.Count; i++)
        //{
        //    Debug.Log(i+"번째");
        //    for(int i2 = 0; i2 < GameManager.G_M.MapList[i].Count; i2++)
        //    {
        //        Debug.Log(i2 + "," + GameManager.G_M.MapList[i][i2]);
        //        
        //    }
        //}
        //
        //Debug.Log(GameManager.G_M.MapList.Count);

        GetManager.MoveAble = false;
        // 아군을 등장시킵니다
        AwakePongs();
        switch (MapType)
        {


                // 랜덤 이벤트가 등장합니다
            case 0:
                ChatManager.ChatAwake();
                break;
                // 일반 적이 등장합니다
            case 1:
                AwakeP();
                ga.SetActive(true);
                break;
                // 상점이 등장합니다
            case 3:
                AwakeCoin();
                break;
                // 보스가 등장합니다
            case 4:
                break;

            default:
                break;
        }
    }


    #region CASE1

    /// <summary>
    /// 등장페이즈입니다. 보통 1번만 호출됩니다.
    /// </summary>
    private void AwakeP()
    {
        AwakeEnemy(0);

        AwakeEnemy(0);

        AwakeEnemy(1);



    }

    #region 적군이 있는곳에서 작동합니다

    #region Awakes
    /// <summary>
    /// 퐁들을 등장시킵니다.
    /// </summary>
    private void AwakePongs()
    {
        for (int i = 0; i < 4; i++)
        {
            // 퐁이 비어있다면
            if (GameManager.G_M.GetPongs(i) == null)
            {
                // 생성하지 않습니다.
                return;
            }

            // 퐁의 위치를 설정할 벡터입니다.
            Vector3 vector3 = new Vector3(i * -2 - 2, (i % 2 == 0) ? -1f : -2.3f, 0);
            // 퐁을 생성해줍니다.
            Party.Add(Instantiate(PongsPrefab, vector3, Quaternion.identity));
            // 생성한 퐁의 스프라이트를 바꿔줍니다.
            //Party[Party.Count - 1].GetComponent<SpriteRenderer>().sprite =
            //    GameManager.G_M.ClassSprites[GameManager.G_M.GetPongs(i).PongsData.GetPongType()];

            // 대신에 스프라이트 묶음들을 수정합니다
            Party[Party.Count - 1].GetComponent<PongManager>().ChageImage(GameManager.G_M.GetPongs(i).PongsData.GetPongType());

            // 생성한 퐁에 배틀매니저를넣습니다.
            Party[Party.Count - 1].GetComponent<PongManager>().BattleSceneManager = this;

            // 생성한 퐁에 파티의 순번을 넣습니다.
            Party[Party.Count - 1].GetComponent<PongManager>().partynumber = Party.Count - 1;

            // 생성한 퐁에 기본 공격을 넣습니다.
            Party[Party.Count - 1].GetComponent<PongManager>().AttackN = new List<PongManager.PongAttack>();

            Party[Party.Count - 1].GetComponent<PongManager>().AttackN.Add(ANS.Attack1);

            

        }
    }

    /// <summary>
    /// 에너미를 생성하는 함수입니다.
    /// </summary>
    /// <param name="PreFab">프리펩 넘버</param>
    /// <param name="Script">스크립터블오브젝트넘버</param>
    private void AwakeEnemy(int PreFab)
    {
        int i = Monsters.EnemyList.Count;
        Vector3 vector3 = new Vector3(-(i * -2 - 2), (i % 2 == 0) ? -1f : -2.3f, 0);
        // 에너미를 생성하고 리스트에 넣어줍니다.
        Monsters.EnemyList.Add(Instantiate(Monsters.EnemyPrfab[PreFab], vector3, Quaternion.identity));

        // 생성한 적에 배틀매니저를넣습니다.
        Monsters.EnemyList[Monsters.EnemyList.Count - 1].GetComponent<EnemyManager>().BattleSceneManager = this;

        // 생성한 적에 리스트의 순번을 넣습니다.
        Monsters.EnemyList[Monsters.EnemyList.Count - 1].GetComponent<EnemyManager>().EnemyNUmber = Monsters.EnemyList.Count - 1;

        // 생성한 적에 공격을 넣어줍니다
        Monsters.EnemyList[Monsters.EnemyList.Count - 1].GetComponent<EnemyManager>().AttackN = new List<EnemyManager.EnemyAttack>();
        
        Monsters.EnemyList[Monsters.EnemyList.Count - 1].GetComponent<EnemyManager>().AttackN.Add(ANS.EAttack1);
    }


    #endregion


    #region 뱀매니지먼트

    /// <summary>
    /// 뱀을 움직일때 씁니다
    /// </summary>
    public void UpBAM()
    {
        GetTweens.Add(
        BAM.GetComponent<RectTransform>().DOAnchorPosY(10, 0.8f)
            .SetEase(Ease.OutCubic)
            );
    }
    // 여기도 피버충같은걸 만들면 어떨까

    /// <summary>
    /// 뱀을 내릴때 씁니다
    /// </summary>
    public void DownBAM()
    {
        GetTweens.Add(
BAM.GetComponent<RectTransform>().DOAnchorPosY(-100, 0.8f)
            .SetEase(Ease.OutCubic)
            );
    }

    /// <summary>
    /// 개전을 합니다
    /// </summary>
    public void DoFight()
    {
        BAM.GetComponent<BAManager>().PongTEC();
        // 전투가 시작했습니다
        BattleStart = true;
        // 전투중엔 이동할수없습니다
        GetManager.MoveAble = false;
    }

    public void BS(GameObject game)
    {
        UpBAM();
        DoFight();
        game.SetActive(false);
    }

    /// <summary>
    /// 적이 전부 쓰러지면 실행하는 함수입니다
    /// </summary>
    public void Victory()
    {
        // 전투를 끝냅니다
        BattleStart = false;
        // 이제 다음맵으로 움직일 수 있습니다
        GetManager.MoveAble = true;

        // 게이지바에 띄운것들을 비활성화합니다
        BAM.GetComponent<BAManager>().PongTEC();
        // 게이지바를 내립니다
        DownBAM();



    }

    #endregion

    #endregion

    #endregion

    #region CASE2

    /// <summary>
    /// 상인을 등장시킵니다
    /// </summary>
    public void AwakeCoin()
    {
        CoinObject.SetActive(true);
    }


    #endregion

    public void MoveToMain()
    {

        // 메인화면으로 이동시킵니다.
        GameManager.G_M.ChangeScene("Main_S");

    }

    /// <summary>
    /// 디버그용입니다.
    /// </summary>
    public void DenugEnemy()
    {
        for(int  i = 0; i < Monsters.EnemyList.Count; i++)
        {
            Debug.Log(Monsters.EnemyList[i].GetComponent<EnemyManager>().GetHp());

            
        }

        
    }


    private void OnDestroy()
    {
        for (int i = 0; i < GetTweens.Count; i++)
        {
            GetTweens[i].Kill();
        }
    }

}
