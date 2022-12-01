using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Transactions;

public class StoryManager : MonoBehaviour
{


    /// <summary>
    /// 지도가 펴져있는지 확인합니다
    /// </summary>
    public bool UDB;

    /// <summary>
    /// 다음맵으로 이동가능한지 여부입니다
    /// </summary>
    public bool MoveAble;

    /// <summary>
    /// 다음맵으로 넘어가게해주는 선택지입니다
    /// </summary>
    public GameObject NextObject;

    /// <summary>
    /// 지도를 펼쳤는데 콜라이더가 겹쳐서 확인이 안되면 안되므로 조절합니다
    /// </summary>
    //public List<BoxCollider2D> boxCollider2Ds;

    /// <summary>
    /// 여러가지 트윈들의 묶음입니다
    /// </summary>
    public List<Tween> GetTweens;

    /// <summary>
    /// 지도를 띄울때 안보이게 해줄 이미지들입니다
    /// </summary>
    public GameObject[] HpgameObjects;

    /// <summary>
    /// 레이어 컨트롤용의 불입니다
    /// </summary>
    private bool cont = false;

    #region 설명서 띄우기

    /// <summary>
    /// 마우스를 올렸을떄 나타나는 오브젝트입니다
    /// </summary>
    public GameObject MText;

    /// <summary>
    /// 마우스가 포인트 위에있는지 확인용입니다
    /// </summary>
    public bool Hovering;

    /// <summary>
    /// 항목에 맞는 글자를 띄우도록 돕습니다
    /// </summary>
    public int TextNumb;
    #endregion

    #region 설명서 띄우기B

    /// <summary>
    /// 마우스를 올렸을떄 나타나는 오브젝트입니다
    /// </summary>
    public GameObject MTextB;

    /// <summary>
    /// 마우스가 포인트 위에있는지 확인용입니다
    /// </summary>
    public bool HoveringB;

    /// <summary>
    /// 항목에 맞는 글자를 띄우도록 돕습니다
    /// </summary>
    public string NameB;

    /// <summary>
    /// 항목에 맞는 글자를 띄우도록 돕습니다
    /// </summary>
    public string TextB;
    #endregion

    /// <summary>
    /// 접히고 펼쳐지는 소리입니다
    /// </summary>
    [SerializeField]
    private AudioSource GetAudioSource;

    #region 기본함수

    private void Awake()
    {
        GetTweens = new List<Tween>();
    }

    private void Start()
    {
        //for (int i = 0; i < GameManager.G_M.MapList.Count; i++)
        //{
        //    for (int i2 = 0; i2 < GameManager.G_M.MapList[i].Count; i2++)
        //    {
        //        Debug.Log(i + "번째 가로줄 " + i2 + "번째 세로줄 " + GameManager.G_M.MapList[i][i2]);
        //    }
        //}

        //Debug.Log("기본값" + GameManager.G_M.MapList.Count);
        //
        //Debug.Log("세이브데이터" + GameManager.G_M.MapList.Count);

        GameManager.G_M.MapList = GameManager.G_M.MapList;

        //Debug.Log("기본값" + GameManager.G_M.MapList.Count);
        //
        //Debug.Log("세이브데이터" + GameManager.G_M.MapList.Count);

        MapINstance();
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        //Invoke("MapINstance", 0.5f);
        MapINstance();
    }

