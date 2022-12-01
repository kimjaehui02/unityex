using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Nextmanafer : MonoBehaviour
{
    /// <summary>
    /// 스토리매니저입니다
    /// </summary>
    public StoryManager StoryManager;

    /// <summary>
    /// 다음 맵으로 가는 타입입니다 0. 메인 1. 배틀
    /// </summary>
    public int nextTYpe;

    /// <summary>
    /// 대상으로 지정한 맵의 가로좌표입니다
    /// </summary>
    public int NGA;

    /// <summary>
    /// 대상으로 지정한 맵의 세로좌표 입니다
    /// </summary>
    public int NSE;

    /// <summary>
    /// 씬을 이동시키는 함수입니다
    /// </summary>
    public void MoveToScene()
    {
        // 선택된 씬에 따라서 다른 씬으로 가야합니다

        // 지금좌표를 다음좌표로 고칩니다
        GameManager.G_M.MGA = NGA;
        GameManager.G_M.MSE = NSE;

        // UI를 올려줍니다
        // 지도를 닫아줍니다
        StoryManager.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 200);
        // 지도를 접었다고 저장합니다
        StoryManager.UDB = false;

        StoryManager.Pontwig();

        switch (nextTYpe)
        {
            case 0:

                // 메인화면으로 이동시킵니다.
                GameManager.G_M.ChangeScene("Main_S");
                break;
            case 1:
                // 배틀씬으로 이동시킵니다.
                GameManager.G_M.ChangeScene("Battle_S");
                break;
        }

    }


}
