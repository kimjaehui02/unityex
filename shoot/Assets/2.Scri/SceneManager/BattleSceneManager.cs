using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class BattleSceneManager : MonoBehaviour
{
    #region 텍스트 레벨디자인
    // 텍스트 파일로 레벨디자인을 해봅시다.
    
    // 적이 소환되는 정보셋트를 지닌 구조체의 묶음입니다.
    public List<Level> levelsSave;
    // 에너미의 생성시도 횟수 입니다.
    public int spawnIndex;
    // 소환이 끝났는지 체크합니다.
    public bool spawnEnd;

    // 다음 스폰 딜레이 입니다.
    public float nextSpawnDelay;
    // 스폰 시간입니다.
    public float SpawnDelay;

    #endregion

    // G_M GM입니다!
    public GameManager G_M;

    // 이봐 스코어 어째서 제대로 돈을 표기하지않는것이지?
    public Text Score;

    // 스코어를 점진적으로 올려주는 노랗고 작은 글씨입니다.
    public Text SubScore;

    // 플레이어를 조종해 줍니다.
    public GameObject Player;

    // 적들이 등장하는곳 입니다.
    public Transform[] EnemyPortal;

    #region 퍼블릭 프리펩들
    // 플레이어의 총알 프리펩들
    public GameObject BulletAPrefab;
    public GameObject BulletBPrefab;


    // 에너미의 프리펩들
    public GameObject EnemyAPrefab;

    public GameObject EnemyM_BPrefab;

    // 배틀씬에서 쓸 돈 프리펩들
    public GameObject MoneyPrefab;

    // 폭팔이펙트의 프리펩입니다.
    public GameObject EffsPrefab;

    #endregion

    #region 생산할 오브젝트들의 배열들

    // 플레이어 총알들의 배열들
    GameObject[] BulletA;// 기본총알
    GameObject[] BulletB;


    // 에너미의 배열들
    GameObject[] EnemyA;// 기본 적

    GameObject[] EnemyM_B;// 중간보스

    // 돈의 배열들
    GameObject[] Moneys;

    // 폭팔이펙트 배열
    GameObject[] Effs;

    // MakeObj를 실행시킬때 for을 돌릴 표적이 됩니다.
    GameObject[] targetObj;

    #endregion

    // 스코어가 한번에 오르는게 아니라 점진적으로 오르게 하고싶습니다.
    int SubMoney;

    // 스코어가 점진적으로 증가하는 상태인지 체크합니다.
    bool SubCheck = false;

    #region 기본 함수//----------------------------------------------------------------

    // 등장시에 처리해줄것들을 여기 담아줍니다.
    private void Awake()
    {
        // GM등장! 쿠구구궁!
        G_M = GameObject.FindWithTag("G_M").GetComponent<GameManager>();

        // 설정한 레벨디자인을 담을 준비를 해줍시다.
        levelsSave = new List<Level>();

        // 플레이어 기본총알을 담을 배열을 생성해줍니다.
        BulletA = new GameObject[200];

        // 에너미를 담을 배열을 생성해줍니다.
        EnemyA = new GameObject[30];

        EnemyM_B = new GameObject[30];

        // 돈들을 담을 배열을 생성해줍니다.
        Moneys = new GameObject[20];

        // 할퀴기를 만들어 줍시다.
        Effs = new GameObject[80];

        // 프리펩의 오브젝트를 미리 생성해 줍니다.
        Generate();

        // 텍스트 파일로부터 배열을 채워줍시다.
        // 이 함수는 텍스트 폴더에서 단물을 쪽쪽 빨아서 데이터를 줄겁니다.
        ReadLevel();
    }

    // Update is called once per frame
    private void Update()
    {
        // 점수를 관리하는 함수입니다.
        MoneyManager();

        // 사격의 발사와 같습니다. 
        EnemyAwake();

        // 사격의 재장전과 같은 친구입니다. 대신 에너미 딜레이를장전하죠
        EnemyAwakeReLoad();
    }

    // 오브젝트가 비활성화될 때 발생합니다.
    private void OnDisable()
    {
        // 미처 다 못보낸 돈을 보내줘야죠
        G_M.GetMoneyGM(SubMoney);

        // 송금을 했으니 빈털털이가 됩니다. ㅠㅠ
        SubMoney = 0;
    }

    #endregion//----------------------------------------------------------------


    #region 커스텀 함수//----------------------------------------------------------------

    #region public

    // 비활성화된 오브젝트중에서 꺼내오는 역할을 합니다.
    public GameObject MakeObj(string type)
    {
        bool Type_Enemy = false;
       

        // 만들고자 하는 오브젝트를 표적Obj로 바꿔줘서 for문을 돌릴 준비를 합니다.
        switch (type)
        {
            case "BulletA":
                targetObj = BulletA;
                break;
            case "Effs":
                targetObj = Effs;
                break;
            case "EnemyA":
                targetObj = EnemyA;
                Type_Enemy = true;
                break;
            case "EnemyM_B":
                targetObj = EnemyM_B;
                Type_Enemy = true;
                break;
            case "Moneys":
                targetObj = Moneys;
                break;
            default:
                targetObj = null;
                break;
        }

        // 표적으로 포문을 돌려서 비활성화된 목록중에 가져올게 있나 봅시다 
        for (int index = 0; index < targetObj.Length; index++)
        {
            // 대상이 비활성화되있다면 실행합니다.
            if (!targetObj[index].activeSelf)
            {
                // 대상 오브젝트를 활성화해 줍니다.
                targetObj[index].SetActive(true);

                // 만약에 에너미라면
                if (Type_Enemy == true)
                {

                    // 자식 오브젝트가 있을수도 있죠
                    // 자식이 있나 없나 하나하나 세보기위한 인덱스입니다.
                    int Cindex = 0;

                    // C인덱스보다 자식의 숫자가 많으면 실행합니다.
                    while (Cindex < targetObj[index].transform.childCount)
                    {
                        // 자식을 잡아서 체크하기위한 에너미매니저입니다.
                        EnemyManager childEnemy;

                        // 인덱스번쨰의 타겟오브잭트가 가진 C인덱스번쨰의 자식을 찾아줍시다.
                        childEnemy = targetObj[index].transform.GetChild(Cindex).GetComponent<EnemyManager>();

                        // 자식을 활성화해 주었습니다.
                        childEnemy.OnEnable();

                        // 다음 순번을 찾아봅시다.
                        Cindex++;
                    }

                }

                return targetObj[index];
            }
        }

        // 그게안되면 어쩔수없죠...
        return null;

    }

    // 이 친구는 금수박사입니다. 하하하!하하하!하하하!윾!
    public void GetMoney(int income)
    {
        // 수입을 내놔랏!
        SubMoney += income;
    }

    // 폭팔효과를 생성합니다.
    public void MakeEff(Vector3 pos, string type)
    {
        // 이펙트를 활성화 하면서 대상으로 잡습니다.
        GameObject explosion = MakeObj("Effs");
        // 대상으로 잡은 오브젝트의 이펙트 매니저도 대상으로 잡습니다.
        EffManager explosionLogic = explosion.GetComponent<EffManager>();

        // 폭팔이펙트의 위치를 지정해줍니다.
        explosion.transform.position = pos;

        explosionLogic.StartExplosion(type);
    }

    #endregion

    #region private

    // 텍스트 파일을 불러옵니다.
    private void ReadLevel()
    {
        // 텍스트로 불러올 자료가 들어갈곳을 초기화해줍니다.

        // 적이 소환되는 정보셋트를 지닌 구조체입니다.
        levelsSave.Clear();
        // 적이 소환되는 딜레이입니다.
        spawnIndex = 0;
        // 소환이 끝났는지 체크합니다.
        spawnEnd = false;

        // 자료를 불러옵시다.
        // 리소스 폴더에서 "Level0"라는 파일을 꺼내옵니다.
        TextAsset textAsset = Resources.Load("Level0") as TextAsset;
        //글자를 읽을 준비를 합니다.
        StringReader stringReader = new StringReader(textAsset.text);

        // 파일이 끝났는지를 체크합니다.
        while(stringReader != null)
        { 
            // 줄 단위로 읽도록 설정합니다.
            string line = stringReader.ReadLine();

            // 라인이 없다구요? 그럼 나가야지
            if(line == null)
            {
                break;
            }

            // 구조체를 이용하여 3가지 데이터를 받아야하므로 구조체 변수를 선언합니다.
            Level level = new Level();

            // 지정한 구분 문자로 문자열을 나누는 함수입니다.
            // 구조체에 딜레이를 넣습니다.
            level.delay = float.Parse(line.Split(',')[0]);
            // 구조체에 적의 타입을 넣습니다.
            level.type = line.Split(',')[1];
            // 구조체에 적의 위치를 넣습니다.
            level.point = int.Parse(line.Split(',')[2]);

            // 구조체 묶음에 이번에 읽어온 자료를 넣습니다.
            levelsSave.Add(level);

        }

        // 파일을 꺼내왔으니 다시 닫아주어야겟죠.
        stringReader.Close();

        // 첫번째 스폰 딜레이 적용
        nextSpawnDelay = levelsSave[0].delay;

    }

    // 에너미를 생성해주는 함수입니다.
    private void EnemyAwake()
    {


        // 리스트의 길이보다 인덱스가 크면 실행을 정지합니다.
        if(spawnIndex == levelsSave.Count)
        {
            return;
        }

        // 딜레이시간이 일정값을 넘지 않으면 실행하지 않습니다.
        if (SpawnDelay < nextSpawnDelay)
        {
            return;
        }

        // 대상으로 한 에너미매니저입니다.
        EnemyManager enemyManager;

        // 타입을 읽어보고 어떤 적이 필요한지 말해줍니다.
        // 그리고 우리는 에너미의 경우 스피드값만 주면 자기혼자 움직이게
        // 만들어놨습니다 ^^7
        switch (levelsSave[spawnIndex].type)
        {
            case "A":
                // 적을 생성하면서 생성한 친구를 대상으로 집습니다.
                enemyManager = MakeObj("EnemyA").GetComponent<EnemyManager>();
                // 집은 대상에게 위치를 부여합니다.
                enemyManager.transform.position = EnemyPortal[levelsSave[spawnIndex].point - 1].transform.position;
                // 집은 대상에게 이동속도를 부여합니다.
                enemyManager.Speed = 1;
                break;
            case "M_B":
                // 적을 생성하면서 생성한 친구를 대상으로 집습니다.
                enemyManager = MakeObj("EnemyM_B").GetComponent<EnemyManager>();
                // 집은 대상에게 위치를 부여합니다.
                enemyManager.transform.position = EnemyPortal[levelsSave[spawnIndex].point - 1].transform.position;
                // 집은 대상에게 이동속도를 부여합니다.
                enemyManager.Speed = 0.25f;
                break;
            default:
                break;
        }
        spawnIndex++;

        // 스폰 딜레이를 초기화합니다.
        SpawnDelay = 0;

    }

    // 유닛 재장전!
    private void EnemyAwakeReLoad()
    {
        // 재장전시간을 체크합니다.
        SpawnDelay += Time.deltaTime;
    }

    // 점수를 관리하는 함수입니다.
    private void MoneyManager()
    {

        // 점수를 점진적으로 전환시키는 함수입니다.
        if (SubCheck == false && SubMoney > 0)
        {
            SubCheck = true;
            Invoke("MoneySlowUp",1f);
        }

        if (SubCheck == true && SubMoney == 0)
        {
            SubCheck = false;
        }

        // 여긴 게임이야 까라면 까는곳이지 점수를 표기해랏!
        Score.text = string.Format("{0:n0}", G_M.Money);

        if (SubMoney == 0)
        {
            // 점수가 없다면 이용가치가 없다!
            SubScore.text = " ";
        }
        else
        {
            SubScore.text = string.Format("+ {0:n0}", SubMoney);
        }

    }

    // 점수를 점진적으로 증감시켜 줍시다.
    private void MoneySlowUp()
    {


        // 보조점수가 남아있다면 그걸 진짜 점수에 추가시킵니다.
        if (SubMoney > 0)
        {
            // 보조점수가 100점보다 크게 남아있다면 9점씩 본점수에 더해줍니다.
            if (SubMoney > 100)
            {
                G_M.Money += 9;
                SubMoney -= 9;
                Invoke("MoneySlowUp", 0.02f);
            }
            // 보조점수가 10점보다 크게 남아있다면 3점씩 본점수에 더해줍니다.
            else if (SubMoney > 10)
            {
                G_M.Money += 3;
                SubMoney -= 3;
                Invoke("MoneySlowUp", 0.02f);
            }
            // 아니라면 전부 더해줍니다.
            else
            {
                G_M.Money += SubMoney;
                SubMoney = 0;
            }
        }
    }

    // 휴식 씬으로 이동하는 함수입니다.
    private void MoveToRest()
    {
        // 휴식 화면으로 이동하기전에 결손처리가 되면 안되겠죠.

        // 남아있는 돈들을 찾아줍시다.
        GameObject[] MoneysGones = GameObject.FindGameObjectsWithTag("Money");

        // 남아있는 돈들의 단물을 빨아먹습니다. 쭈왑!
        for(int index =0; index < MoneysGones.Length; index++)
        {
            // 자기 차례가 된 돈을 집어서 단두대에 올립니다.
            MoneyManager moneyManager = MoneysGones[index].GetComponent<MoneyManager>();
            
            // 댕겅! 돈을 쪼옥 빨아버립니다.
            moneyManager.MoneysGone();
}

        // 돈들의 단물을 전부 빨아드려 통통해진 B_S_M의 배를 갈라봅시다.
        G_M.Money += SubMoney;
        SubMoney = 0;

        // 휴식 화면으로 이동시킵니다.
        SceneManager.LoadScene("RestScene");
    }

    // 프리펩의 오브젝트를 미리 생성해 줍니다.
    private void Generate()
    {

        // 총알을 생성해줍시다
        for (int index = 0; index < BulletA.Length; index++)
        {
            BulletA[index] = Instantiate(BulletAPrefab);
            BulletA[index].SetActive(false);
        }

        // 에너미A를 생성해줍시다.
        for (int index = 0; index < EnemyA.Length; index++)
        {
            EnemyA[index] = Instantiate(EnemyAPrefab);

            EnemyA[index].SetActive(false);
        }

        // 에너미M_B를 생성해줍시다.
        for (int index = 0; index < EnemyM_B.Length; index++)
        {
            EnemyM_B[index] = Instantiate(EnemyM_BPrefab);

            EnemyM_B[index].SetActive(false);
        }

        // 머니를 생성해줍시다.
        for (int index = 0; index < Moneys.Length; index++)
        {
            Moneys[index] = Instantiate(MoneyPrefab);

            Moneys[index].SetActive(false);
        }

        // 할퀴기를 만들어 줍시다.
        for (int index = 0; index < Effs.Length; index++)
        {
            Effs[index] = Instantiate(EffsPrefab);

            Effs[index].SetActive(false);
        }
    }

    #endregion

    #endregion//----------------------------------------------------------------

}
