using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public GameObject sprite;
    public GameObject game;

    #region value
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


    public List<GameObject> gameItems;

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
        moving();


    }
    #endregion


    private void moving()
    {
        // 좌우
        float h = Input.GetAxisRaw("Horizontal");
        // 상하
        float v = Input.GetAxisRaw("Vertical");
        Vector3 curPos = game.transform.position;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            hide = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            hide = false;
        }

        if (hide == true || Animator.GetCurrentAnimatorStateInfo(0).IsName("hideOut")
                         || Animator.GetCurrentAnimatorStateInfo(0).IsName("hide"))
        {
            h = 0;
            v = 0;
            
        }
        
        Animator.SetBool("hide", hide);
        

        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        // 오른쪽키와 윗키가 양수이다

        // 오른쪽키는 x좌표이고 hor
        // 윗키는 y좌표이고 ver

        game.transform.position = curPos + nextPos;


        Animator.SetInteger("Ver", (int)v);
        Animator.SetInteger("Horizontal", (int)h);

        //if(h == 0 && v == 0)
        //{
        //    Animator.SetInteger("speed", 0);
        //}
        //else
        //{
        //    Animator.SetInteger("speed", 1);
        //}



    }

    private void chageItem()
    {



    }

    public void Shaking()
    {
        StartCoroutine(shaked());


    }

    #region 코루틴

    IEnumerator shaked()
    {
        Debug.Log(shakeint);
        shakeint++;
        if(shakeint % 2== 1)
        {
            sprite.gameObject.transform.localPosition = new Vector2(0.1f,0.8f);
        }
        else
        {
            sprite.gameObject.transform.localPosition = new Vector2(-0.1f, 0.8f);
        }

        sprite.GetComponent<SpriteRenderer>().color = new Color(255/255f, 100/255f, 100/255f);

        if(shakeint != 5)
        {
            yield return new WaitForSeconds(0.05f);
            StartCoroutine(shaked());
        }
        else
        {
            sprite.gameObject.transform.localPosition = new Vector2(0, 0.8f);
            sprite.GetComponent<SpriteRenderer>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
            shakeint = 0;
        }

        
    }

    


    #endregion

}
