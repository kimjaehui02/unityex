using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PointManager : MonoBehaviour
{

    #region 확장트윈
    /// <summary>
    /// 자신의 기본크기 
    /// </summary>
    public Vector3 GetTransform;

    /// <summary>
    /// 자신의 확장정도 
    /// </summary>
    public float valuet;

    /// <summary>
    /// 확상시 사용하는 알고리즘 
    /// </summary>
    public Ease ease2;

    /// <summary>
    /// 확장스피드
    /// </summary>
    public float Speeds;

    /// <summary>
    /// 확장트윈
    /// </summary>
    public Tween GetTween;

    /// <summary>
    /// 빛내는 효과를 가진 트윈입니다
    /// </summary>
    public Tween LiTween;

    /// <summary>
    /// 빛이 나는지의 여부입니다
    /// </summary>
    public bool LiBool;

    /// <summary>
    /// 트윈들입니다
    /// </summary>
    public List<Tween> GetTweens;
    #endregion

    /// <summary>
    /// 보일 모습의 종류입니다
    /// </summary>
    public List<Sprite> GetSprites;

    /// <summary>
    /// 모습을 지정할 번호입니다 Random, Enemy, Rest, coin, boss
    /// </summary>
    public int IncSprite;

    /// <summary>
    /// UI매니저입니다
    /// </summary>
    public GameObject MapManager;

    /// <summary>
    /// 맵의 접근상태입니다 0 = 접근가능 1 = 이전맵이라서 접근불가능 2 = 이후맵이라서 나중에 접근가능
    /// 3 현재 있는 맵
    /// </summary>
    public int ACCMap;

    /// <summary>
    /// 내가 몇열인가
    /// </summary>
    public int Myduf;

    #region 기본함수

    private void Awake()
    {
        // 등장시 맵매니저에 접근합니다
        MapManager = GameObject.FindGameObjectWithTag("MapImage");

        GetTweens = new List<Tween>();
    }




    private void Update()
    {
        // 자신의 이미지를 번호에 따라서 변화합니다
        gameObject.GetComponent<Image>().sprite = GetSprites[IncSprite];
    }

    private void OnMouseOver()
    {
        // 맵 매니저의 호버링항목을 트루로 만들어 설명서가 표기되도록합니다
        MapManager.GetComponent<StoryManager>().Hovering = true;
        // 자신이 무슨항목인지 알려줍니다
        MapManager.GetComponent<StoryManager>().TextNumb = IncSprite;

    }

    /// <summary>
    /// 온 마우스다운은 버튼컴포넌트보다 먼저작동합니다 다음 맵에 갈수 있도록 합니다
    /// </summary>
    private void OnMouseDown()
    {
        // 접근가능일때만 작동합니다
        if (ACCMap == 0)
        {
            bool doen = false;
            // 이미 자신의 불이 켜져잇다면
            if(LiBool == true)
            {
                doen = true;
            }

            // 내가하고싶은것 - 내가 몇열인지 아는것
            for (int i = 0; i < MapManager.transform.GetChild(Myduf).childCount; i++)
            {
                if(MapManager.transform.GetChild(Myduf).GetChild(i).GetComponent<PointManager>().ACCMap == 0)
                {
                    MapManager.transform.GetChild(Myduf).GetChild(i).GetComponent<PointManager>().LightingOff();
                }
                
            }

            if (doen == false)
            {
                Lighting();
            }
        }
    }

    #endregion

    /// <summary>
    /// 맵이 커지거나 작아지는걸 시작합니다
    /// </summary>
    public void wigstart()
    {

        Color sdd = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255/255f);
        gameObject.GetComponent<Image>().color = sdd;

        switch (ACCMap)
        {
            case 0:
                wiggling();
                break;

            case 1:
                STOPing();
                break;

            case 2:
                STOPing2();
                break;
            case 3:
                HEER();
                break;
        }

    }

    #region 버튼확대

    /// <summary>
    /// 작아졋다커졋다합니다
    /// </summary>
    public void wiggling()
    {
        // 확장트윈의 정지
        GetTween.Kill();
        // 위글링을시작합니다
        GetTween = transform.DOScale(transform.localScale * valuet, Speeds)
            .SetLoops(-1, LoopType.Yoyo) // 무한루프, 실행 및 되감기 반복
            .SetEase(ease2); // 커지고 작아지는 알고리즘
    }


    /// <summary>
    /// 원래크기로 돌아오고 멈춥니다
    /// </summary>
    public void STOPing()
    {
        // 확장트윈의 정지
        GetTween.Kill();

        // 원래크기로 돌아오기
        GetTweens.Add(
        gameObject.GetComponent<RectTransform>().DOScale(GetTransform, Speeds)
            .SetEase(ease2)
            
            );
        // 커지고 작아지는 알고리즘

        Color sdd = new Color(122 / 255f, 122 / 255f, 122 / 255f);
        gameObject.GetComponent<Image>().color = sdd;
    }

    /// <summary>
    /// 나중에 갈수는 있는곳입니다
    /// </summary>
    public void STOPing2()
    {
        // 확장트윈의 정지
        GetTween.Kill();
        // 원래크기로 돌아오기
        GetTweens.Add(
        gameObject.GetComponent<RectTransform>().DOScale(GetTransform, Speeds)
            .SetEase(ease2)

            );
        // 커지고 작아지는 알고리즘
        Color sdd = new Color(200 / 255f, 200 / 255f, 200 / 255f);
        gameObject.GetComponent<Image>().color = sdd;
    }


    /// <summary>
    /// 빛나게 합니다
    /// </summary>
    public void Lighting()
    {
        // 불이 이미 켜져있다면
        if(LiBool == true)
        {
            LiBool = false;
            // 순환을 정지합니다
            LiTween.Kill();
            // 그리고 원상복구할 색입니다
            Color sdd = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
            // 원상복구시킵니다
            GetTweens.Add(
    gameObject.GetComponent<Image>().DOColor(sdd, 0.6f)
    );

        }
        else
        {
            LiBool = true;
            // 변할 색상을 지정합니다
            Color sdd = new Color(255 / 255f, 255 / 255f, 255 / 255f, 155/255f);
            // 해당 색상으로 변환시켜 루프합니다
            LiTween = gameObject.GetComponent<Image>().DOColor(sdd, 0.6f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(ease2);
        }
    }

    /// <summary>
    /// 무조건 불이 꺼집니다
    /// </summary>
    public void LightingOff()
    {
        LiBool = false;
        // 순환을 정지합니다
        LiTween.Kill();
        // 그리고 원상복구할 색입니다
        Color sdd = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        // 원상복구시킵니다
        GetTweens.Add(
gameObject.GetComponent<Image>().DOColor(sdd, 0.6f)
);
    }

    /// <summary>
    /// 현재 내가 존재하는 맵입니다
    /// </summary>
    public void HEER()
    {
        // 변할 색상을 지정합니다
        Color sdd = new Color(255 / 255f, 255 / 255f, 255 / 255f, 155 / 255f);
        // 해당 색상으로 변환시켜 루프합니다
        gameObject.GetComponent<Image>().color = sdd;
        
    }

    public void OnDestroy()
    {
        // 무한루프가 되어있는 트윈을 삭제시켜 오류를 방지합니다
        GetTween.Kill();
        LiTween.Kill();
        for (int i = 0; i < GetTweens.Count; i++)
        {
            GetTweens[i].Kill();
        }
    }
    #endregion

}
