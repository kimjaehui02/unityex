using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestSceneManager : MonoBehaviour
{
    // G_M GM입니다!
    public GameManager G_M;

    // 옵션창입니당.
    public GameObject Option;

    // 이봐 스코어 어째서 제대로 돈을 표기하지않는것이지?
    public Text Score;

    #region 기본 함수

    private void Awake()
    {
        // GM등장! 쿠구구궁!
        G_M = GameObject.FindWithTag("G_M").GetComponent<GameManager>();

        // 옵션창을 비활성화 시킵니다.
        Option.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // 여긴 게임이야 까라면 까는곳이지 점수를 표기해랏!
        Score.text = string.Format("{0:n0}", G_M.Money);
    }

    #endregion

    #region 커스텀 함수

    // 전투 씬으로 이동해줍니다.
    public void MoveToBattle()
    {
        // 전투 화면으로 이동시킵니다.
        SceneManager.LoadScene("BattleScene");
    }

    // 옵션창을 키고 끄는 기능입니다.
    public void ChangeOption()
    {
        // 옵션이 켜져있다면 꺼야죠
        if(Option.activeSelf == true)
        {
            // 깜깜해용...
            Option.SetActive(false);
        }
        else
        {
            // 옵션이 꺼져있어서 켰습니다!
            Option.SetActive(true);
        }
    }

#endregion

}
