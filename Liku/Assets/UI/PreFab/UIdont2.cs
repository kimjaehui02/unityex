using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIdont2 : MonoBehaviour
{
    #region 게임매니저

    /// <summary>
    /// 게임매니저는 싱글톤 패턴입니다. 게임매니저를 담아두는 인스턴스입니다.
    /// </summary>
    private static UIdont2 instance = null;

    /// <summary>
    /// 게임매니저 인스턴스에 접근가능한 프로퍼티
    /// </summary>
    public static UIdont2 G_M
    {
        // get는 값을 반환하기위헤 사용합니다.
        get
        {
            // 인스턴스가 널이라면 똑같이 널을 반환합니다.
            if (instance == null)
            {
                return null;
            }

            // 그렇지 않다면 인스턴스를 제대로 반환합니다.
            return instance;
        }

    }

    /// <summary>
    /// 게임매니저가 Awake떄 해야할 일들입니다.
    /// </summary>
    private void GameMangerAwake()
    {
        // 만약에 인스턴스가 비어있다면
        if (instance == null)
        {
            // 이 클래스 인스턴스가 탄생했을 때 전역변수 instance에 게임매니저 인스턴스가
            // 담겨있지 않다면 자신을 넣어준다.
            instance = this;

            // 씬 전환이 되어도 파괴되지 않게 한다.
            DontDestroyOnLoad(this.gameObject);


        }
        // 인스턴스가 비어있지 않다면
        else
        {
            // 씬 이동이 된 후에 그 씬에도 게임매니저가 있을수 있으니 새 씬의
            // 게임매니저를 삭제해 준다.
            Destroy(this.gameObject);
        }
    }

    #endregion

    private void Awake()
    {
        GameMangerAwake();
    }
}
