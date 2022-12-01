using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    #region 기본 함수
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region 커스텀 함수
    public void MoveToRest()
    {
        // 레스트 씬으로 이동시켜줍니다.
        SceneManager.LoadScene("RestScene");
    }

    #endregion






}

