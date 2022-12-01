using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    // 배틀매니저를 받아와서 도움을 좀 받습니다
    // 총알생성이라던가...
    public BattleSceneManager B_S_M;

    // 머니라는 친구는 플레이어에게 돌격할놈입니다.
    public PlayerManager PlayerTransform;


    // 물리제어인 리지드바디를 저장합니다.
    public Rigidbody2D rigid;


    // 얼마를 줄건데?
    public int MoneyPoint;

    // 머니가 나아갈 방향을 지정해 줍니다.
    Vector2 vector3;

    // 플레 기?
    public bool ReadyPla;

    #region 기본 함수 

    // 돈, 생성!
    private void Awake()
    {
        // 프리펩이지만 초기화는 하고싶어!
        // 프리펩에서 생성된 오브젝트는 씬에있는 사물로 초기화를 해주려면 따로
        // 코드가 필요합니다.
        // 그래서 배틀매니저(사물)를 찾은다음에 컴포넌트로 배틀매니저(스크립트)
        // 로 참조를 해줍니다 ^^7
        B_S_M = GameObject.FindWithTag("B_S_M").GetComponent<BattleSceneManager>();

        // 이번엔 변수에다 리지드바디를요.
        rigid = GetComponent<Rigidbody2D>();

        // 이 씬의 플레는 누구냐
        PlayerTransform = GameObject.FindWithTag("Player").GetComponent<PlayerManager>();

        // 아직 갈준비 안됫어 ㅡㅡ;
        ReadyPla = false;
    }

    private void FixedUpdate()
    {
        GoToPl();
    }

    // 오브젝트가 닿는걸 체크합니다.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어에게 닿았는지를 체크합니다.
        if (collision.gameObject.tag == "Player")
        {
            MoneysGone();
        }

    }

    #endregion

    #region 커스텀 함수 

    #region public
    public void MoneysGone()
    {
        // 미처 다 못보낸 돈을 보내줘야죠
        B_S_M.GetMoney(MoneyPoint);

        // 송금을 했으니 빈털털이가 됩니다. ㅠㅠ
        MoneyPoint = 0;

        // 단물 쪽쪽 빨리고 빈털털이가 되었으니 퇴장할시간입니다.
        gameObject.SetActive(false);
    }

    // 에너미에게서 튕겨나옵니다.
    public void RanOut()
    {
        //transform.position;

        // 일단 자기자신의 위치를 저장해줍시다.
        Vector2 dirVec = transform.position;

        // 그리고 랜덤으로 튕겨나갈 방향을 정해줍시다.
        Vector2 ranVec = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(0f, 2f));

        // 그리고 이 둘을 합칩니다.
        dirVec += ranVec;

        // 그리고 합친 값으로 튕겨나가게합니다.
        rigid.AddForce(dirVec.normalized * 3.5f, ForceMode2D.Impulse);

        if (gameObject.activeSelf == true)
        {
            Invoke("Stop", 0.15f);
        }
    }

    #endregion

    #region private

    // 정지! 그리고 멈추고나면 플레이어에게 날아랏!
    private void Stop()
    {
        // 이 물체를 정지합니다.
        rigid.velocity = Vector2.zero;
        //Debug.Log("스땁");
        // 0.6f초후 도착할역은 플레, 플레역입니다.
        if (gameObject.activeSelf == true)
        {
            Invoke("ReadyToGo", 0.6f);
        }
    }

    // 이제 그만 보내주자...
    private void ReadyToGo()
    {
        // 갈준비 ON
        ReadyPla = true;
    }

    // 날 아 라
    private void GoToPl()
    {
        // 뭐어? 갈준비가 안됬다고?
        if (ReadyPla == false)
        {
            // bool이 없으니 빠꾸먹지
            return;
        }
        //Debug.Log("고플");

        // 조준 플레이어!
        vector3 = PlayerTransform.transform.position - (transform.position);

        // 발싸!
        rigid.velocity = Vector2.zero;
        rigid.AddForce(vector3.normalized * 350 * Time.deltaTime, ForceMode2D.Impulse);
    }

    #endregion

    #endregion
}
