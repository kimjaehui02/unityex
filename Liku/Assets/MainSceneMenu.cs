using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 메인씬의 메뉴입니다
/// </summary>
public class MainSceneMenu : MonoBehaviour
{
    /// <summary>
    /// 블레스의 관리자입니다
    /// </summary>
    public BlessBOx BlessBOx;

    /// <summary>
    /// 메인 유아이입니다
    /// </summary>
    public StoryManager StoryManager;

    /// <summary>
    /// 체력바들을 담을 배열들입니다
    /// </summary>
    private GameObject[] HpgameObjects;

    /// <summary>
    /// 다른 버튼들 입니다
    /// </summary>
    GameObject[] changebutton;

    /// <summary>
    /// 메인씬의 관리자입니다
    /// </summary>
    public MainSceneManager MainSceneManager;

    /// <summary>
    /// 텍스트 프리펩입니다
    /// </summary>
    public GameObject TextPrefab;

    /// <summary>
    /// 트윈들입니다
    /// </summary>
    public List<Tween> GetTweens;

    private void Awake()
    {
        GetTweens = new List<Tween>();
    }

    private void Start()
    {
        // 등장시 메인유아이에접근합니다
        StoryManager = GameObject.FindGameObjectWithTag("MapImage").GetComponent<StoryManager>();

        StoryManager.MoveAble = false;

        // 블레스 상자에도 접근합니다
        BlessBOx = GameObject.Find("BlessPoint").GetComponent<BlessBOx>();

        StoryManager.CanvasControll(true);

    }

    private void OnDestroy()
    {
        // 트윈을 전부 제거해서 에러가 없게 합니다
        for (int i = 0; i < GetTweens.Count; i++)
        {
            GetTweens[i].Kill();
        }
    }

    /// <summary>
    /// 휴식을 실시합니다
    /// </summary>
    public void RestButton()
    {
        // 회복할 체력 수치입니다
        float HPPlus = 100;

        // 아군의 체력을 회복합니다
        for (int i = 0; i < GameManager.G_M.PongsParty.Count; i++)
        {
            // 체력을 더해줍니다
            GameManager.G_M.PongsParty[i].PongsData
                .SetHp(GameManager.G_M.PongsParty[i].PongsData.GetHp()+HPPlus);
            // 체력이 최대체력보다 많다면 다시 줄여줍니다
            if(GameManager.G_M.PongsParty[i].PongsData.GetHp() > GameManager.G_M.PongsParty[i].PongsData.GetMaxHp())
            {
                GameManager.G_M.PongsParty[i].PongsData.SetHp(GameManager.G_M.PongsParty[i].PongsData.GetMaxHp());
            }

            Vector3 vector32 = (MainSceneManager.MainPong[i].transform.position);
            // 텍스트를 생성합니다 동시에 텍스트의 위치도 조정합니다
            GameObject gameObject = Instantiate(TextPrefab, vector32, TextPrefab.transform.rotation);
            
            // 택스트를 조정합니다
            gameObject.GetComponent<DamageText>().Startingtext(HPPlus, true);
        }



        ButtonEnd();    
    }

    /// <summary>
    /// 기도를 합니다
    /// </summary>
    public void BlessButton()
    {
        // 무작위 장신구를 1개 얻습니다
        BlessBOx.RandBless();


        ButtonEnd();
    }

    /// <summary>
    /// 장비를 강화합니다
    /// </summary>
    public void ReinforceButton()
    {
        BlessBOx = GameObject.Find("BlessPoint").GetComponent<BlessBOx>();

        for (int i = 0; i < GameManager.G_M.Equips1.Length; i++)
        {
            BlessBOx.ReinforceEqu(i,0);
        }

        for (int i = 0; i < GameManager.G_M.Equips2.Length; i++)
        {
            BlessBOx.ReinforceEqu(i, 1);

        }

        ButtonEnd();
    }


    /// <summary>
    /// 메인메뉴의 버튼이 끝나면 처리해줄것들입니다
    /// </summary>
    private void ButtonEnd()
    {
        // 선택이 끝났으니 이제 이동할수 있다고 말합니다
        StoryManager.MoveAble = true;

        // 선택이 끝났으니 사라집니다
        gameObject.SetActive(false);

        StoryManager.CanvasControll(true);

    }
}
