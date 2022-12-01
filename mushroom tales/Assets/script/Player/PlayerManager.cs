using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region ��ũ��Ʈ,������Ʈ
    /// <summary>
    /// �������� �浹�� ����ϴ� �浹ó���� ��ũ��Ʈ
    /// </summary>
    public PlayerItemcolider playerItemcolider;

    /// <summary>
    /// ���ݿ� �����
    /// </summary>
    public GameObject AttackStick;

    public PlayerUi playerUi;

    #endregion


    #region value

    /// <summary>
    /// �����̴� ĳ������ ��������Ʈ��
    /// </summary>
    public GameObject sprite;

    /// <summary>
    /// ������ �۵��ϴ� ���� ū �θ� ���ӿ�����Ʈ
    /// </summary>
    public GameObject game;

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

    #region Ŀ���� private
    private void Moving()
    {
        // �¿�
        float h = Input.GetAxisRaw("Horizontal");
        // ����
        float v = Input.GetAxisRaw("Vertical");
        Vector3 curPos = game.transform.position;

        if(Time.timeScale != 1)
        {
            h = h2;
            v = v2;
        }

        // ���� ������������ üũ�ϴ� ��
        bool hidding = false;

        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("hideOut")
              || Animator.GetCurrentAnimatorStateInfo(0).IsName("hide"))
        {
            hidding = true;
        }

        // ����Ű�� ������ ���� 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            hide = true;
        }

        // ����Ű�� ���� �ٽ� ������
        if (Input.GetKeyUp(KeyCode.Space))
        {
            hide = false;
        }

        // ���⵵��, ���� �������߿��� �ȿ����̰�
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

        #region ���� ����Ͽ� �̵���Ŵ
        // �¿� ����Ű�� �������� �������� ����3���� ����
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        // ������Ű�� ��Ű�� ����̴�

        // ������Ű�� x��ǥ�̰� hor
        // ��Ű�� y��ǥ�̰� ver

        // ������� ����3������ ������ �̵���Ŵ
        game.transform.position = curPos + nextPos;

        AttackStick.transform.rotation = Quaternion.Euler(vector3attck);

        #endregion




        #region �ִϸ����� �� ����

        // ���� ��ư�� ���������� ���� �ִϸ��̼��� ����
        Animator.SetBool("hide", hide);

        // �Է¹��� Ű���尪���� ������ �ִϸ��̼��� ������
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



    #region Ŀ���� public
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



    #region �ڷ�ƾ

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
