using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.IO;

public class GameManager : MonoBehaviour
{
    #region 게임매니저

    /// <summary>
    /// 게임매니저는 싱글톤 패턴입니다. 게임매니저를 담아두는 인스턴스입니다.
    /// </summary>
    private static GameManager instance = null;

    /// <summary>
    /// 게임매니저 인스턴스에 접근가능한 프로퍼티
    /// </summary>
    public static GameManager G_M
    {
        // get는 값을 반환하기위헤 사용합니다.
        get
        {
            // 인스턴스가 널이라면 똑같이 널을 반환합니다.
            if (instance == null)
            {
                return null;
            }

            // 그렇지 않다면 인스턴스를 제대로 반환합니다.
            return instance;
        }

    }

    /// <summary>
    /// 게임매니저가 Awake떄 해야할 일들입니다.
    /// </summary>
    private void GameMangerAwake()
    {
        // 만약에 인스턴스가 비어있다면
        if (instance == null)
        {
            // 이 클래스 인스턴스가 탄생했을 때 전역변수 instance에 게임매니저 인스턴스가
            // 담겨있지 않다면 자신을 넣어준다.
            instance = this;

            // 씬 전환이 되어도 파괴되지 않게 한다.
            DontDestroyOnLoad(this.gameObject);


        }
        // 인스턴스가 비어있지 않다면
        else
        {
            // 씬 이동이 된 후에 그 씬에도 게임매니저가 있을수 있으니 새 씬의
            // 게임매니저를 삭제해 준다.
            Destroy(this.gameObject);
        }
    }

    #endregion

    /// <summary>
    /// 기본 소리입니다
    /// </summary>
    public AudioClip AudioClip;

    #region 세이브를 만든다면 저장해야할 데이터들

    /*
    /// <summary>
    /// 퐁들을 담아두는 리스트입니다.
    /// </summary>
    [SerializeField]
    private List<Pongs> PongsParty;

    /// <summary>
    /// 돈의 보유량입니다.
    /// </summary>
    [SerializeField]
    private int HaveMoney;

    /// <summary>
    /// 맵의 포인터에 어떤맵인지 값을 저장하는용도입니다
    /// </summary>
    public List<List<int>> MapList;

    /// <summary>
    /// 맵값인 이중리스트에 집어넣기전에 임시로 사용되는 리스트입니다
    /// </summary>
    public List<int> OTF;

    /// <summary>
    /// 가로줄입니다 
    /// </summary>
    public int MGA;

    /// <summary>
    /// 세로줄입니다 단일맵1개를 나타냅니다
    /// </summary>
    public int MSE;

    /// <summary>
    /// 랜덤시드입니다
    /// </summary>
    private int RANDAMSEED;
    */

    /// <summary>
    ///  저장데이터입니다
    /// </summary>
    //public ScriptableGame SaveData;


    #region 운용데이터

    #region 맵들
    /// <summary>
    /// 맵의 포인터에 어떤맵인지 값을 저장하는용도입니다
    /// </summary>
    public List<List<int>> MapList;

    /// <summary>
    /// 맵값인 이중리스트에 집어넣기전에 임시로 사용되는 리스트입니다
    /// </summary>
    public List<int> OTF;

    /// <summary>
    /// 가로줄입니다 최대 7개있습니다
    /// </summary>
    public int MGA;

    /// <summary>
    /// 세로줄입니다 단일맵1개를 나타냅니다
    /// </summary>
    public int MSE;
    #endregion

    #region 퐁들
    /// <summary>
    /// 퐁들을 담아두는 리스트입니다.
    /// </summary>
    public List<Pongs> PongsParty;
    #endregion

    #region 장비들

    /// <summary>
    /// 머리의 리스트입니다
    /// </summary>
    public GameObject[] Equips1;

    /// <summary>
    /// 무기의 리스트입니다
    /// </summary>
    public GameObject[] Equips2;
    #endregion

