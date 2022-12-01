
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BAManager : MonoBehaviour
{

    // 전투 매커니즘을 짜봅시다

    #region 스크롤바 관련

    /// <summary>
    /// 기본 스크롤바입니다
    /// </summary>
    [SerializeField]
    private Scrollbar BAR;

    /// <summary>
    /// 스크롤바의 값을 참조해서 겹쳣는지 확인하는 용도입니다
    /// </summary>
    [SerializeField]
    private List<Scrollbar> Bars;

    /// <summary>
    /// 움직이는 택바입니다
    /// </summary>
    [SerializeField]
    private GameObject MoveBA;

    /// <summary>
    /// 스크롤바가 아니라 진짜로 뜨는 네모박스들을 관리합니다
    /// </summary>
    [SerializeField]
    private List<GameObject> BAS;

    /// <summary>
    /// 택바의 진행속도입니다
    /// </summary>
    [SerializeField]
    private float TECSpeed;

    /// <summary>
    /// 우리팀의 택바가 도는 최대 횟수입니다
    /// </summary>
    [SerializeField]
    private int TECCycleMax;

    /// <summary>
    /// 적팀의 택바가 도는 최대 횟수입니다
    /// </summary>
    [SerializeField]
    private int ETECCycleMax;

    /// <summary>
    /// 우리팀의 택바가 돈 횟수입니다
    /// </summary>
    [SerializeField]
    private int TECCycle;

    /// <summary>
    /// 우리차례인지 아닌지를 체크합니다
    /// </summary>
    [SerializeField]
    private bool TeamBool;

    /// <summary>
    /// 우리꺼일때 스크롤바 색상입니다
    /// </summary>
    public Color TColor;

    /// <summary>
    /// 적꺼일때 스크롤바 색상입니다
    /// </summary>
    public Color EColor;

    #endregion

    #region 실패시 관리할것들

    /// <summary>
    /// 실패했을때 주어지는 행동불가시간입니다
    /// </summary>
    [SerializeField]
    private float MissCool;

    /// <summary>
    /// 실패한 남은시간입니다
    /// </summary>
    [SerializeField]
    private float MissTime;

    /// <summary>
    /// 기본색상입니다
    /// </summary>
    [SerializeField]
    private Color nomal;

    /// <summary>
    /// 변화색상입니다
    /// </summary>
    [SerializeField]
    private Color MissColor;

    #endregion

    /// <summary>
    /// 배틀씬 매니저를 얻어옵니다
    /// </summary>
    public BattleSceneManager BattleSceneManager;

    #region 기본함수

    private void Start()
    {
        nomal = MoveBA.GetComponent<Image>().color;
    }

    private void Update()
    {
        if (BattleSceneManager.BattleStart == true)
        {
            KeyIndex(); 
        }

        // 실패시 색상을 조절합니다
        if(MissTime > 0)
        {
            MoveBA.GetComponent<Image>().color = new Color(255/255f, 122/255f, 122/255f, 255/255f);
        }
        else
        {
            MoveBA.GetComponent<Image>().color = new Color(255, 255, 255);
        }
    }

    #endregion

    /// <summary>
    /// 타겟의 넘버입니다
    /// </summary>
    public int targetnumb;

    /// <summary>
    /// 적의 타겟의 넘버입니다
    /// </summary>
    public int Etargetnumb;

    /// <summary>
    /// 키를 입력받습니다
    /// </summary>
    private void KeyIndex()
    {
        #region 키관리들
        // 실패횟수를 계산합니다
        int missCheck = 0;


        if (MissTime > 0)
        {
            // 실패시간은 점점 줄어듭니다
            MissTime -= Time.deltaTime;
        }



        for (int i = 0; i < BAS.Count; i++)
        {
            // 인덱스란?

            // 움직이는 택바와 고정된 택바의 위치차이를 절대값으로 나타낸것 입니다.
            double index;


            // 절대값으로 표현되도록 합니다
            if (Bars[i].value < BAR.value)
            {
                index = BAR.value - Bars[i].value;
            }
            else
            {
                index = Bars[i].value - BAR.value;
            }

            // 인덱스가 일정값 이하일때 버튼을 눌럿을 경우에 발동된다

            // 일정값?

            // 일정값 = (움직이는 택바의 길이 + 고정된 택바의 길이) * 0.5 / 전체 택바의 길이 

            double index2 = (MoveBA.GetComponent<RectTransform>().rect.width 
                           + BAS[i].GetComponent<RectTransform>().rect.width)
                * 0.5 
                / BAR.gameObject.GetComponent<RectTransform>().rect.width;

            // 범위내일경우
            if (index < index2 && MissTime <= 0)
            {
                // 범위내에서 눌렀을경우 그리고 실패타임이 없을경우
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    // 그리고 성공하면 공격점수를 추가합니다 이미 성공한 박스는 중복되지 않습니다
                    if (BAS[i].GetComponent<BAS>().Contact == false)
                    {

                        switch (TeamBool)
                        {
                            // 우리턴에 성공했을경우
                            case true:
                                BattleSceneManager.Party[i].GetComponent<PongManager>().PlusAtack();
                                break;
                            // 상대턴에 성공했을경우
                            case false:
                                BattleSceneManager.Monsters.EnemyList[i].GetComponent<EnemyManager>().PowerDown();
                                break;
                        }

                    }



                    // 성공적으로 누른 대상을 활성화시킵니다
                    BAS[i].GetComponent<BAS>().Contacted();// = true; 


                }

            }
            // 범위내가 아닐경우
            else
            {
                // 범위밖에서 눌렀을경우
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    // 실패에 +1을합니다
                    missCheck++;
                    //MissTime = MissCool;

                    // 누구의 턴이냐에 따라 달라집니다
                    switch (TeamBool)
                    {
                        case true:

                            break;
                        case false:
                            break;
                    }

                }
            }

        }

        int Cheaker = 0;

        // 활성화된 숫자만큼 안맞을경우와 실패시간이 없을경우
        for(int i =0; i < BAS.Count; i++)
        {
            Cheaker++;
        }

        if(missCheck == Cheaker && MissTime <= 0)
        {
            // 실패해서 누를수없는시간이 됩니다
            MissTime = MissCool;

        }

        #endregion

        #region 바매니저들

        // 아군턴이맞다면 아군꺼만큼 적군꺼가 맞다면 적군꺼만큼 사이클이 돕니다
        int MaxCycle = TeamBool ? TECCycleMax : ETECCycleMax;
        // 택바를 최대횟수까지 돌립니다
        if (MaxCycle > TECCycle)
        {
            // 택바를 진행시킵니다
            BAR.value += Time.deltaTime * TECSpeed;

            // 택바가 일정수치를 넘으면 복귀시킵니다
            if (BAR.value > 1)
            {
                // 택바가 초기화됩니다
                BAR.value = 0;
                // 사이클횟수를 늘립니다
                TECCycle++;
                // 비활성화된 BAS도 활성화시켜줍니다
                for (int i = 0; i < BAS.Count; i++)
                {
                    BAS[i].GetComponent<BAS>().ReCont();
                }
            }
        }
        // 택바가 최대횟수만큼 돌았다면 상대턴으로 넘어갑니다
        if (MaxCycle == TECCycle)
        {
            // 사이클이 초기화됩니다
            TECCycle = 0;

            // 누구 턴이 끝낫냐에 따라 다릅니다
            switch(TeamBool)
            {
                // 아군의 턴이 끝난거라면 아군의 공격포인트를 소비해서 공격을 합니다
                case true:
                    int index = 0;
                    StartCoroutine(BattleDelay(index));
                    // 그 후 적군의 공격포인트가 재생됩니다
                    for(int i =0; i < BattleSceneManager.Monsters.EnemyList.Count; i++)
                    {
                        BattleSceneManager.Monsters.EnemyList[i].GetComponent<EnemyManager>().Recharge();
                    }

                    break;// 턴종료 - 소비, 턴중 - 생산
                // 적군의 턴이 끝난거라면 적군의 포인트에 따라 공격을 합니다
                case false:
                    int index2 = 0;
                    StartCoroutine(BattleDelayE(index2));
                    break;// 턴종료 - 생산, 턴중 - 소비
            }



            // 불체크를 뒤집어줍니다
            TeamBool = TeamBool ? false : true;


            // 텍바역시 재생성합니다
            TECSpawn();

        }
        // 뱀의 색상을 바꿔줍니다
        transform.GetChild(4).GetComponent<Image>().color = TeamBool ? TColor : EColor;

        #endregion

    }

    // 전투함수를 다시 짜봅니다

    #region 코루틴들

    /// <summary>
    /// 아군의 공격이 순차적으로 들어가도록합니다
    /// </summary>
    /// <param name="index">공격자의 순번입니다</param>
    /// <returns></returns>
    IEnumerator BattleDelay(int index)
    {
        // 공격자가 활성화 되어있을때만 작동합니다
        if (BattleSceneManager.Party[index].activeSelf == true)
        {
            // 공격대상을 지정합니다
            GameObject target = BattleSceneManager.Monsters.EnemyList[targetnumb];

            // 공격대상을 공격합니다
            BattleSceneManager.Party[index].GetComponent<PongManager>().ATAAAAAACK(target);

            // 공격대상이 비활성화됬다면 다음으로 넘깁니다
            if (target.activeSelf == false)
            {

                targetnumb++;


            }
            // 딜레이를 주는 부분입니다
            yield return new WaitForSeconds(0.3f);
            // 하지만 이미 적들이 전부 쓰러졌다면
            if (targetnumb == BattleSceneManager.Monsters.EnemyList.Count)
            {
                // 전투를 종료시키고 순환을 끝냅니다
                BattleSceneManager.Victory();
                // 퐁들의 포인트를 제거합니다
                for (int i = 0; i < BattleSceneManager.Party.Count; i++)
                {
                    BattleSceneManager.Party[i].GetComponent<PongManager>().ReMovePoints();
                }


            }
            else
            {
                // 그게아니라면 순환합니다

                // 4명의 파티라면 배열의 길이는 4 하지만 최대 숫자는 3
                // 즉 2에서 정지시켜야 [3]이 작동이 되고 [4]가 불리는 일이 없다
                // 최대 파티숫자-2에서 정지를 시켜야 멀쩡하다
                // 최대파티숫자-1 이상은 문제가 생긴다
                if (BattleSceneManager.Party.Count - 1 != index)
                {
                    StartCoroutine(BattleDelay(index + 1));
                }

            }
        }
        // 공격자가 활성화 된게 아니라면바로 다음대상으로 넘깁니다
        else
        {
            if (BattleSceneManager.Party.Count - 1 != index)
            {
                StartCoroutine(BattleDelay(index + 1));
            }
        }



    }

    /// <summary>
    /// 적군의 공격이 순차적으로 들어가게합니다
    /// </summary>
    /// <param name="index">공격자의 순번입니다</param>
    /// <returns></returns>
    IEnumerator BattleDelayE(int index)
    {
        // 공격자가 활성화 되어있을때만 작동합니다
        if(BattleSceneManager.Monsters.EnemyList[index].activeSelf == true)
        {
            // 공격대상을 지정합니다
            int Party = Etargetnumb;
            // 공격대상을 공격합니다
            BattleSceneManager.Monsters.EnemyList[index].GetComponent<EnemyManager>().EAttack(Party);

            // 공격대상이 비활성화됬다면 다음으로 넘깁니다
            if (BattleSceneManager.Party[Etargetnumb].activeSelf == false)
            {

                Etargetnumb++;


            }
            // 딜레이를 주는 부분입니다

            yield return new WaitForSeconds(0.3f);

            // 아군이 전부 비활성화됬다면 전투에서 패배합니다

            if (Etargetnumb == BattleSceneManager.Party.Count)
            {
                // 전투를 종료시키고 순환을 끝냅니다
                BattleSceneManager.Victory();
                // 퐁들의 포인트를 제거합니다
                for (int i = 0; i < BattleSceneManager.Party.Count; i++)
                {
                    BattleSceneManager.Party[i].GetComponent<PongManager>().ReMovePoints();
                }


            }
            else
            {
                // 그게아니라면 순환합니다

                // 4명의 파티라면 배열의 길이는 4 하지만 최대 숫자는 3
                // 즉 2에서 정지시켜야 [3]이 작동이 되고 [4]가 불리는 일이 없다
                // 최대 파티숫자-2에서 정지를 시켜야 멀쩡하다
                // 최대파티숫자-1 이상은 문제가 생긴다
                if (BattleSceneManager.Monsters.EnemyList.Count - 1 != index)
                {
                    StartCoroutine(BattleDelayE(index + 1));
                }

            }
        }
        // 공격자가 활성화 된게 아니라면바로 다음대상으로 넘깁니다
        else
        {
            if (BattleSceneManager.Monsters.EnemyList.Count - 1 != index)
            {
                StartCoroutine(BattleDelayE(index + 1));
            }
        }

    }

    #endregion

    /// <summary>
    /// 퐁이 행동하는 택바를 생성합니다
    /// </summary>
    public void PongTEC()
    {
        if(MoveBA.activeSelf == false)
        {
            // 퐁의 수만큼 네모바들을 더해줍니다
            //for (int i = 0; i < BattleSceneManager.Party.Count; i++)
            //{
            //    BAS.Add(transform.GetChild(4).GetChild(0).GetChild(i).gameObject);
            //    BAS[i].SetActive(true);
            //}

            // 퐁의 수보다 많은 네모바들은 비활성화됩니다
            //for (int i = BattleSceneManager.Party.Count; i < 4; i++)
            //{
            //    transform.GetChild(4).GetChild(0).GetChild(i).gameObject.SetActive(false);
            //}

            TECSpawn();

            MoveBA.SetActive(true);
        }
        else
        {
            // 재실행시 네모바들은 비활성화됩니다
            for (int i = 0; i < 4; i++)
            {
                transform.GetChild(4).GetChild(0).GetChild(i).gameObject.SetActive(false);
            }

            MoveBA.SetActive(false);
        }

    }

    /// <summary>
    /// 턴이 교체될때마다 적 또는 아군 상황에 맞에 텍바를 소환하는 역할을 합니다
    /// </summary>
    public void TECSpawn()
    {
        // 생성에 들어가기전에 전부 없앱니다
        for (int i = 0; i < BAS.Count; i++)
        {
            BAS[i].SetActive(false);
        }

        // 1. 턴 시작시 행동가능한 인원을 파악합니다
        int SpawnT = 0;

        switch(TeamBool)
        {
            // 아군 턴일떄 소환합니다
            case true:
                for(int i = 0; i < BattleSceneManager.Party.Count; i++)
                {

                    SpawnT++;
                }
                break;
            // 적군 턴일때 소환합니다
            case false:
                for (int i = 0; i < BattleSceneManager.Monsters.EnemyList.Count; i++)
                {
                    // 적군이 활성화된 숫자만큼 넣습니다
                    if (BattleSceneManager.Monsters.EnemyList[i].activeSelf == true)
                    {
                        SpawnT++;

                    }
                    //Debug.Log(SpawnT);
                }
                break;
        }

        // 2. 파악된 인원수 만큼 텍바를 소환합니다
        for (int i = 0; i < SpawnT; i++)
        {
            BAS[i].SetActive(true);
            
        }


        // 3. 턴 종료시 소환되있던 텍바를 전부 지웁니다

    }

}