    private void Update()
    {
        // 탭키를 누르면 반응합니다
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            UpDown();



        }
        MTMANAGER();
        NextMap();
        //MapINstance();
    }


    #endregion

    /// <summary>
    /// 씬 생성시 맵의 값을 지정해줍니다
    /// </summary>
    public void MapINstance()
    {
        if (this == null)
        {
            return;
        }

        // 맵 값이 잘 들어있을때만 실시합니다
        if (GameManager.G_M.MapList != null)
        //if(GameManager.G_M.OKTEst == true)
        {

            //Debug.Log(GameManager.G_M.MapList[GameManager.G_M.MapList.Count - 1]
            //                                 [GameManager.G_M.MapList[GameManager.G_M.MapList.Count - 1].Count - 1]);
            // 자신의 직속아들 - 세로줄
            for (int i = 0; i < transform.childCount; i++)
            {
                // 세로줄(직속아들)의 아들 - 단일맵
                for (int i2 = 0; i2 < transform.GetChild(i).childCount; i2++)
                {
                    //Debug.Log(i + "번째 가로줄 " + i2 + "번째 세로줄 " + GameManager.G_M.MapList[i][i2]);
                    // 단일맵에 게임매니저의 맵정보를 집어넣습니다
                    transform.GetChild(i).GetChild(i2).gameObject.GetComponent<PointManager>().IncSprite =
                        GameManager.G_M.MapList[i][i2];

                    // 맵의 활성화 정보를 집어넣습니다

                    // 디폴트값일땐 적용되지않습니다
                    if (GameManager.G_M.MGA != 100)
                    {
                        // 그리고 현재 맵의 위치에 따라서 맵의 접근상태를 지정합니다

                        // 0. 일단 전부 0이됩니다
                        transform.GetChild(i).GetChild(i2).gameObject.GetComponent<PointManager>().ACCMap = 0;


                        // 1. 거리에 따라서 활성화 비활성으로 나뉩니다

                        // 현재 존재하는 맵보다 왼쪽(이미지나온곳)과 같은줄에 있다면 전부 1번으로합니다
                        if (i <= GameManager.G_M.MGA)
                        {

                            transform.GetChild(i).GetChild(i2).gameObject.GetComponent<PointManager>().ACCMap = 1;
                        }
                        // 현재 존재하는 맵+1보다 초과한 나중의것은 2의 값을 넣습니다
                        if (i > GameManager.G_M.MGA + 1)
                        {

                            transform.GetChild(i).GetChild(i2).gameObject.GetComponent<PointManager>().ACCMap = 2;
                        }

                        // 2. 맨위, 맨아래는 이동할 수 없는 맵입니다

                        // 홀수번째든 짝수번째든 내가 맨아래에 있다면 맨위의 맵으로 못가고 맨 위에잇다면 맨아래로못갑니다


                    }
                    if(GameManager.G_M.MGA == 100)
                    {

                        transform.GetChild(i).GetChild(i2).gameObject.GetComponent<PointManager>().ACCMap = 2;

                        // 첫줄 전부에 엑세스 가능하게 합니다
                        transform.GetChild(0).GetChild(i2).gameObject.GetComponent<PointManager>().ACCMap = 0;
                    }



                }

            }
            // 하지만 이 규칙은 맨 끝맵에서는 적용되지 않습니다
            // 
            if (GameManager.G_M.MGA < 5)
            {

                // 맨 위에 있을때
                if (GameManager.G_M.MSE == 0)
                {
                    // 바로 다음줄에있는 맵중에서
                    // 맨 끝에있는 맵에 2번을 넣습니다
                    int index = transform.GetChild(GameManager.G_M.MGA + 1).childCount - 1;

                    transform.GetChild(GameManager.G_M.MGA + 1).GetChild(index).gameObject.GetComponent<PointManager>().ACCMap = 2;

                }

                // 맨 아래에 있을때
                if (GameManager.G_M.MSE == transform.GetChild(GameManager.G_M.MGA).childCount - 1)
                {
                    // 바로 다음줄에있는 맵중에서
                    // 맨 처음에있는 맵에 2번을 넣습니다
                    transform.GetChild(GameManager.G_M.MGA + 1).GetChild(0).gameObject.GetComponent<PointManager>().ACCMap = 2;

                }

            }

            // 현재 있는맵에는 있다는 표시를 합니다
            if (GameManager.G_M.MGA != 100)
                transform.GetChild(GameManager.G_M.MGA).GetChild(GameManager.G_M.MSE).GetComponent<PointManager>().ACCMap = 3;

            Pontwig();


        }
        else
        {
            GameManager.G_M.DungeanRand();
        }
 
    }

    /// <summary>
    /// 포인트의 위글링입니다
    /// </summary>
    public void Pontwig()
    {
                    // 자신의 직속아들 - 세로줄
            for (int i = 0; i < transform.childCount; i++)
            {
                // 세로줄(직속아들)의 아들 - 단일맵
                for (int i2 = 0; i2 < transform.GetChild(i).childCount; i2++)
                {
                transform.GetChild(i).GetChild(i2).gameObject.GetComponent<PointManager>().OnDestroy();
                    // 반짝이는걸 시작합니다
                    transform.GetChild(i).GetChild(i2).gameObject.GetComponent<PointManager>().wigstart();
                }
            }
    }


    /// <summary>
    /// 설명서를 띄워주도록 돕습니다
    /// </summary>
    private void MTMANAGER()
    {
        #region 맵 설명서

        // 마우스가 어떤 항목에 올라가있다면
        if (Hovering == true)
        {

            // 설명서를 활성화하고
            MText.SetActive(true);

            // 항목에 맞는 설명을 활성화하고
            MText.transform.GetChild(TextNumb).gameObject.SetActive(true);

            // 마우스옆으로 옮긺니다
            //Vector2 mousePos = new Vector2(Input.mousePosition.x/ 160-10.1f, Input.mousePosition.y/160-5.1f);
            Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            MText.GetComponent<RectTransform>().position = mousePos;
            //MText.GetComponent<RectTransform>().localScale = new Vector2(Screen.width*0.001f,
            //                                                             Screen.width * 0.001f);



        }
        // 아니라면
        else
        {
            // 텍스트들부터 비활성화합니다
            for (int i = 0; i < MText.transform.childCount; i++)
            {
                MText.transform.GetChild(i).gameObject.SetActive(false);
            }

            // 비활성화합니다
            MText.SetActive(false);
        }
        // 호버링을 비활성화해서 보여지는일 없게합니다
        Hovering = false;

        #endregion

        #region bless 설명서

        // 마우스가 어떤 항목에 올라가있다면
        if (HoveringB == true)
        {
            
            // 설명서를 활성화하고
            MTextB.SetActive(true);

            // 마우스옆으로 옮긺니다
            //Vector2 mousePos = new Vector2(Input.mousePosition.x/ 160-10.1f, Input.mousePosition.y/160-5.1f);
            Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y-MTextB.GetComponent<RectTransform>().rect.width);
            MTextB.GetComponent<RectTransform>().position = mousePos;
            //MText.GetComponent<RectTransform>().localScale = new Vector2(Screen.width*0.001f,
            //                                                             Screen.width * 0.001f);

            // 텍스트를 지정합니다
            // 제목을 바꿔줍니다
            MTextB.transform.GetChild(0).GetComponent<Text>().text = NameB;

            // 설명을 바꿔줍니다
            MTextB.transform.GetChild(1).GetComponent<Text>().text = TextB;



        }
        // 아니라면
        else
        {

            // 비활성화합니다
            MTextB.SetActive(false);
        }
        // 호버링을 비활성화해서 보여지는일 없게합니다
        HoveringB = false;

        #endregion


    }

    Tween UDtw;




    /// <summary>
    /// 지도를 올리거나 내리는 기능을 합니다
    /// </summary>
    public void UpDown()
    {
        // 움직일 수 없다면 작동하지 않습니다
        if (MoveAble == false)
        {
            return;
        }



        CanvasControll();
        // 소리를 출력합니다
        GetAudioSource.volume = GameManager.G_M.MainSound;
        GetAudioSource.Play();

        // 지도가 닫혀있다면 펼칩니다
        if (UDB == false)
        {
            UDtw = GetComponent<RectTransform>().DOAnchorPosY(-190, 0.8f)
                .SetEase(Ease.OutCubic);
            // 지도가 펼쳐졌다고 저장합니다
            UDB = true;

            // 지도가 펴진 동안은 다른 콜라이더가 무시됩니다
            //for (int i = 0; i < boxCollider2Ds.Count; i++)
            //{
            //    boxCollider2Ds[i].enabled = false;
            //}


        }
        // 지도가 열려있다면 닫습니다
        else
        {
            UDtw = GetComponent<RectTransform>().DOAnchorPosY(200, 0.8f)
                .SetEase(Ease.OutCubic);
            // 지도를 접었다고 저장합니다
            UDB = false;

            // 지도가 닫힌 동안엔 콜라이더가 활성화됩니다
            //for (int i = 0; i < boxCollider2Ds.Count; i++)
            //{
            //    boxCollider2Ds[i].enabled = true;
            //}


        }

    }



    /// <summary>
    /// 선택한 다음 맵으로 이동합니다
    /// </summary>
    public void NextMap()
    {
        // 이동가능할때만 작동합니다
        if(MoveAble == true)
        {
            // 마지막 방일경우 계산하지 않습니다
            if(GameManager.G_M.MGA == 6)
            {
                return;
            }

            // 위치가 기본값이면 작동하지 않습니다
            if(GameManager.G_M.MGA == 100)
            {


                // 선택지가 켜져있다면
                //if (NextObject.activeSelf == true)
                //{
                //    // 끕니다
                //    NextObject.SetActive(false);
                //}

                bool testbool = false;
                PointManager pointManager = transform.GetChild(0).GetChild(0).GetComponent<PointManager>();
                int inputi = 0;

                // 다음열에 선택된 맵이 있나 없나 찾아봅니다
                for (int i = 0; i < transform.GetChild(0).childCount; i++)
                {
                    // 다음열의 맵중 1개를 지정한것 입니다
                    pointManager = transform.GetChild(0).GetChild(i).GetComponent<PointManager>();
                    // 불이켜진 맵이 있는지 여부입니다
                    if (pointManager.LiBool == true)
                    {
                        testbool = true;
                        inputi = i;
                    }
                }

                // 선택지가 꺼져있다면
                if (testbool == true)
                {
                    // 킵니다
                    if (NextObject.activeSelf == false)
                    {
                        NextObject.SetActive(true);

                    }




                    // 그리고 불이켜진 맵의 정보를 받아옵니다
                    if (pointManager.IncSprite == 2) // 맵의 정보가 rest(2)일경우 다른맵으로 가고 나머진 전투맵으로 갑니다
                    {
                        NextObject.GetComponent<Nextmanafer>().nextTYpe = 0;
                    }
                    else // 전투맵으로 갑니다
                    {
                        NextObject.GetComponent<Nextmanafer>().nextTYpe = 1;
                    }

                    // 활성화된곳의 가로좌표를 넣습니다
                    NextObject.GetComponent<Nextmanafer>().NGA = 0;
                    // 활성화된곳의 세로좌표를 넣습니다
                    NextObject.GetComponent<Nextmanafer>().NSE = inputi;


                }
                else
                {
                    NextObject.SetActive(false);

                }

                return;
            }

            // 선택지가 켜져있다면
            if (NextObject.activeSelf == true)
            {
                // 끕니다
                NextObject.SetActive(false); 
            }

            // 다음열에 선택된 맵이 있나 없나 찾아봅니다
            for (int i = 0; i < transform.GetChild(GameManager.G_M.MGA+1).childCount; i++)
            {
                // 다음열의 맵중 1개를 지정한것 입니다
                PointManager pointManager = transform.GetChild(GameManager.G_M.MGA + 1).GetChild(i).GetComponent<PointManager>();
                // 불이켜진 맵이 있는지 여부입니다
                if (pointManager.LiBool == true)
                {
                    // 선택지가 꺼져있다면
                    if (NextObject.activeSelf == false)
                    {
                        // 킵니다
                        NextObject.SetActive(true);




                        // 그리고 불이켜진 맵의 정보를 받아옵니다
                        if (pointManager.IncSprite == 2) // 맵의 정보가 rest(2)일경우 다른맵으로 가고 나머진 전투맵으로 갑니다
                        {
                            NextObject.GetComponent<Nextmanafer>().nextTYpe = 0;
                        }
                        else // 전투맵으로 갑니다
                        {
                            NextObject.GetComponent<Nextmanafer>().nextTYpe = 1;
                        }

                        // 활성화된곳의 가로좌표를 넣습니다
                        NextObject.GetComponent<Nextmanafer>().NGA = GameManager.G_M.MGA + 1;
                        // 활성화된곳의 세로좌표를 넣습니다
                        NextObject.GetComponent<Nextmanafer>().NSE = i;


                    }
                }
            }


        }
    }

    /// <summary>
    /// 씬 이동시 두트윈의 에러를 방지합니다
    /// </summary>
    private void OnDestroy()
    {
        UDtw.Kill();

        for (int i = 0; i < GetTweens.Count; i++)
        {
            GetTweens[i].Kill();
        }
    }

    /// <summary>
    /// 레이어가 순서가 뒤죽박죽 되는걸 막기위해 체력바 관련들을 안보이게 합니다
    /// </summary>
    /// <param name="instant">즉시 유무입니다</param>
    public void CanvasControll(bool instant = false)
    {
        // 체력바들을 담을 배열을 동적할당하고 대입합니다
        HpgameObjects = new GameObject[GameObject.FindGameObjectsWithTag("HPbar").Length];
        HpgameObjects = GameObject.FindGameObjectsWithTag("HPbar");



        // 다른 버튼들도 관리합니다
        GameObject[] changebutton =
            new GameObject[GameObject.FindGameObjectsWithTag("ChangeButton").Length];
        changebutton = GameObject.FindGameObjectsWithTag("ChangeButton");


        // 레이어가 이미 꺼져있다면
        if (cont == false)
        {
            // 즉시 실행합니다
            if (instant == true)
            {
                // 지도를 펼쳤다면 체력바들을 투명하게 만듭니다
                for (int i = 0; i < HpgameObjects.Length; i++)
                {
                    HpgameObjects[i].GetComponent<HpBar>().FadeOut(0);
                }

                // 교체버튼도 투명하게 만듭니다
                for (int i = 0; i < changebutton.Length; i++)
                {
                    changebutton[i].GetComponentInChildren<Image>().DOFade(0, 0);
                }
            }
            else
            {
                // 지도를 펼쳤다면 체력바들을 투명하게 만듭니다
                for (int i = 0; i < HpgameObjects.Length; i++)
                {
                    HpgameObjects[i].GetComponent<HpBar>().FadeOut();
                }

                // 교체버튼도 투명하게 만듭니다
                for (int i = 0; i < changebutton.Length; i++)
                {
                    changebutton[i].GetComponentInChildren<Image>().DOFade(0, 0.6f);
                }
            }

            cont = true;
        }
        // 레이어가 켜져있다면
        else
        {


            // 즉시 실행합니다
            if (instant == true)
            {

                // 지도를 접었다면 체력바들을 보이게 만듭니다
                for (int i = 0; i < HpgameObjects.Length; i++)
                {
                    HpgameObjects[i].GetComponent<HpBar>().FadeIN(0);

                }

                // 교체버튼도 보이게 만듭니다
                for (int i = 0; i < changebutton.Length; i++)
                {
                    changebutton[i].GetComponentInChildren<Image>().DOFade(1, 0);
                }
            }
            else
            {

                // 지도를 접었다면 체력바들을 보이게 만듭니다
                for (int i = 0; i < HpgameObjects.Length; i++)
                {
                    HpgameObjects[i].GetComponent<HpBar>().FadeIN();

                }

                // 교체버튼도 보이게 만듭니다
                for (int i = 0; i < changebutton.Length; i++)
                {
                    changebutton[i].GetComponentInChildren<Image>().DOFade(1, 0.6f);
                }
            }



            cont = false;
        }
    }


}