    #region 축복들
    /// <summary>
    /// 축복의 리스트입니다
    /// </summary>
    public List<GameObject> Blesses;
    #endregion


    /// <summary>
    /// 돈의 보유량입니다.
    /// </summary>
    private int HaveMoney;

    /// <summary>
    /// 랜덤시드입니다
    /// </summary>
    public int RANDAMSEED;

    /// <summary>
    /// 전체적인 소리의 크기입니다
    /// </summary>
    public float MainSound;

    #endregion


    #region 시드프로퍼티

    /// <summary>
    /// 시드값을 받아옵니다
    /// </summary>
    /// <returns></returns>
    public int GetSEED()
    {
        return RANDAMSEED;
    }

    /// <summary>
    /// 주의-게임내에서 시작시 단1번만 실행해 주시길 바랍니다
    /// </summary>
    public void SetSEED(int index)
    {
        RANDAMSEED = index;
    }
    #endregion



    #endregion

    #region 데이터 인풋 아웃풋을 해봅니다

    // 불러온 데이터를 담아둘 스트링입니다
    public string Line;

    /// <summary>
    /// 퐁들의 기본정보들을 가져옵니다
    /// </summary>
    /// <param name="index"></param>
    private void ReadFile(string index)
    {
        // 파일을 읽어오는 2개의 코드입니다
        TextAsset textAsset = Resources.Load(index) as TextAsset;
        StringReader stringReader = new StringReader(textAsset.text);

        // 가로줄입니다 직업명, 최대체력, 체력, 공격력, 타입 순으로 나열됩니다
        int indexX = 0;

        // 값이 빌떄까지 계속합니다
        while (stringReader != null)
        {
            // 맨 첫줄은 표의 설명서가 들어있으니 넘깁니다
            if (indexX == 0)
            {
                Line = stringReader.ReadLine();
                indexX++;
                continue;
            }
            indexX++;

            Line = stringReader.ReadLine();

            if (Line == null)
            {
                break;
            }



            // 맨 첫값은 직업이름이 들어있으니 넘깁니다
            // i가 1이면 a번쨰 2이면b번째를 의미합니다
            for (int i = 1; i < Line.Split(',').Length; i++)
            {
                // 아무것도 안적힌 칸은 넘깁니다
                if (Line.Split(',')[i] == "")
                {
                    continue;
                }
                string test = "";

                // 전부 더해준 글자를 출력합니다

                for (int ii = 0; ii < Line.Split(',')[i].Split('#').Length; ii++)
                {
                    test += Line.Split(',')[i].Split('#')[ii];
                    // #이 있는 부분에서 자른 다음에 원하는 글자를 집어 넣어야합니다
                    // #이 없는 배열은 길이가 1일테니 1이 아닐때 작동합니다
                    // #이 있는 배열도 마지막에서 더해질 수 있으니 길이-1 == ii일떈 작동하지 않습니다
                    // #이 있는 배열은 # 1개당 길이가 1 늘어납니다
                    // 
                    if (Line.Split(',')[i].Split('#').Length != 1 &&
                        Line.Split(',')[i].Split('#').Length - 1 != ii)
                    {
                        test += "1";
                    }


                }
                //Debug.Log(test);
            }

        }


    }



    #endregion

    #region Pongs프로퍼티

    /// <summary>
    /// 직업들을 파티에 추가시켜줍니다.
    /// </summary>
    /// <param name="index">직업의 열거형을 적어주세요</param>
    public void AddPongs(int index)
    {
        

        // 열거형에 따라서 직업을 추가시킵니다.
        switch (index)
        {
            case (int)PongClass.Flag:
                // 퐁들의 파티에 컴포넌트를 더한후 리스트에 넣어줍니다.
                PongsParty.Add(gameObject.AddComponent<Flag>());

                PongsParty[PongsParty.Count - 1].PongsData = Default[(int)PongClass.Flag];

                break;

            case (int)PongClass.Shield:
                PongsParty.Add(gameObject.AddComponent<Shield>());
;
                PongsParty[PongsParty.Count - 1].PongsData = Default[(int)PongClass.Shield];

                break;

            case (int)PongClass.Spear:
                PongsParty.Add(gameObject.AddComponent<Spear>());

                PongsParty[PongsParty.Count - 1].PongsData = Default[(int)PongClass.Spear];

                break;

            case (int)PongClass.Bow:
                PongsParty.Add(gameObject.AddComponent<Bow>());

                PongsParty[PongsParty.Count - 1].PongsData = Default[(int)PongClass.Bow];

                break;

            default:
                break;

        }


    }

