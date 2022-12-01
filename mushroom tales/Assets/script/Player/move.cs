using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public GameObject sprite;
    public GameObject game;

    #region value
    /// <summary>
    /// ĳ������ �ִϸ�����
    /// </summary>
    private Animator Animator;

    /// <summary>
    /// ĳ������ �̵����ǵ�
    /// </summary>
    private float speed;

    /// <summary>
    /// �ǰ�ó���� ��Ʈ
    /// </summary>
    private int shakeint;

    /// <summary>
    /// �� ��� �� ����� ��Ʈ
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
        // �¿�
        float h = Input.GetAxisRaw("Horizontal");
        // ����
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

        // ������Ű�� ��Ű�� ����̴�

        // ������Ű�� x��ǥ�̰� hor
        // ��Ű�� y��ǥ�̰� ver

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

    #region �ڷ�ƾ

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
