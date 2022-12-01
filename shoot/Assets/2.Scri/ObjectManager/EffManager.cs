using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffManager : MonoBehaviour
{
    // 이펙트 매니저니까 이펙트를 대상으로 집어줘야죠
    Animator anim;

    #region 기본 함수

    // Start is called before the first frame update
    private void Awake()
    {
        // 자신의 애니매이터에 참조를 합니다.
        anim = GetComponent<Animator>();
    }

    #endregion

    #region 커스텀 함수

    // 폭팔을 불러오는 함수입니다.
    public void StartExplosion(string target)
    {
        // 애니메이터에서 지정해놓은 OnEx를 실행시켜 폭팔하게 만듭니다.
        anim.SetTrigger("OnExPlo");

        // 타겟에 따라서 폭발 크기를 조정합니다.
        switch (target)
        {
            // A적은 작은 폭팔이 일어납니다.
            case "A":
                transform.localScale = Vector3.one * 0.9f;
                break;
            // A의 실드는 좀 더 작아용.
            case "A_C":
                transform.localScale = Vector3.one * 0.4f;
                break;
            // 중간보스급은 조금 더 큰 폭팔이요.
            case "M_B":
                transform.localScale = Vector3.one * 1.2f;
                break;
            // 중간보스의 실드도 좀 더 작아용.
            case "M_B_C":
                transform.localScale = Vector3.one * 0.5f;
                break;
            case "P":
                transform.localScale = Vector3.one * 1;
                break;
            default:
                break;
        }
    }

    #endregion
}