    /// <summary>
    /// 퐁을 리턴하는 프로퍼티입니다.
    /// </summary>
    /// <param name="index"></param>
    /// <returns> index번째의 퐁입니다. </returns>
    public Pongs GetPongs(int index)
    {
        if (index >= PongsParty.Count)
        {
            // 파티의 크기보다 큰 순번의 퐁을 찾으려고했다면 널을 반환해줍니다.
            return null;
        }

        // index번째의 퐁을 반환해줍니다.
        return PongsParty[index];
    }

    /// <summary>
    /// 퐁리스트에 접근하는 프로퍼티입니다.
    /// </summary>
    public void SetPongs(int index, Pongs pongs)
    {
        PongsParty[index] = pongs;
    }

    public int PartyCount()
    {
        return PongsParty.Count;
    }

    /// <summary>
    /// 무작위로 파티원을 추가시킵니다 겹침없이
    /// </summary>
    public void RandAddParty()
    {
        // 퐁을 무작위로 생성합니다
        int rand = Random.Range(0,(int)PongClass.END);

        // 파티의 최대치까지 비교해서 같은게 있나 시험합니다
        for (int i = 0; i < PartyCount(); i++)
        {
            // 만약 같다면 다시 랜덤을 돌립니다
            if(PongsParty[i].PongsData.GetPongType() == rand)
            {
                RandAddParty();
                return;
            }
        }

        // 같은게 하다도 없었다면 새로 만들어줍니다
        AddPongs(rand);

    }

    #endregion

    #region 돈 프로퍼티
    public int GetMoney()
    {
        return HaveMoney;
    }

    public void SetMoney(int index)
    {
        HaveMoney = index;
    }

    public void PlusMoney(int index)
    {
        HaveMoney += index;
    }

    public void MoneyDown(int index)
    {
        if(index > HaveMoney)
        {
            HaveMoney = 0;
        }
        else
        {
            HaveMoney -= index;
        }
        
    }
    #endregion

    /// <summary>
    /// 직업들의 스프라이트입니다.
    /// </summary>
    public List<Sprite> ClassSprites;

    /// <summary>
    /// 퐁들의 스크립터블 오브젝트입니다
    /// </summary>
    public List<ScriptablePongs> Default;

    /// <summary>
    /// 실행이 가능한지의 여부입니다
    /// </summary>
    //public bool OKTEst;


    #region 기본 함수

    private void Awake()
    {
        //MapList = new List<List<int>>();
        //MapList.Clear();


        //SaveData.MapListFull = new List<int>();
        //SaveData.MapListFull.Clear();

        GameMangerAwake();


        #region 운용 데이터 초기화
        Blesses = new List<GameObject>();
        Equips1 = new GameObject[4];
        Equips2 = new GameObject[4];
        #endregion

    }



    #endregion

    #region 씬 매니저들

    /// <summary>
    /// 변화하고 있다고 표시합니다 이동안은 씬 이동기능이 먹히지 않습니다
    /// </summary>
    [SerializeField]
    private bool Changing;

    /// <summary>
    /// 씬이 변화하는 시간입니다
    /// </summary>
    const float chtime = 0.3f;


