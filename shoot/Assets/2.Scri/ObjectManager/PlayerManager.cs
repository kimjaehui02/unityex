using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // G_M GM입니다!
    public GameManager G_M;

    // 배틀매니저를 받아와서 도움을 좀 받습니다
    // 총알생성이라던가...
    public BattleSceneManager B_S_M;

    public GameObject MobileKeyPad;

    #region 플레이어 스테이터스

    // 최대공격 레벨입니다.
    public int MaxPowerLevel;
    // 현재공격 레벨입니다.
    public int PowerLevel;

    // 최대 체력입니다.
    public int MaxPlayerHp;
    // 체력입니다.
    public int PlayerHp;

    // 최대 폭탄의 개수입니다.
    public int MaxBoomCount;
    // 폭탄의 개수입니다.
    public int BoomCount;
    // 플레이어의 스피드 입니다.
    public float speed;

    // 사격 재장전 시간과 사격시간값입니다.
    public float AttackCool;
    public float AttackTime;

    #endregion

    #region 컨트롤 변수

    // 판넬로 조종하기 위한 변수들입니다.
    public bool[] joyControl;
    public bool isControl;

    // 플레이어의 상하좌우가 경계에 닿앗는지 체크하는 역할을 합니다.
    bool isTouchTop;
    bool isTouchBottom;
    bool isTouchRight;
    bool isTouchLeft;

    #endregion

    #region 기본 함수  

    private void Awake()
    {
        // GM등장! 쿠구구궁!
        G_M = GameObject.FindWithTag("G_M").GetComponent<GameManager>();

        // pc에 따라서 모바일 키패드를 비활성화합니다.
        CheckPC();
    }

    // Update is called once per frame
    private void Update()
    {
        // 키를 입력받아 움직히는 함수를 호출합니다.
        Move();

        // 발싸!  공격함수를 호출해줍니다.
        Fire();

        // 발사를 한 후 재장전함수를 호출해서 다시 공격을 가능하게 합니다.
        Reload();
    }

    // 플레이어가 닿으면 실행합니다.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 벽을 넘지 않게 합니다.
        if (collision.gameObject.tag == "Border")
        {
            switch(collision.gameObject.name)
            {
                // 머리가 닿았습니다. 쿵!
                case "Top":
                    isTouchTop = true;
                    break;

                // 엉덩방아를 찧었네요. 아이쿠!
                case "Bottom":
                    isTouchBottom = true;
                    break;

                // 오른팔이 비좁습니다. 윾.
                case "Right":
                    isTouchRight = true;
                    break;

                // 왼편이 길이 막혔네요. 하하하.
                case "Left":
                    isTouchLeft = true;
                    break;
            }
        }
    }

    // 닿은게 떨어지면 실행합니다.
    private void OnTriggerExit2D(Collider2D collision)
    {
        // 벽을 넘지 않게 합니다.
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                // 머리가 시원합니다.
                case "Top":
                    isTouchTop = false;
                    break;

                // 엉덩이가 땅에서 떨어졌네요.
                case "Bottom":
                    isTouchBottom = false;
                    break;

                // 오른팔이 거침없습니다.
                case "Right":
                    isTouchRight = false;
                    break;

                // 왼편의 길이 뚫렷네요!
                case "Left":
                    isTouchLeft = false;
                    break;
            }
        }
    }
    #endregion

    #region 커스텀 함수

    #region public

    // 패널로 플레이어를 움직여 줍시다.
    public void JoyPanel(int type)
    {
        // 어디 버튼이 눌렸는지 조사해 봅시다.
        for (int index = 0; index < 9; index++)
        {
            // 조사한 버튼이 어떤타입인가요?
            joyControl[index] = index == type;
        }
    }

    // 버튼이 눌렷습니다.
    public void JoyDown()
    {
        isControl = true;
    }

    // 버튼을 뗏습니다.
    public void JoyUp()
    {
        isControl = false;
    }

    #endregion


    #region private

    // PC인지 아닌지에 따라서 키패드를 활성화합니다.
    private void CheckPC()
    {
        // 먼저 gm의 pc값을 받아옵니다.

        // PC라면 키패드를 비활성화합니다.
        if(G_M.G_M_PC == true)
        {
            // pc에 따라서 모바일 키패드를 비활성화합니다.
            MobileKeyPad.SetActive(false);
        }

    }

    // 플레이어를 움직여 줍니다.
    private void Move()
    {
        // 플레이어가 누르는 버튼을 입력받습니다.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical"); ;

        // pc가 아닐때만 작동합니다.
        if (G_M.G_M_PC == false)
        {
            // 조이 컨트롤을 넣어봅시다.
            if (joyControl[0]) { h = -1; v = 1; }
            if (joyControl[1]) { h = 0; v = 1; }
            if (joyControl[2]) { h = 1; v = 1; }
            if (joyControl[3]) { h = -1; v = 0; }
            if (joyControl[4]) { h = 0; v = 0; }
            if (joyControl[5]) { h = 1; v = 0; }
            if (joyControl[6]) { h = -1; v = -1; }
            if (joyControl[7]) { h = 0; v = -1; }
            if (joyControl[8]) { h = 1; v = -1; }
        }
        else
        {
            isControl = true;
        }


        // 경계를 넘지 않도록 0으로 만듭니다.
        if ((isTouchRight && h == 1) || (isTouchLeft && h == -1) || !isControl)
        {
            h = 0;
        }

        if ((isTouchTop && v == 1) || (isTouchBottom && v == -1) || !isControl)
        {
            v = 0;
        }

        // 현재 위치를 저장합니다.
        Vector3 curPos = transform.position;
        // 변위를 계산합니다.
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        // 현재 위치에 변위를 더해줍니다.
        transform.position = curPos + nextPos;
    }

    // 키 입력을 받아서 총알을 발사합니다.
    private void Fire()
    {
        // 총알을 발사하는 불값을 받습니다.
        bool inputFire = false;

        inputFire = Input.GetButton("Fire1");

        // 불값이 충족되지 않으면 실행하지않습니다.
        if (!inputFire)
        {
            return;
        }

        // 재장전시간이 일정 값이 넘지 않으면 실행하지 않습니다.
        if(AttackTime < AttackCool)
        {
            return;
        }

        // 힘의 단계에 따라 발사하는 총알을 다르게 설정합니다.
        switch(PowerLevel)
        {
            // 파워 단계가 1일때 발사하는 총알입니다.
            case 1:
                // 총알을 생성하기위해 전투매니저에서 a총알을 요구합니다.
                GameObject bullet = B_S_M.MakeObj("BulletA");

                // 총알의 위치를플레이어 위치에 소환시킵니다.
                bullet.transform.position = transform.position;

                // 생성한 총알의 리지드바디를 받아옵니다.
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

                // 생성한 총알에 추진력을 부여합니다.
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 2:
                // 총알을 생성하기위해 전투매니저에서 a총알을 요구합니다.
                GameObject bullet21 = B_S_M.MakeObj("BulletA");
                GameObject bullet22 = B_S_M.MakeObj("BulletA");

                // 총알의 위치를플레이어 위치에 소환시킵니다.
                bullet21.transform.position = transform.position;
                bullet21.transform.Translate(0.1f, 0, 0);

                bullet22.transform.position = transform.position;
                bullet22.transform.Translate(-0.1f, 0, 0);

                // 생성한 총알의 리지드바디를 받아옵니다.
                Rigidbody2D rigid21 = bullet21.GetComponent<Rigidbody2D>();
                Rigidbody2D rigid22 = bullet22.GetComponent<Rigidbody2D>();

                // 생성한 총알에 추진력을 부여합니다.
                rigid21.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                // 생성한 총알에 추진력을 부여합니다.
                rigid22.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 3:
                // 총알을 생성하기위해 전투매니저에서 a총알을 요구합니다.
                GameObject bullet31 = B_S_M.MakeObj("BulletA");
                GameObject bullet32 = B_S_M.MakeObj("BulletA");
                GameObject bullet33 = B_S_M.MakeObj("BulletA");

                // 총알의 위치를플레이어 위치에 소환시킵니다.
                bullet31.transform.position = transform.position;
                bullet31.transform.Translate(0, 0.1f, 0);

                bullet32.transform.position = transform.position;
                bullet32.transform.Translate(0.2f,0,0);

                bullet33.transform.position = transform.position;
                bullet33.transform.Translate(-0.2f, 0, 0);

                // 생성한 총알의 리지드바디를 받아옵니다.
                Rigidbody2D rigid31 = bullet31.GetComponent<Rigidbody2D>();
                Rigidbody2D rigid32 = bullet32.GetComponent<Rigidbody2D>();
                Rigidbody2D rigid33 = bullet33.GetComponent<Rigidbody2D>();

                // 생성한 총알에 추진력을 부여합니다.
                rigid31.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigid32.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigid33.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                break;
        }

        // 재장전시간을 초기화 합니다.
        AttackTime = 0;
    }

    // 사격과 재장전은 항상 세트입니다.
    private void Reload()
    {
        // 재장전시간을 체크합니다.
        AttackTime += Time.deltaTime;
    }

    // 일정 범위의 적을 타격합니다.
    private void Scratch()
    {

    }


    #endregion

    #endregion
}
