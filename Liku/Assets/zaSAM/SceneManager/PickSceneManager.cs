using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class PickSceneManager : MonoBehaviour
{
    /// <summary>
    /// 버튼들입니다.
    /// </summary>
    [SerializeField]
    private GameObject[] Buttons;

    /// <summary>
    /// 선택지3개의 그림들의 컴포넌트입니다.
    /// </summary>
    [SerializeField]
    private List<SpriteRenderer> Pick3Sprite;

    /// <summary>
    /// 파티에 넣어줄 2개의 선택지입니다.
    /// </summary>
    [SerializeField]
    private List<int> AddPick;

    #region 색상들

    /// <summary>
    /// 선택되있습니다.
    /// </summary>
    [SerializeField]
    private Color pick = new Color(255, 255, 255);

    /// <summary>
    /// 미택되있습니다.
    /// </summary>
    [SerializeField]
    private Color nopick = new Color(222, 222, 222);

    /// <summary>
    /// 이미 다 골라졌습니다.
    /// </summary>
    [SerializeField]
    private Color endpick = new Color(122, 122, 122);

    #endregion

    /// <summary>
    /// 처음에 주어지는 선택지3개를 담아둘 인트리스트입니다.
    /// </summary>
    [SerializeField]
    private List<int> Pick3;

    #region 기본 함수

    private void Update()
    {
        ButtonManage();
        PickManage();
        PickUImange();
    }

    private void Awake()
    {
        Pickin3();
    }
    #endregion

    #region 커스텀 함수

    #region private 함수

    /// <summary>
    /// 시작버튼이 불이 켜지는 등 불빛효과입니다.
    /// </summary>
    private void ButtonManage()
    { 
        // 2개 골라졌다면 시작 버튼에 불이들어옵니다.
        if (AddPick.Count == 2)
        {
            // 선택되지 못한 버튼의 불을 끕니다.
            for (int i = 0; i < Buttons.Length - 2; i++)
            {
                // 선택되지 못한 버튼을 찾습니다.
                if (Buttons[i].GetComponent<Image>().color == nopick)
                {
                    // 불을 꺼버립니다.
                    Buttons[i].GetComponent<Image>().color = endpick;
                }
            }
            // 시작 버튼에 불을 넣습니다.
            Buttons[Buttons.Length - 1].GetComponent<Image>().color = pick;
        }
        // 아니라면 불이 꺼집니다.
        else
        {
            // 선택되지 못한 버튼의 불을 다시 킵니다.
            for (int i = 0; i < Buttons.Length - 2; i++)
            {
                // 불꺼진 버튼을 찾습니다.
                if (Buttons[i].GetComponent<Image>().color == endpick)
                {
                    // 선택되지 않은 상태로 만듭니다.
                    Buttons[i].GetComponent<Image>().color = nopick;
                }
            }

            // 시작 버튼의 불을 끕니다.
            Buttons[Buttons.Length - 1].GetComponent<Image>().color = nopick;
        }

    }

    /// <summary>
    /// 선택지 3개에 따라서 선택지의 그림들을 바꿔줍니다.
    /// </summary>
    private void PickManage()
    {
        for(int i = 0; i < 3; i++)
        {
            // i번째의 그림에 i번째의 선택지에담긴 클래스 이미지를 넣습니다.
            Pick3Sprite[i].sprite = GameManager.G_M.ClassSprites[Pick3[i]];
        }
    }

    /// <summary>
    /// 선택지3개를 채워넣습니다.
    /// </summary>
    private void Pickin3()
    {
        // 골라진게 3개가 아니라면 반복합니다.
        while(Pick3.Count != 3)
        {
            // 일단 랜덤으로 직업중 1개를 고릅니다.
            int input = Random.Range(0, (int)PongClass.END);

            // 만약 직업중 1개를 포함하지 않았다면 집어넣습니다.
            if(Pick3.Contains(input) == false)
            {
                Pick3.Add(input);
            }

        }
    }

    /// <summary>
    /// UI를 오브젝트의 위치에 맞게 조절합니다
    /// </summary>
    private void PickUImange()
    {
        for(int i = 0; i < Pick3Sprite.Count; i++)
        {
            Buttons[i].transform.position = 
                Camera.main.WorldToScreenPoint(Pick3Sprite[i].transform.position + new Vector3(0, -2.5f, 0));

        }
    }

    #endregion

    #region 버튼의 경우에수 입니다.
    // 버튼의 경우에수 입니다.

    // 전부 골라진 상태 1
    // 아직 안골라진 상태 2

    // 전부 골라져있을떄:1
    // 이미 선택된걸 누를때11
    // 선택되지 않은걸 누를떄12

    // 아직 다 안골랐을떄:2
    // 이미 선택된걸 누를때21
    // 선택되지 않은걸 누를때22
    #endregion

    /// <summary>
    /// 버튼 클릭시 색을 바꿔주고 2개의 선택지에 추가시킵니다.
    /// </summary>
    /// <param name="index">버튼의 순서입니다</param>
    public void ChangeColor(int index)
    {

        // 전부 골라져있을떄 :
        if (AddPick.Count == 2)
        {
            // 이미 선택된걸 누를때
            if (Buttons[index].GetComponent<Image>().color == pick)
            {
                // 선택된 버튼을 선택되지 않은상태로 만듭니다.
                Buttons[index].GetComponent<Image>().color = nopick;

                // 선택된 숫자를 1개줄입니다.
                if(AddPick.Contains(Pick3[index]))
                {
                    AddPick.Remove(Pick3[index]);
                }

            }
            // 선택되지 않은걸 누를때
            else
            {
                // 아무일 없습니다.
            }
        }
        // 아직 다 안골랐을떄:
        else
        {
            // 이미 선택된걸 누를때
            if (Buttons[index].GetComponent<Image>().color == pick)
            {
                // 선택된 버튼을 선택되지 않은상태로 만듭니다.
                Buttons[index].GetComponent<Image>().color = nopick;

                // 선택된 숫자를 1개줄입니다.
                if (AddPick.Contains(Pick3[index]))
                {
                    AddPick.Remove(Pick3[index]);
                }
            }
            // 선택되지 않은걸 누를때
            else
            {
                // 선택되지 않은 버튼을 선택된 상태로 만듭니다.
                Buttons[index].GetComponent<Image>().color = pick;

                // 선택된 숫자를 1개 늘립니다.
                if (AddPick.Contains(Pick3[index]) == false)
                {
                    AddPick.Add(Pick3[index]);
                }
            }
        }

    }



    #region 씬 관리목록

    List<Tween> GetTweens = new List<Tween>();

    public void MoveToStart()
    {
        // 버튼들을 투명하게 만들기위해 찾습니다
        Image[] gameObjectss = new Image[FindObjectsOfType<Image>().Length];
        gameObjectss = FindObjectsOfType<Image>();

        for (int i = 0; i < gameObjectss.Length; i++)
        {
            GetTweens.Add(gameObjectss[i].DOFade(0, 0.15f));
            GetTweens.Add(gameObjectss[i].transform.GetComponentInChildren<Text>().DOFade(0, 0.15f));
        }

        // 시작화면으로 이동시킵니다.
        GameManager.G_M.ChangeScene("StartScene");
    }

    // 여러번 넘어가지않게 조절합니다
    [SerializeField]
    bool ch;

    public void MoveToMain()
    {



        if (Buttons[Buttons.Length - 1].GetComponent<Image>().color == pick && ch == false)
        {
            // 버튼들을 투명하게 만들기위해 찾습니다
            Image[] gameObjectss = new Image[FindObjectsOfType<Image>().Length];
            gameObjectss = FindObjectsOfType<Image>();
            for (int i = 0; i < gameObjectss.Length; i++)
            {
                GetTweens.Add(gameObjectss[i].GetComponent<Image>().DOFade(0, 0.15f));
                GetTweens.Add(gameObjectss[i].transform.GetComponentInChildren<Text>().DOFade(0, 0.15f));

            }

            // 넘어가는걸 표시합니다
            ch = true;
            // 퐁2개를 추가시킵니다
            GameManager.G_M.AddPongs(AddPick[0]);
            GameManager.G_M.AddPongs(AddPick[1]);
            GameManager.G_M.RandAddParty();
            GameManager.G_M.RandAddParty();


            // 랜덤맵을 생성시킵니다
            GameManager.G_M.DungeanRand();
            // 메인화면으로 이동시킵니다.
            GameManager.G_M.ChangeScene("Main_S");
        }
    }
    #endregion

    #endregion

    private void OnDestroy()
    {
        for (int i = 0; i < GetTweens.Count; i++)
        {
            GetTweens[i].Kill();
        }
    }
}