    /// <summary>
    /// 씬을 교체합니다.
    /// </summary>
    public void ChangeScene(string index)
    {
        // 씬이 바뀌는중이라면 시도되지않습니다
        if (Changing == false)
        {
            // 씬이 바뀌고있다고 알려줍니다
            Changing = true;
            // 씬이 바뀌는동안 화면을 가립니다
            gameObject.GetComponent<SpriteRenderer>().DOFade(1, chtime);
            // 씬을 바꿔주는 코루틴을 실행합니다
            StartCoroutine(MoveTos(index));

        }
    }

    /// <summary>
    /// 씬 교체시 페이드아웃등을 해주는 함수입니다
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    IEnumerator MoveTos(string index)
    {
        // 대기시간동안 기다리도록합니다
        yield return new WaitForSeconds(chtime);
        // 씬이 바뀌는중을 취소합니다
        Changing = false;





        // 씬을 불러옵니다
        SceneManager.LoadScene(index);


        // 씬 가리개를 해제합니다
        gameObject.GetComponent<SpriteRenderer>().DOFade(0, chtime).SetEase(Ease.OutCubic);
    }

    #endregion

    #region 맵 관련들

    /// <summary>
    /// 맵리스트를 초기화해줍니다
    /// </summary>
    public void MapNEw()
    {
        //SaveData.MapList = new List<List<int>>();
    }


    public int EnemyMapInt;
    public int RandMapInt;
    public int RestMapInt;
    public int CoinMapInt;

    /// <summary>
    /// 맵을 랜덤으로 지정해줍니다 DungeanRand의 도구입니다
    /// </summary>
    /// <returns>반납하는 맵의 값입니다</returns>
    private int MapRand()
    {
        int Returnint = Random.Range(0, RandMapInt + EnemyMapInt + RestMapInt + CoinMapInt);

        int Rangeint = 0;

        // 랜덤변수가 에너미인트이하일경우
        if (Returnint < Rangeint + RandMapInt)
        {
            // 에너미이벤트를 반납합니다
            return 0;
        }

        // 에너미인트를 추가시킵니다
        Rangeint += RandMapInt;

        // 랜덤변수가 에너미인트초과, 에너미인트+랜덤인트 이하일경우
        if (Rangeint <= Returnint && Returnint < Rangeint + EnemyMapInt)
        {
            // 랜덤이벤트를 반납합니다
            return 1;
        }

        // 랜덤인트를 추가시킵니다
        Rangeint += EnemyMapInt;

        // 에너미인트+랜덤인트초과, 에너미인트+랜덤인트+휴식인트 이하일경우
        if (Rangeint <= Returnint && Returnint < Rangeint + RestMapInt)
        {
            // 휴식이벤트를 반납합니다
            return 2;
        }

        // 휴식인트를 추가시킵니다
        Rangeint += RestMapInt;

        // 랜덤변수가 에너미인트+랜덤인트+휴식인트초과,
        // 에너미인트+랜덤인트+휴식인트+코인인트 이하일경우
        if (Rangeint <= Returnint && Returnint < Rangeint + CoinMapInt)
        {
            // 상점이벤트를 반납합니다
            return 3;
        }

        // 예외처리입니다
        return 0;
    }

    //public List<int> OTF;

