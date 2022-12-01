using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 배틀씬에서 사용하는 퐁들의 매니저입니다
/// </summary>
public class PongManager : MonoBehaviour
{
    #region 공격들

    /// <summary>
    /// 퐁의 공격을담기위한 델리게이트입니다
    /// </summary>
    public delegate void PongAttack(int mynumb, GameObject target);

    /// <summary>
    /// 공격들입니다
    /// </summary>
    public List<PongAttack> AttackN;

    /// <summary>
    /// 공격포인트입니다
    /// </summary>
    public int AttackPoint;

    /// <summary>
    /// 포인트를 알려주는 바 입니다
    /// </summary>
    public List<GameObject> PointBars;

    #endregion

    /// <summary>
    /// 자신의 스텟입니다
    /// </summary>
    public GameObject selfstat;

    /// <summary>
    /// 퐁들의 서있는 이미지입니다
    /// </summary>
    public List<GameObject> GetGameObjects;

    /// <summary>
    /// 자신이 활성화된 이미지입니다
    /// </summary>
    public MySpirtes MySpirtes;

    /// <summary>
    /// 씬이 어딘지 알아야합니다
    /// </summary>
    public bool mainb;

    /// <summary>
    /// 트윈들입니다
    /// </summary>
    public List<Tween> GetTweens;

    /// <summary>
    /// 배틀씬 매니저입니다.
    /// </summary>
    public BattleSceneManager BattleSceneManager;

    /// <summary>
    /// 파티의 순번입니다.
    /// </summary>
    public int partynumber;

    

    /// <summary>
    /// 자신의 스테이터스들 입니다
    /// </summary>
    public List<TextMesh> selfStat;

    private void Awake()
    {
        GetTweens = new List<Tween>();
        MySpirtes = GetGameObjects[0].GetComponent<MySpirtes>();

        // 배틀씬에서만 작동합니다
        if (mainb == false)
        {
            for(int i =0; i < 3; i++)
            {
                // 1. 비활성화된 포인트들을 활성화
                transform.GetChild(i).gameObject.SetActive(true);

                GetTweens.Add(
    // 해당 오브젝트를 투명하게함
    transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().DOFade(0, 0)
    );

                // 3. 포인트들을 내려간 위치로 보냄
                GetTweens.Add(
    // 해당 오브젝트를 아래로 내림
    transform.GetChild(i).DOLocalMoveY(-1f, 0)
    .SetRelative()
    );
            }

            // 스탯창도 지워줍니다
            selfstat.SetActive(false);
        }
        

    }

    private void Update()
    {

        NewStat();
    }


    private void OnDestroy()
    {
        for(int i = 0; i < GetTweens.Count; i++)
        {
            // 잔존 트윈들을 제거합니다
            GetTweens[i].Kill();
        }
    }

