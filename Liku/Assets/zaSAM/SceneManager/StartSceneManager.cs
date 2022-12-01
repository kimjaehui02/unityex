using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class StartSceneManager : MonoBehaviour
{

    public void MoveToPick_S()
    {
        // 선택화면으로 이동시킵니다.
        GameManager.G_M.ChangeScene("Story_S");
    }
}