    /// <summary>
    /// 던전을 생성하는 랜덤변수입니다
    /// </summary>
    public void DungeanRand()
    {
        MapList = new List<List<int>>();
        MapList.Clear();
        OTF.Clear();
        // 던전 생성시 생각할점
        // 1. 마지막은 무조건 보스
        // 2. 중앙은 무조건 회복및 상점으로 예상중
        // 3. 3, 2, 3, 2(회), 3, 2(둘중1개는 상점?), 1(보)
        // 확률체크
        // 45프로 랜덤 45프로 적 5프로 상점 5프로 휴식처
        // 0~8 9개     9~17 9개  18~18 1개   19~19 1개  

        // 이차원리스트
        // 1번째는 세로줄
        // 2번째는 가로줄

        // 리스트의 가로길이는 7이됩니다
        // 그래서 포문을 돌려 7개의 리스트를 추가합니다
        // 리스트별로 각각 특징이 있습니다

        // 1,3,5번째 세로줄은 3개의 랜덤
        // 2번째 세로줄은 2개의 랜덤
        // 4번째 세로줄은 2개의 쉼터
        // 6번째 세로줄은 1개는 상점, 1개는 랜덤
        // 7번째 세로줄은 1개의 보스

        // 총 16개의 맵이 나타답니다

        for (int i = 0; i < 7; i++)
        {
            // 더해줄 인트리스트입니다
            OTF = new List<int>();
            // 더해줄 인트리스트에 이것저것 집어넣습니다.
            switch (i)
            {
                // 1,3,5번째 세로줄
                case 0:case 2:case 4:
                    // 3개의 맵을 집어넣습니다
                    for (int i2 = 0; i2 < 3; i2++)
                    {
                        // 랜덤으로 돌려서 리스트에 집어넣습니다
                        OTF.Add(MapRand());
                    }

                    break;

                // 2번째 세로줄
                case 1:
                    // 2개의 맵을 집어넣습니다
                    for (int i2 = 0; i2 < 2; i2++)
                    {
                        // 랜덤으로 돌려서 리스트에 집어넣습니다
                        OTF.Add(MapRand());
                    }

                    break;
                // 4번째 세로줄 (무조건 쉼터2개가 나옵니다)
                case 3:
                    // 1번째 맵추가
                    OTF.Add((int)Maps.Rest);
                    // 2번째 맵추가
                    OTF.Add((int)Maps.Rest);
                    break;

                    // 6번째 세로줄
                case 5:
                    // 둘중 1개는 상점 1개는 랜덤맵입니다
                    int Half = Random.Range(0, 2);

                    if(Half == 0)
                    {
                        // 1번째 맵추가
                        OTF.Add((int)Maps.Coin);
                        // 2번째 맵추가
                        OTF.Add(MapRand());
                    }
                    else
                    {
                        // 1번째 맵추가
                        OTF.Add(MapRand());
                        // 2번째 맵추가
                        OTF.Add((int)Maps.Coin);
                    }

                    break;

                    // 7번째 세로줄 (무조건 보스1개가 나옵니다)
                case 6:
                    // 1번째 맵추가
                    OTF.Add((int)Maps.Boss);
                    break;
            }

            // 인스리스트를 더합니다
            MapList.Add(OTF);
        }

        // 디버그용입니다
        //for(int i = 0; i < SaveData.MapList.Count; i++)
        //{
        //    for (int i2 = 0; i2 < SaveData.MapList[i].Count; i2++) 
        //    {
        //        Debug.Log(i + "번째 가로줄 " + i2 + "번째 세로줄 " + SaveData.MapList[i][i2]);
        //    }
        //}
    }
    #endregion


    // 일단 내가 해야하는것은 무엇인가
    // 로그라이크형 게임
    // 파티원을 모집해 싸우는 게임
    // 전투방식은?
    // 타이머가 지낢때마다 자기혼자 스킬을 쓰는 구조
    // 배속이 필요할수도

    // 


    /// <summary>
    /// 랜덤을 처리해주는 함수입니다 n분의 1입니다
    /// </summary>
    /// <param name="randmax">n분의 1 입니다</param>
    /// <returns>n분의 1이 되었으면 true를 반환합니다</returns>
    public bool RandManager(int n = 1)
    {
        int rand = Random.Range(0,n);

        // n분의 1이 되었습니다
        if(rand == 0)
        {
            return true;
        }

        return false;
    }


}

/// <summary>
/// 씬 이동시 어떤씬으로 가는지 설명할떄 씁니다
/// </summary>
enum S_
{ 
    Start, Pick, Main, Battle
}

/// <summary>
/// 맵에서 어떤맵인지 나타낼떄 씁니다
/// </summary>
enum Maps
{
    Random, Enemy, Rest, Coin, Boss
}
