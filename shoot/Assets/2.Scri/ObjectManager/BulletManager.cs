using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    // 총소리를 내어 봅시다.
    public AudioSource audioSource;

    // 공격은 공격력을 가지고 있습니다.
    public int Damage;

    // 공격은 단순 접촉후 사라지는게 아니라 지속되는 타입도 있습니다.
    public bool StaticA;

    // 지속타입이라면 지속시간도 있겠죠.
    public float StaticTimeMax;
    public float StaticTime;

    // ps: 지속되는 타입은 여러가지 이유가 있습니다.

    // 1. 다단히트형, 사이오닉스톰처럼 범위에 계속 남아서 피해를 입히는방식
    // 2. 적 관통형 총알이라서 부딪혀도 사라지지 않으면서 뒤의 적을 공격하는방식


    // 지속형 공격은 공격을 살포하기로 했습니다.
    // 살포 재장전 시간과 살포시간값입니다.
    public float AttackCool;
    public float AttackTime;

    // 지속형 공격은 단순 재장전만 하는게 아니라 선딜을 넣어서 처음 공격을 제어해야합니다.
    public float FirstAttack;

    #region 기본 함수

    private void Awake()
    {

        audioSource = gameObject.AddComponent<AudioSource>();
        // 뮤트: true일 경우 소리가 나지 않음
        audioSource.mute = false;

        // 루핑: true일 경우 반복 재생
        audioSource.loop = false;

        // 자동 재생: true일 경우 자동 재생
        audioSource.playOnAwake = false;
    }

    private void OnEnable()
    {
        audioSource.Play();
        // 공격시간값을 재장전시간-선딜 로 해두어서 선딜만큼만 시간이 지나면
        // 첫 공격이 나갈겁니다.
        AttackTime = AttackCool - FirstAttack;

        StaticTime = 0;
    }

    // 공격은 지속형일경우에 살포 싸이클을 하면서 씁니다.
    private void Update()
    {
        // 재장전!
        TimeManager();

        // 공격함수는 트리거엔터가 해주므로 재장전만 있으면 될겁니다.
        // 아마도...

        // 공격시간이 재사용보다 크면 실행합니다.
        if (AttackTime > AttackCool)
        {

            // 발사를 해서 시간이 초기화됩니다.
            AttackTime = 0;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {



        // 총알이 경계를 넘으면 비활성화됩니다.
        if (collision.gameObject.tag == "BorderOut")
        {
            // 총알이 사라집니다. 으어억!
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // 공격시간이 재사용보다 크면 실행합니다.
        if (AttackTime > AttackCool)
        {
            // 지속형 공격이 적과 닿을경우 입니다.
            if (other.gameObject.tag == "Enemy" && StaticA)
            {
                // 공격 살포!
                Splash(other);
            }

            // 발사를 해서 시간이 초기화됩니다.
            AttackTime = 0;
        }
    }


    #endregion

    #region 커스텀 함수

    // 공격을 살포하는 함수입니다.
    private void Splash(Collider collision)
    {

        // 에너미매니저를 불러와서 대상으로 삼습니다.
        EnemyManager enemyManager = collision.gameObject.GetComponent<EnemyManager>();

        // 대상으로 삼은 에너미매니저를 데미지를 입힙니다.
        enemyManager.OnHit(Damage);
    }

    private void TimeManager()
    {
        // 지속형이 아니라면 돌아가
        if(StaticA == false)
        {
            return;
        }

        // 재장전시간을 체크합니다.
        AttackTime += Time.deltaTime;

        // 지속시간을 체크합니다.
        StaticTime += Time.deltaTime;

        // 지속시간이 최대 지속시간보다 크다면 사라져야죠
        if(StaticTime > StaticTimeMax)
        {
            gameObject.SetActive(false);
        }
    }
    #endregion

}