    /// <summary>
    /// 자신의 스텟을 최신화합니다
    /// </summary>
    private void NewStat()
    {
        // 자신의 스테이터스를 최신화 합니다

        // 근데 메인씬이 아니라면 안해줍니다
        if(mainb == false)
        {
            return;
        }

        // 파티보다 큰 숫자는 실행하지 않습니다
        if (GameManager.G_M.PongsParty.Count <= partynumber)
        {
            return;
        }

        // 이름을 최신화합니다
        switch (GameManager.G_M.PongsParty[partynumber].PongsData.GetPongType())
        {
            case 0:
                selfStat[0].text = "깃발";
                break;
            case 1:
                selfStat[0].text = "방패";
                break;
            case 2:
                selfStat[0].text = "창";
                break;
            case 3:
                selfStat[0].text = "화살";
                break;
        }

        // 공격력을 최신화합니다
        selfStat[1].text = "공격:" + GameManager.G_M.PongsParty[partynumber].PongsData.GetAttack();


        // 체력을 최신화합니다
        selfStat[2].text = "체력:" + GameManager.G_M.PongsParty[partynumber].PongsData.GetMaxHp();

        // 장비 상태를 최신화합니다

        // 머리장비 등급
        int headint = 0;

        // 무기장비 등급
        int weapint = 0;

        // 아이템들로 증가하는 공격력입니다
        float PlusAttacks = 0;
        
        // 아이템들로 증가하는 체력입니다
        float PlusHps = 0;

        // 해당 장비가 있어야만 최신화가 됩니다
        if (GameManager.G_M.Equips1[partynumber] != null)
        {
            headint = GameManager.G_M.Equips1[partynumber].GetComponent<SimpleEquip>().Rare;

            PlusAttacks += GameManager.G_M.Equips1[partynumber].GetComponent<SimpleEquip>().PlusAttack;
            PlusHps += GameManager.G_M.Equips1[partynumber].GetComponent<SimpleEquip>().PlusHp;

        }

        // 해당 장비가 있어야만 최신화가 됩니다
        if (GameManager.G_M.Equips2[partynumber] != null)
        {
            weapint = GameManager.G_M.Equips2[partynumber].GetComponent<SimpleEquip>().Rare;

            PlusAttacks += GameManager.G_M.Equips2[partynumber].GetComponent<SimpleEquip>().PlusAttack;
            PlusHps += GameManager.G_M.Equips2[partynumber].GetComponent<SimpleEquip>().PlusHp;

        }

        // 그러면서 장비수치도 공격력에 추가합니다
        selfStat[1].text += "+" + PlusAttacks;
        // 그러면서 장비수치도 추가해줍니다
        selfStat[2].text += "+" + PlusHps;


        selfStat[3].text = "장비:" + headint + "/" + weapint;

    }

    /// <summary>
    /// 턴이끝났습니다 공격을 합니다 포인트에 따라 다른 공격을 실시합니다
    /// </summary>
    public void ATAAAAAACK(GameObject target)
    {
        // 어택포인트에따라서 공격이달라집니다
        // 어택포인트 0초과
        if(AttackPoint > 0)
        {
            AttackN[0](partynumber, target);
        }

        // 공격을 햇으니 공격포인트도소비됩니다
        ReMovePoints();
    }

    /// <summary>
    /// 성공했다면 포인트를 1개 추가합니다
    /// </summary>
    public void PlusAtack()
    {
        AttackPoint++;

        float times = 0.5f;

        // 트윈들을 관리해주기 위해 리스트에 더함
        GetTweens.Add(
            // 해당 오브젝트를 위로 올림
            transform.GetChild(AttackPoint-1).DOLocalMoveY(1f, times)
            .SetRelative()
            );

        // 트윈들을 관리해주기 위해 리스트에 더함
        GetTweens.Add(
            // 해당 오브젝트를 보이게함
            transform.GetChild(AttackPoint-1).gameObject.GetComponent<SpriteRenderer>().DOFade(1, times)
            );
    }

    /// <summary>
    /// 공격을 했다면 포인트가 초기화됩니다
    /// </summary>
    public void ReMovePoints()
    {


        float times = 0.5f;

        for(int i = 0; i < AttackPoint; i++)
        {
            GetTweens.Add(
                // 해당 오브젝트를 아래로 내림
                transform.GetChild(i).DOLocalMoveY(-1f, times)
                .SetRelative()
                );

            // 트윈들을 관리해주기 위해 리스트에 더함
            GetTweens.Add(
                // 해당 오브젝트를 투명하게함
                transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().DOFade(0, times)
                );
        }

        AttackPoint = 0;


    }

    /// <summary>
    /// 넣은 숫자의 서있는 이미지를 불러옵니다
    /// </summary>
    /// <param name="index"></param>
    public void ChageImage(int index)
    {
        for (int i = 0; i < GetGameObjects.Count; i++)
        {
            if(index == i)
            {
                GetGameObjects[i].SetActive(true);
            }
            else
            {
                GetGameObjects[i].SetActive(false);
            }

            
        }

        // 불러온 이미지만 관리 대상이 됩니다
        MySpirtes = GetGameObjects[index].GetComponent<MySpirtes>();
    }

}

