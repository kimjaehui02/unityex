using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // G_M GM입니다!
    public GameManager G_M;

    // 에너미도 배틀매니저의 도움이 필요할겁니다.
    public BattleSceneManager B_S_M;

    #region 에너미의 세부 스탯
    // 에너미는 타입이 있습니다.
    public string EType;

    // 에너미는 각각의 체력을 가지고 있습니다.
    public int MaxHealth;
    public int Health;

    // 에너미는 각각 자신의 이동속도를 가지고 있습니다.
    public float Speed;

    // 에너미는 자신의 스프라이트를 여러개 가졌습니다.
    public Sprite[] Sprites;

    // 에너미도 플레이어처럼 공격의 쿨타임이 있습니다.
    public float AttackCool;
    public float AttackTime;

    // 보여줄 스프라이트를제어해줍니다.
    SpriteRenderer spriteRenderer;

    // 물리제어인 리지드바디를 저장합니다.
    Rigidbody2D rigid;

    #endregion

    #region 기본 함수//----------------------------------------------------------------

    #region public
    public void OnEnable()
    {
        // 현재 체력을 최대체력만큼 받아야겠죠.
        Health = MaxHealth;

        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }
    }

    #endregion

    #region private
    // 등장시에 처리해줄것들을 여기 담아줍니다.
    private void Awake()
    {
        // 프리펩이지만 초기화는 하고싶어!
        // 프리펩에서 생성된 오브젝트는 씬에있는 사물로 초기화를 해주려면 따로
        // 코드가 필요합니다.
        // 그래서 배틀매니저(사물)를 찾은다음에 컴포넌트로 배틀매니저(스크립트)
        // 로 참조를 해줍니다 ^^7
        B_S_M = GameObject.FindWithTag("B_S_M").GetComponent<BattleSceneManager>();

        // GM등장! 쿠구구궁!
        G_M = GameObject.FindWithTag("G_M").GetComponent<GameManager>();

        // 변수에다가 자신의 스프라이트 렌더러를 참조할수잇게합니다.
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 이번엔 변수에다 리지드바디를요.
        rigid = GetComponent<Rigidbody2D>();

        // 현재 체력을 최대체력만큼 받아야겠죠.
        Health = MaxHealth;

        if(gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    private void Update()
    {

        // 그 리지드바디에 속도값을 부여합니다.
        // 그리고 이 문장은 적이 아래로 내려가게합니다.
        rigid.velocity = Vector2.down * Speed;

        // 재장전 시간이 0이라면 어택사이클이 동작하지않습니다.
        if (AttackCool != 0)
        {
            // 쏴라!
            Fire();

            // 재장전!
            Reload();
        }
    }


    // 닿으면 실행합니다.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 외부 경계에 닿으면 퇴장을 해줘야죠
        if (collision.gameObject.tag == "BorderOut")
        {
            // 뿅!
            gameObject.SetActive(false);
        }
        // 그게아니라 총알이라면 데미지를 입습니다.
        else if (collision.gameObject.tag == "Attack")
        {
            // 접촉한 총알을 호출해줍시다.
            BulletManager bullet = collision.gameObject.GetComponent<BulletManager>();

            // 공격한 총알이 스태틱타입이라면 사라질수야 없죠.
            if (bullet.StaticA == true)
            {
                // 일단 공격이 지속형이라면 데미지를 주는것을 공격 자체에서 하기로 했습니다.
            }
            else
            {
                // 호출한 총알의 공격력만큼 피해를 입습니다. 으악!
                OnHit(bullet.Damage);

                // 공격한 발사체는 역할을 다했으니 이제 사라질시간입니다.
                collision.gameObject.SetActive(false);
            }
        }
    }

    #endregion

    #endregion//----------------------------------------------------------------



    #region 커스텀 함수//----------------------------------------------------------------

    #region private
    // 에너미는 플레이어의 공격에 맞으면 피해를 입을겁니다.
    public void OnHit(int dmg)
    {
        // 에너미는 공격을 당햇으니 데미지를 입습니다.
        Health -= dmg;


        // 공격을 당하면 잠시 탈색됩니다
        // 그러나 항상 스프라이트에 여유가 있는건 아니죠
        if (Sprites.Length > 0)
        {
            spriteRenderer.sprite = Sprites[1];
        }
        // 0.1초 후엔 복구도 되야죠. 
        Invoke("ReturnSprite", 0.1f);

        // 에너미가 공격을 당해 체력이 0이하가되면 비활성화가 될겁니다.
        if (Health <= 0)
        {
            // 으아악!
            EnemyDead();
        }
    }

    #endregion

    #region private

    // 탈색됬던 에너미를 고쳐줘야죠.
    private void ReturnSprite()
    {
        // 기본 스프라이트가 되어랏!
        if (Sprites.Length > 0)
        {
            spriteRenderer.sprite = Sprites[0];
        }
    }

    // 에너미가 주거씀다 ㅡㅡ;
    public void EnemyDead()
    {
        // 이미 비활성화가 되어있다면 실행을 하면 안되겟죠.
        if(gameObject.activeSelf == false)
        {
            // 어딜 바가지를!
            return;
        }

        // 하지만 죽었으니 일단 돈은 줘야겠죠. 내놔랏!
        SpawnMoney();

        // 자식이 있나 없나 하나하나 세보기위한 인덱스입니다.
        int index = 0;

        // 하지만 자식이 있다면 그돔의 돈도 가차없이 빨아내야합니다.
        while (index < transform.childCount)
        {
            // 자식을 잡아서 체크하기위한 에너미매니저입니다.
            EnemyManager childEnemy;
        
            // 인덱스번째의 자식을 대상으로 잡습니다.
            childEnemy = transform.GetChild(index).GetComponent<EnemyManager>();

            // 인덱스번째의 자식의 OnHit()으로 데미지를 줘 사라지게 만듭니다.
            childEnemy.OnHit(childEnemy.MaxHealth);

            // 다음 순번을 찾아봅시다.
            index++;
        }

        // 죽음의 메아리: 폭팔 이펙트를 남깁니다.
        B_S_M.MakeEff(transform.position, EType);

        // 뿅!
        gameObject.SetActive(false);
    }

    // 돈을 생성합니다.
    private void SpawnMoney()
    {
        // 머니를 생성합니다. 생성한 머니에 돈을 쥐여줍시다.
        MoneyManager money = B_S_M.MakeObj("Moneys").GetComponent<MoneyManager>();

        // 생성한 머니에 좌표를 부여합니다. 바로 내 시체 위에!
        money.transform.position = transform.position;

        //생성한 머니에 돈을 쥐여줍시다.
        money.MoneyPoint = MaxHealth;

        // 생성한 머니가 캐릭터를 향해 달려갈수있도록 해줍시다.
        money.RanOut();

        // 돈이 달려갈 준비가 미리 되있으면 안되니까 절제해 줍시다.
        money.ReadyPla = false;
    }

    // 에너미의 공격방식입니다.
    private void Fire()
    {
        // 총알을 발사하는 불값을 받습니다.
        bool inputFire = false;

        //inputFire = Input.GetButton("Fire1");

        // 불값이 충족되지 않으면 실행하지않습니다.
        if (inputFire)
        {
            return;
        }

        // 재장전시간이 일정 값이 넘지 않으면 실행하지 않습니다.
        if (AttackTime < AttackCool)
        {
            return;
        }


        // 총알을 생성하기위해 전투매니저에서 a총알을 요구합니다.
        GameObject bullet = B_S_M.MakeObj("BulletA");
        // 총알의 위치를 자기 위치에 소환시킵니다.
        bullet.transform.position = transform.position;
        // 생성한 총알의 리지드바디를 받아옵니다.
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

        // 생성한 총알에 추진력을 부여합니다.
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

        // 재장전시간을 초기화 합니다.
        AttackTime = 0;
    }

    // 사격과 재장전은 항상 세트입니다.
    private void Reload()
    {
        // 재장전시간을 체크합니다.
        AttackTime += Time.deltaTime;
    }

    #endregion

    #endregion//----------------------------------------------------------------

}
