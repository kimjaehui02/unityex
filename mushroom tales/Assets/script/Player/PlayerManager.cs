using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region 스크립트,컴포넌트
    /// <summary>
    /// 아이템의 충돌때 사용하는 충돌처리용 스크립트
    /// </summary>
    public PlayerItemcolider playerItemcolider;

    /// <summary>
    /// 공격용 막대기
    /// </summary>
    public GameObject AttackStick;

    public PlayerUi playerUi;

    #endregion


    #region value

    /// <summary>
    /// 움직이는 캐릭터의 스프라이트들
    /// </summary>
    public GameObject sprite;

    /// <summary>
    /// 실제로 작동하는 가장 큰 부모 게임오브젝트
    /// </summary>
    public GameObject game;

    /// <summary>
    /// 캐릭터의 애니메이터
    /// </summary>
    private Animator Animator;

    /// <summary>
    /// 캐릭터의 이동스피드
    /// </summary>
    private float speed;

    /// <summary>
    /// 피격처리용 인트
    /// </summary>
    private int shakeint;

    /// <summary>
    /// 갓 방어 및 숨기용 인트
    /// </summary>
    private bool hide;

    private float h2;
    private float v2;

    //public List<GameObject> gameItems;

    #endregion

    #region default

    private void Awake()
    {
        Animator = sprite.gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        speed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
        ChangeUse();
        ChageItem();
        Attack();

    }
    #endregion

    #region 커스텀 private
    private void Moving()
    {
        // 좌우
        float h = Input.GetAxisRaw("Horizontal");
        // 상하
        float v = Input.GetAxisRaw("Vertical");
        Vector3 curPos = game.transform.position;

        if(Time.timeScale != 1)
        {
            h = h2;
            v = v2;
        }

        // 숨기 동작중인지를 체크하는 불
        bool hidding = false;

        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("hideOut")
              || Animator.GetCurrentAnimatorStateInfo(0).IsName("hide"))
        {
            hidding = true;
        }

        // 숨기키를 누르면 숨기 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            hide = true;
        }

        // 숨기키를 떼면 다시 나오기
        if (Input.GetKeyUp(KeyCode.Space))
        {
            hide = false;
        }

        // 숨기도중, 숨기 해제도중에는 안움직이게
        if (hide == true || hidding == true)
        {
            h = 0;
            v = 0;

        }

        Vector3 vector3attck = AttackStick.transform.rotation.eulerAngles;

        if(AttackStick.activeSelf ==false)
        {
            if (Animator.GetCurrentAnimatorStateInfo(0).IsName("idleA") || Animator.GetCurrentAnimatorStateInfo(0).IsName("walkA"))
            {
                vector3attck = new Vector3(0, 0, 0 + (h * 45));
                //if (AttackStick.GetComponent<StickManager>().animator.GetCurrentAnimatorStateInfo(0).IsName("Attack01") == false)
                //{
                //    vector3attck = new Vector3(0, 0, 0 + (h * 45));
                //}


            }

            if (Animator.GetCurrentAnimatorStateInfo(0).IsName("idleB") || Animator.GetCurrentAnimatorStateInfo(0).IsName("walkB"))
            {
                vector3attck = new Vector3(0, 0, 180 - (h * 45));
                //if (AttackStick.GetComponent<StickManager>().animator.GetCurrentAnimatorStateInfo(0).IsName("Attack01") == false)
                //{
                //    vector3attck = new Vector3(0, 0, 180 - (h * 45));
                //}


            }
        }



        //vector3attck = new Vector3(0, 0, 180 * h + 90 * v);

        #region 값을 사용하여 이동시킴
        // 좌우 방향키의 누른값을 바탕으로 백터3값을 만듬
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        // 오른쪽키와 윗키가 양수이다

        // 오른쪽키는 x좌표이고 hor
        // 윗키는 y좌표이고 ver

        // 만들어진 벡터3값으로 실제로 이동시킴
        game.transform.position = curPos + nextPos;

        AttackStick.transform.rotation = Quaternion.Euler(vector3attck);

        #endregion




        #region 애니메이터 값 변경

        // 숨기 버튼을 눌렀는지에 따라서 애니메이션을 진행
        Animator.SetBool("hide", hide);

        // 입력받은 키보드값으로 진행할 애니메이션을 선택함
        Animator.SetInteger("Ver", (int)v);
        Animator.SetInteger("Horizontal", (int)h);

        #endregion

        h2 = h;
        v2 = v;

    }

    private void ChangeUse()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            GameManager.instance.TargetItem = (GameManager.instance.TargetItem +1) %3 ;
        }

        for (int i = 0; i < 3; i++)
        {
            playerUi.colors[i].color = new Color(122/255f, 100/255f, 0/255f, 255f);
        }
        playerUi.colors[GameManager.instance.TargetItem].color = new Color(255/255f, 200.255f, 0/255f, 255/255f);
    }

    private void Attack()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {

            //if(AttackStick.GetComponent<StickManager>().animator.GetCurrentAnimatorStateInfo(0).IsName("Attack01"))
            //{
            //    return;
            //}

            AttackStick.SetActive(true);
        }
    }

    #endregion



    #region 커스텀 public
    public void ChageItem()
    {
        if (playerItemcolider.sqrMagnitudeObj == null)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            playerUi.ItemChange(playerItemcolider.sqrMagnitudeObj.GetComponent<ItemManager>());
        }



    }



    public void Shaking()
    {
        StartCoroutine(Shaked());
        StartCoroutine(KnockBack());

    }

    public void fired()
    {
        GameManager.instance.EffectManager.fire(gameObject);
    }
    #endregion



    #region 코루틴

    IEnumerator Shaked()
    {
        Debug.Log(shakeint);
        shakeint++;
        if (shakeint % 2 == 1)
        {
            //sprite.gameObject.transform.localPosition = new Vector2(0.1f, 0.8f);
        }
        else
        {
            //sprite.gameObject.transform.localPosition = new Vector2(-0.1f, 0.8f);
        }

        sprite.GetComponent<SpriteRenderer>().color = new Color(255 / 255f, 100 / 255f, 100 / 255f);



        if (shakeint != 5)
        {
            yield return new WaitForSecondsRealtime(0.05f);
            StartCoroutine(Shaked());
        }
        else
        {
            sprite.gameObject.transform.localPosition = new Vector2(0, 0.8f);
            sprite.GetComponent<SpriteRenderer>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
            shakeint = 0;

        }


    }

    IEnumerator KnockBack()
    {
        //gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 150));
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 30) / Time.timeScale;
        Time.timeScale = 0.3f;
        yield return new WaitForSecondsRealtime(0.25f);
        
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        Time.timeScale = 1f;
    }



    #endregion


}
