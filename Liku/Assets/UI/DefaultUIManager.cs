using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class DefaultUIManager : MonoBehaviour
{

    /// <summary>
    /// 돈수치를 관리합니다.
    /// </summary>
    [SerializeField]
    private Text MoneyText;

    /// <summary>
    /// 이미지들입니다 색을 연하게 해주려고합니다
    /// </summary>
    [SerializeField]
    private List<Image> GetImages;

    /// <summary>
    /// 작은 장비창들입니다
    /// </summary>
    public List<PongUI> pongUIs;

    /// <summary>
    /// 글씨들입니다 색을 연하게 해주려고합니다
    /// </summary>
    [SerializeField]
    private List<Text> GetTexts;

    /// <summary>
    /// 트윈들입니다
    /// </summary>
    public List<Tween> GetTweens;


    #region 기본함수


    private void Awake()
    {
        // 초기화를 해줍니다
        GetTweens = new List<Tween>();
        GetTweens.Clear();

        // 장비창 ui의 이미지들도 더해주어서 투명화가 되도록합니다
        for(int i =0; i< pongUIs.Count; i++)
        {
            GetImages.AddRange(pongUIs[i].GetImages);
        }
    }

    private void Start()
    {
        Bullr();
    }

    private void Update()
    {
        // 돈을 지속적으로 최신화 합니다
        MoneyText.text = GameManager.G_M.GetMoney().ToString();
    }

    private void OnDestroy()
    {
        // 트윈을 전부 제거해서 에러가 없게 합니다
        for (int i = 0; i < GetTweens.Count; i++)
        {
            GetTweens[i].Kill();
        }


    }

    private void OnMouseEnter()
    {
        BullrRE();
    }

    private void OnMouseExit()
    {
        Bullr();
    }

    #endregion

    /// <summary>
    /// 시간차로 색이 어두워집니다
    /// </summary>
    public void INBUll()
    {
        Invoke("Bullr", 2f);
    }

    /// <summary>
    /// 기다리면 색이 연해집니다
    /// </summary>
    public void Bullr()
    {
        // 그러나 장비창중에 하나라도 켜져있다면 작동하지 않습니다

        /*

        for(int i =0; i < pongUIs.Count; i++)
        {
            if(pongUIs[i].toglebool == true)
            {
                Invoke("Bullr", 2f);
                return;
            }
        }

        // 트윈을 전부 제거해서 에러가 없게 합니다
        for (int i = 0; i < GetTweens.Count; i++)
        {
            GetTweens[i].Kill();
        }

        // 색에 접근합니다
        for (int i = 0; i < GetImages.Count; i++)
        {
            GetTweens.Add(
            GetImages[i].DOFade(0.2f, 1)
            //.SetDelay(0.2f)


                ); 
            


        }

        for (int i = 0; i < GetTexts.Count; i++)
        {
            GetTweens.Add(
            GetTexts[i].DOFade(0.3f, 1)
            //.SetDelay(0.2f)


        );



        }
        */
        

    }

    /// <summary>
    /// 색이 다시 진해집니다
    /// </summary>
    public void BullrRE()
    {
        /*
        // 트윈을 전부 제거해서 에러가 없게 합니다
        for (int i = 0; i < GetTweens.Count; i++)
        {
            GetTweens[i].Kill();
        }

        // 색에 접근합니다
        for (int i = 0; i < GetImages.Count; i++)
        {
            GetTweens.Add(
            GetImages[i].DOFade(1, 0.5f)

                );
            


        }


        for (int i = 0; i < GetTexts.Count; i++)
        {
            GetTweens.Add(
            GetTexts[i].DOFade(1, 0.5f)

                );



        }


        //Invoke("Bullr", 2f);
        */
    }

}
