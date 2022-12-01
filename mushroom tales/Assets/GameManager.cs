using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region 싱글톤
    /* // 싱글톤 //
 * instance라는 변수를 static으로 선언을 하여 다른 오브젝트 안의 스크립트에서도 instance를 불러올 수 있게 합니다 
 */
    public static GameManager instance = null;

    private void singleAwake()
    {
        if (instance == null) //instance가 null. 즉, 시스템상에 존재하고 있지 않을때
        {
            instance = this; //내자신을 instance로 넣어줍니다.
            DontDestroyOnLoad(gameObject); //OnLoad(씬이 로드 되었을때) 자신을 파괴하지 않고 유지
        }
        else
        {
            if (instance != this) //instance가 내가 아니라면 이미 instance가 하나 존재하고 있다는 의미
                Destroy(this.gameObject); //둘 이상 존재하면 안되는 객체이니 방금 AWake된 자신을 삭제
        }
    }
    #endregion

    //[field: SerializeField]
    [SerializeField]
    public EffectManager EffectManager;

    private void Awake()
    {
        singleAwake();
        
    }


    #region 데이터들

    #region 체력바 관련들
    /// <summary>
    /// 플레이어의 최대 체력
    /// </summary>
    [field: SerializeField]
    public float MaxHp { get; set; }
    /// <summary>
    /// 플레이어의 현재 체력
    /// </summary>
    [field: SerializeField] 
    public float Hp { get; set; }



    #endregion

    #region sp바 관련들
    /// <summary>
    /// 플레이어의 최대 기술포인트
    /// </summary>
    [field: SerializeField] 
    public float MaxSp { get; set; }
    /// <summary>
    /// 플레이어의 현재 기술포인트
    /// </summary>
    [field: SerializeField] 
    public float Sp { get; set; }


    #endregion

    #region 아이템창 관련들
    /// <summary>
    /// 가져다 쓸 사용형 아이템 스프라이트들
    /// </summary>
    [field: SerializeField]
    public Sprite[] ActiveSprites { get; set; }

    /// <summary>
    /// 가져다 쓸 사용형 아이템 스프라이트들
    /// </summary>
    [field: SerializeField]
    public Sprite[] PassiveSprites { get; set; }

    /// <summary>
    /// 사용형 아이템들을 인트형으로 저장해놓은 배열
    /// </summary>
    [field: SerializeField]
    public int[] Activeint { get; set; }

    /// <summary>
    /// 패시브 아이템을 인트형으로 저장함
    /// </summary>
    [field: SerializeField]
    public int Passiveint { get; set; }

    /// <summary>
    /// 현재 ui에서 가리키고 있는 아이템
    /// </summary>
    [field: SerializeField]
    public int TargetItem { get; set; }
    #endregion



    #endregion




}
