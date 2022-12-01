using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class PongUI : MonoBehaviour
{
    /// <summary>
    /// 기본유아이 매니저입니다
    /// </summary>
    [SerializeField]
    private DefaultUIManager DefaultUIManager;

    /// <summary>
    /// 트윈들을 관리하는 함수입니다
    /// </summary>
    private List<Tween> tweens;

    /// <summary>
    /// 장비를 보여주는 스크롤바입니다
    /// </summary>
    [SerializeField]
    private GameObject scrollObject;

    /// <summary>
    /// 펼처진지 아닌지 알려주는 불값입니다
    /// </summary>
    public bool toglebool;

    /// <summary>
    /// 자신과 자신의 아들의 이미지입니다
    /// </summary>
    public List<Image> GetImages;

    /// <summary>
    /// 키를 받는 숫자입니다
    /// </summary>
    public int inputint;

    private void Awake()
    {
        // 초기화를 합니다
        tweens = new List<Tween>();
        // 비워줍니다
        tweens.Clear();
    }

    private void Update()
    {
        INputkey();
        SpriteUPdate();
    }

    /// <summary>
    /// 씬이 교체되어 사라질때 처리합니다
    /// </summary>
    private void OnDestroy()
    {
        for (int i = 0; i < tweens.Count; i++)
        {
            tweens[i].Kill();
        }
    }

    /// <summary>
    /// 스프라이트를 지속적으로 업데이트 해줍니다
    /// </summary>
    public void SpriteUPdate()
    {
        // 길이가 맞지 않다면 실행하지 않습니다
        if ((GameManager.G_M.PongsParty.Count <= inputint - 1))
        {
            return;
        }

        if ((GameManager.G_M.PongsParty[inputint - 1] == null))
        {
            return;
        }

        int iput = GameManager.G_M.PongsParty[inputint-1].PongsData.GetPongType();

        switch (iput)
        {
            case (int)PongClass.Flag:
                GetImages[GetImages.Count - 1].sprite = GameManager.G_M.ClassSprites[(int)PongClass.Flag];

                break;
            case (int)PongClass.Shield:
                GetImages[GetImages.Count - 1].sprite = GameManager.G_M.ClassSprites[(int)PongClass.Shield];

                break;
            case (int)PongClass.Spear:
                GetImages[GetImages.Count - 1].sprite = GameManager.G_M.ClassSprites[(int)PongClass.Spear];

                break;
            case (int)PongClass.Bow:
                GetImages[GetImages.Count - 1].sprite = GameManager.G_M.ClassSprites[(int)PongClass.Bow];

                break;

        }



    }

    #region 장비창 피고 접기

    public void togle()
    {
        // 소리를 출력합니다
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.volume = GameManager.G_M.MainSound;
        audioSource.Play();

        // 펼쳐져있지 않다면 펼칩니다
        if (toglebool == false)
        {
            ScrollDown();
            // 펼치면서 색도 돌아오게만듭니다
            DefaultUIManager.BullrRE();
            toglebool = true;
        }
        else
        {
            ScrollUp();
            DefaultUIManager.INBUll();
            toglebool = false;
        }
    }
    

    public void ScrollDown()
    {
        tweens.Add
            (
            scrollObject.GetComponent<RectTransform>().DOAnchorPosY(-60, 0.5f)
            
            );
    }

    public void ScrollUp()
    {
        tweens.Add
    (
    scrollObject.GetComponent<RectTransform>().DOAnchorPosY(60, 0.5f)
    );
    }

    #endregion

    /// <summary>
    /// 키를 받는 함수입니다
    /// </summary>
    public void INputkey()
    {


        switch(inputint)
        {
            case 1:
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    togle();
                }
                break;
            case 2:
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    togle();
                }
                break;
            case 3:
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    togle();
                }
                break;
            case 4:
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    togle();
                }
                break;

        }


    }

}
