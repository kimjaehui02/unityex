using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyManager : MonoBehaviour
{

    #region 에너미 정보들

    /// <summary>
    /// 최대 체력입니다.
    /// </summary>
    [SerializeField]
    private float MaxHp;

    /// <summary>
    /// 체력입니다.
    /// </summary>
    [SerializeField]
    private float Hp;

    #region Hp프로퍼티

    public float GetHp()
    {
        return Hp;
    }
    public float GetMaxHp()
    {
        return MaxHp;
    }

    public void SetHp(float index)
    {
        Hp = index;
    }


    #endregion

    /// <summary>
    /// 공격력입니다.
    /// </summary>
    [SerializeField]
    private float Attack;

    #region Attack프로퍼티

    public float GetAttack()
    {
        return Attack;
    }

    public void SetAttack(float index)
    {
        Attack = index;
    }

    #endregion

    /// <summary>
    /// 보상금입니다.
    /// </summary>
    [SerializeField]
    private int Reward;

    #region Reward프로퍼티

    public int GetReward()
    {
        return Reward;
    }

    public void SetReward(int index)
    {
        Attack = index;
    }

    #endregion

    #endregion

    #region 적공격들

    /// <summary>
    /// 적의 공격을 담기위한 델리게이트입니다
    /// </summary>
    /// <param name="EAttacker">공격하는 에너미입니다</param>
    /// <param name="Pongnumb">방어하는 퐁입니다</param>
    public delegate void EnemyAttack(GameObject EAttacker, int Pongnumb);

    /// <summary>
    /// 공격들입니다
    /// </summary>
    public List<EnemyAttack> AttackN;

    #endregion

    /// <summary>
    /// 배틀씬매니저입니다
    /// </summary>
    public BattleSceneManager BattleSceneManager;

    /// <summary>
    /// 적의 적리스트의 순번입니다
    /// </summary>
    public int EnemyNUmber;

    /// <summary>
    /// 트윈들입니다
    /// </summary>
    public List<Tween> GetTweens;

    /// <summary>
    /// 공격포인트입니다
    /// </summary>
    public int AttackPoint;

    #region 기본 함수

    private void Awake()
    {
        GetTweens = new List<Tween>();

        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            // 1. 비활성화된 포인트들을 활성화
            transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
            // 2. 포인트들을 안보이게 만듬
            GetTweens.Add(
                // 해당 오브젝트를 투명하게함
                transform.GetChild(0).GetChild(i).gameObject.GetComponent<SpriteRenderer>().DOFade(0, 0)
                );

            // 3. 포인트들을 내려간 위치로 보냄
            GetTweens.Add(
                // 해당 오브젝트를 아래로 내림
                transform.GetChild(0).GetChild(i).DOLocalMoveY(-1f, 0)
                .SetRelative()
                );
        }
    }

    private void Update()
    {



        // 만약 체력이 0 이하라면
        if (GetHp() <= 0)
        {
            // 죽습니다
            Death();
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < GetTweens.Count; i++)
        {
            // 잔존 트윈들을 제거합니다
            GetTweens[i].Kill();
        }
    }

    #endregion


    /// <summary>
    /// 죽을때 발동합니다.
    /// </summary>
    public void Death()
    {
        // 죽엇으니 돈을 줍니다
        GameManager.G_M.SetMoney(GameManager.G_M.GetMoney() + GetReward());
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 적이 행동게이지를 충전합니다
    /// </summary>
    public void Recharge()
    {
        for(int i =0; i < transform.GetChild(0).childCount; i++)
        {
            GetTweens.Add(
    // 해당 오브젝트를 위로 올림
    transform.GetChild(0).GetChild(i).DOLocalMoveY(1f, 0.5f)
    .SetRelative()
    );

            // 트윈들을 관리해주기 위해 리스트에 더함
            GetTweens.Add(
                // 해당 오브젝트를 보이게함
                transform.GetChild(0).GetChild(i).gameObject.GetComponent<SpriteRenderer>().DOFade(1, 0.5f)
                );
        }

        // 포인트를 충전합니다
        AttackPoint = transform.GetChild(0).childCount - 1;
    }

    /// <summary>
    /// 행동게이지가 줄어듭니다
    /// </summary>
    public void PowerDown()
    {
        AttackPoint--;

        float times = 0.5f;

        GetTweens.Add(
            // 해당 오브젝트를 아래로 내림
            transform.GetChild(0).GetChild(AttackPoint+1).DOLocalMoveY(-1f, times)
            .SetRelative()
            );

        // 트윈들을 관리해주기 위해 리스트에 더함
        GetTweens.Add(
            // 해당 오브젝트를 투명하게함
            transform.GetChild(0).GetChild(AttackPoint+1).gameObject.GetComponent<SpriteRenderer>().DOFade(0, times)
            );
    }

    /// <summary>
    /// 포인트를 제거합니다
    /// </summary>
    public void ReMovePoints()
    {

        float times = 0.5f;

        for (int i = 0; i < AttackPoint+1; i++)
        {
            GetTweens.Add(
                // 해당 오브젝트를 아래로 내림
                transform.GetChild(0).GetChild(i).DOLocalMoveY(-1f, times)
                .SetRelative()
                );

            // 트윈들을 관리해주기 위해 리스트에 더함
            GetTweens.Add(
                // 해당 오브젝트를 투명하게함
                transform.GetChild(0).GetChild(i).gameObject.GetComponent<SpriteRenderer>().DOFade(0, times)
                );
        }

        AttackPoint = 0;
    }

    /// <summary>
    /// 적의 단일 공격입니다
    /// </summary>
    /// <param name="index"></param>
    public void EAttack(int index)
    {
        // 어택포인트에따라서 공격이달라집니다
        // 어택포인트 0초과때만 공격을 합니다
        if (AttackPoint > 0)
        {
            // 어택포인트가 최대일경우 무작위 대상을 공격합니다
            if(Attack == transform.GetChild(0).childCount - 1)
            {
                int randa = Random.Range(0,GameManager.G_M.PartyCount());
                // 만약 대상의 체력이 없다면 재시작합니다
                if(GameManager.G_M.GetPongs(randa).PongsData.GetHp() <= 0)
                {

                }

                AttackN[0](gameObject, Random.Range(0, 1));
            }
            else
            {
                AttackN[0](gameObject, index);

            }
        }

        // 공격을 햇으니 공격포인트도소비됩니다
        ReMovePoints();
    }

}
