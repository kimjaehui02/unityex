using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.IO;

public class ChatManager : MonoBehaviour
{
    // 여러 대사가 있는 스크립트들을 불러오기위해 사용합니다

    /// <summary>
    /// 대사를 띄워놓는 박스입니다
    /// </summary>
    public GameObject ChatBox;

    /// <summary>
    /// 텍스트들입니다 0 = 제목, 1 = 본문, 2~ 선택지들
    /// </summary>
    public List<Text> GetTexts;

    /// <summary>
    /// 채팅과 상호작용하는 함수들을 저장해놓은 스크립트입니다
    /// </summary>
    public ChatLists ChatLists;

    /// <summary>
    /// 분리해서 읽기 위해 해당 라인을 통채로 읽습니다
    /// </summary>
    public string Line;

    /// <summary>
    /// 트윈입니다
    /// </summary>
    public List<Tween> GetTweens;

    /// <summary>
    /// 위에있는 버튼입니다
    /// </summary>
    public Button ButtonUp;

    /// <summary>
    /// 위에있는 버튼입니다
    /// </summary>
    public Button ButtonDown;

    #region 기본함수

    private void Awake()
    {
        // 트윈들을 초기화합니다
        GetTweens = new List<Tween>();
        GetTweens.Clear();
        
    }
    private void OnEnable()
    {
        Debug.Log(1329861239);
        FindObjectOfType<StoryManager>().CanvasControll(true);
    }

    private void OnDisable()
    {
        if(FindObjectOfType<StoryManager>() != null)
        {
            FindObjectOfType<StoryManager>().CanvasControll(true);

        }

    }

    private void OnDestroy()
    {
        // 트윈을 전부 제거해서 에러가 없게 합니다
        for (int i = 0; i < GetTweens.Count; i++)
        {
            GetTweens[i].Kill();
        }


    }

    #endregion

    /// <summary>
    /// 랜덤맵 등장시 작동하는 스크립트입니다
    /// </summary>
    public void ChatAwake()
    {
        // 대사창을 활성화합니다
        ChatBox.SetActive(true);

        // 일정값에 따라서 대사창을 띄웁니다
        RandChat();
    }

    // indexX는 숫자로 이루어진 행의 순서입니다 ex:1,2,3,4
    // i는 알파벳으로 이루어진 열의 순서입니다 ','를 단위로 잘라져서 구분합니다 ex: a,b,c,d
    // ii는 셀 안의 문자들을 '#'로 자른 단위입니다 보통은 1개이지만 #1개당 1개가 늘어납니다

    /// <summary>
    /// 랜덤시 발생하는 스크립트들을 가져옵니다
    /// </summary>    
    /// <param name="number">채팅 번호입니다</param>

    public void RandChat(int number = 100)
    {
        if(number == 100)
        {
            number = Random.Range(0, 1);
        }

        // 함수를 지정합니다
        ChatLists.number = number;

        // 파일을 읽어오는 2개의 코드입니다
        TextAsset textAsset = Resources.Load("CDATA") as TextAsset;
        StringReader stringReader = new StringReader(textAsset.text);

        // 가로줄입니다 직업명, 최대체력, 체력, 공격력, 타입 순으로 나열됩니다
        int indexX = -1;

        // 0번째 줄을 읽어옵니다 하지만 설명서가 들어있는 부분이므로 쓸모가 없습니다
        Line = stringReader.ReadLine();

        // 지정한 횟수가 될때까지 반복시킵니다
        while (indexX != number)
        {
            // 맨 첫줄은 표의 설명서가 들어있으니 넘깁니다
            //if (indexX == 0)
            //{
            //    Line = stringReader.ReadLine();
            //    indexX++;
            //    continue;
            //}

            indexX++;

            // indexX번째 줄을 읽어옵니다
            Line = stringReader.ReadLine();

            //if (Line == null)
            //{
            //    break;
            //}



        }



        // 맨 첫값은 확률이 적혀있으니 넘깁니다
        // 두번째 값은 가중치가 들어있으니 여기선 일단 넘깁니다
        // 세번째 값부터 받아주기 시작해야 하므로 2로 시작합니다
        // i가 1이면 a번쨰 2이면b번째를 의미합니다
        for (int i = 2; i < Line.Split(',').Length; i++)
        {
            // 아무것도 안적힌 칸은 넘깁니다
            if (Line.Split(',')[i] == "")
            {
                continue;
            }
            string test = "";

            // 전부 더해준 글자를 출력합니다

            for (int ii = 0; ii < Line.Split(',')[i].Split('#').Length; ii++)
            {
                test += Line.Split(',')[i].Split('#')[ii];
                // #이 있는 부분에서 자른 다음에 원하는 글자를 집어 넣어야합니다
                // #이 없는 배열은 길이가 1일테니 1이 아닐때 작동합니다
                // #이 있는 배열도 마지막에서 더해질 수 있으니 길이-1 == ii일떈 작동하지 않습니다
                // #이 있는 배열은 # 1개당 길이가 1 늘어납니다
                // 
                if (Line.Split(',')[i].Split('#').Length != 1 &&
                    Line.Split(',')[i].Split('#').Length - 1 != ii)
                {
                    test += "1";
                }


            }

            //Debug.Log(test);
            //Debug.Log(i + "?");

            // 크기가 적절하지 않는것은 실행하지 않습니다
            if (i - 2 < GetTexts.Count)
            {
                float delay = 0;
                // 글이 나타나는 속도를 조절합니다
                switch(i)
                {
                    case 2:// 제목
                        delay = 0.2f;
                        break;
                    case 3:// 본문
                        delay = 1.5f;
                        break;
                    case 4:case 5:// 선택지
                        delay = 0.8f;
                        break;
                }

                GetTweens.Add(GetTexts[i - 2].DOText(test, delay));

            }

        }

    }
}
