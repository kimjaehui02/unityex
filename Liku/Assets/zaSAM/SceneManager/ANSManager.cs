using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class ANSManager : MonoBehaviour
{
    /// <summary>
    /// 이펙트들을 담아두는 리스트입니다
    /// </summary>
    [SerializeField]
    private List<GameObject> Effs;

    /// <summary>
    /// 이펙트의 프리펩입니다
    /// </summary>
    [SerializeField]
    private GameObject EffsPrefab;

    /// <summary>
    /// 수치를 나타낼 텍스트를 소환합니다
    /// </summary>
    public List<GameObject> Texttetx;

    /// <summary>
    /// 텍스트의 프리펩입니다
    /// </summary>
    public GameObject TextPrefab;

    /// <summary>
    /// 배틀씬 매니저 입니다
    /// </summary>
    [SerializeField]
    private BattleSceneManager BattleSceneManager;

    private void Awake()
    {
        // 6개의 이팩트를 만들어줍니다
        for (int i = 0; i < 6; i++)
        {
            // 이팩트를 생성하여 리스트에 더해줍니다
            Effs.Add(Instantiate(EffsPrefab));
            // 비활성화 시킵니다
            Effs[Effs.Count - 1].SetActive(false);
        }

        // 6개의 텍스트를 만들어 줍니다
        for (int i = 0; i < 6; i++)
        {
            // 이팩트를 생성하여 리스트에 더해줍니다
            Texttetx.Add(Instantiate(TextPrefab));

            // 비활성화 시킵니다
            Texttetx[Texttetx.Count - 1].SetActive(false);
        }
    }

    /// <summary>
    /// 여러가지 이펙트들을 불러옵니다
    /// </summary>
    /// <param name="target">이펙트를 부를 위치</param>
    /// <param name="index">이펙트의 타입</param>
    /// <param name="index2">이펙트의 크기</param>
    public void EffTypes(Transform target, int index = 0, float index2 = 1f)
    {
        // 이펙트를 꺼내옵니다
        for (int i = 0; i < Effs.Count; i++)
        {
            // 근데 이번 이펙트가 활성화되있다면 바로 다음이펙트를 찾아갑니다
            if (Effs[i].activeSelf == true)
            {
                continue;
            }
            // 이펙트를 활성화합니다
            Effs[i].SetActive(true);
            // 이펙트의 좌표를 지정합니다
            Effs[i].transform.position = target.position;
            // 이펙트의 크기를 지정합니다
            Effs[i].transform.localScale *= index2;
            // 제대로 찾았다면 끝냅니다
            break;
        }
    }


    /// <summary>
    /// 텍스트를 표시합니다
    /// </summary>
    /// <param name="target">텍스트를 부를 위치</param>
    /// <param name="index">텍스트의 색상 true이면 빨강색 false이면 초록색</param>
    /// <param name="Damage">출력할 수치</param>
    public void Texting(Transform target, float Damage,  bool index = true)
    {
        // 이펙트를 꺼내옵니다
        for (int i = 0; i < Texttetx.Count; i++)
        {
            // 근데 이번 이펙트가 활성화되있다면 바로 다음이펙트를 찾아갑니다
            if (Texttetx[i].activeSelf == true)
            {
                continue;
            }
            // 이펙트를 활성화합니다
            Texttetx[i].SetActive(true);
            // 이펙트의 좌표를 지정합니다
            Texttetx[i].transform.position = target.transform.position;
            // 이펙트의 수치를 지정합니다
            // 텍스트를 아래로 던집니다
            Texttetx[i].GetComponent<DamageText>().Startingtext(Damage);
            // 제대로 찾았다면 끝냅니다
            break;
        }

    }

    #region 특수 공격입니다
    public void AllAttack(int mynumb, GameObject target)
    {
        for(int i =0; i < BattleSceneManager.Monsters.EnemyList.Count; i++)
        {
            ToDamage(BattleSceneManager.Monsters.EnemyList[i], 5);
        }
    }

    #endregion

    #region 아군의 공격들입니다

    /// <summary>
    /// 데미지를 입힐 때 사용합니다 (대상, 피해량)
    /// </summary>
    /// <param name="target">대상입니다</param>
    public void ToDamage(GameObject target, float Damage)
    {
        //Debug.Log(target.GetComponent<EnemyManager>().GetHp() + "-" + Damage + "=" + (target.GetComponent<EnemyManager>().GetHp() - Damage));
        // 타겟의 체력을 깎습니다
        target.GetComponent<EnemyManager>().SetHp(target.GetComponent<EnemyManager>().GetHp() - Damage);
        // 타겟을 흔듭니다
        target.transform.DOShakePosition(0.1f, 0.5f, 20, 360);
        
        // 텍스트를 불러옵니다
        Texting(target.transform, Damage);

        // 만약 체력이 0 이하라면
        if (target.GetComponent<EnemyManager>().GetHp() <= 0)
        {
            // 죽습니다
            target.GetComponent<EnemyManager>().Death();
        }

    }


    /// <summary>
    /// 기본 공격입니다 (공격하는 퐁, 방어하는 에너미)
    /// </summary>
    /// <param name="mynumb">공격자의 파티 순서입니다</param>
    /// <param name="targetenemy">타겟으로 지정한 적입니다</param>
    public void Attack1(int mynumb, GameObject target)
    {
        ToDamage(target, GameManager.G_M.GetPongs(mynumb).PongsData.GetAttack());

        EffTypes(target.transform);

        // 47번 소리를 불러옵니다
        GetComponent<AudioSource>().volume = GameManager.G_M.MainSound;
        GetComponent<AudioSource>().Play();

    }

    /// <summary>
    /// 강한 공격입니다 1.5배의 피해를 입힙니다 (공격하는 퐁, 방어하는 에너미)
    /// </summary>
    /// <param name="mynumb">공격자의 파티 순서입니다</param>
    /// <param name="targetenemy">타겟으로 지정한 적입니다</param>
    public void Attack2(int mynumb, GameObject target)
    {
        ToDamage(target, GameManager.G_M.GetPongs(mynumb).PongsData.GetAttack() * 1.5f);

        EffTypes(target.transform, 0, 1.2f);

        // 47번 소리를 불러옵니다
        GetComponent<AudioSource>().volume = GameManager.G_M.MainSound;
        GetComponent<AudioSource>().Play();
    }

    #endregion

    #region 적들의 공격들입니다.

    /// <summary>
    /// 적이 데미지를 입힐 때 사용합니다 (대상, 피해량)
    /// </summary>
    /// <param name="target">대상입니다</param>
    public void EToDamage(Pongs target, float Damage, int Pongnumb)
    {
        //Debug.Log(target.PongsData.GetHp() + "-" + Damage + "=" + (target.PongsData.GetHp() - Damage));
        // 타겟의 체력을 깎습니다
        target.PongsData.SetHp(target.PongsData.GetHp() - Damage);
        // 타겟에게서 데미지 수치를 보여줍니다
        Texting(BattleSceneManager.GetComponent<BattleSceneManager>().Party[Pongnumb].transform, Damage);
        // 타겟을 흔듭니다
        BattleSceneManager.GetComponent<BattleSceneManager>().Party[Pongnumb].transform.DOShakePosition(0.1f, 0.5f, 20, 360);
    }


    /// <summary>
    /// 적이 기본공격을 합니다 (공격하는 에너미, 방어하는 퐁)
    /// </summary>
    /// <param name="EAttacker">에너미입니다</param>
    /// <param name="Pongnumb">퐁의 순번입니다</param>
    public void EAttack1(GameObject EAttacker, int Pongnumb)
    {
        EToDamage(GameManager.G_M.GetPongs(Pongnumb), EAttacker.GetComponent<EnemyManager>().GetAttack(),
            Pongnumb);
        // 47번 소리를 불러옵니다
        // 47번 소리를 불러옵니다
        GetComponent<AudioSource>().volume = GameManager.G_M.MainSound;
        GetComponent<AudioSource>().Play();

        EffTypes(BattleSceneManager.Party[Pongnumb].transform);
    }

    #endregion


}
