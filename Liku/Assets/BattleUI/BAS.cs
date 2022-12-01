using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BAS : MonoBehaviour
{
    /// <summary>
    /// 성공했을떄 트루로 만들어줍니다
    /// </summary>
    public bool Contact;

    [SerializeField]
    public Vector3 vector3;

    /// <summary>
    /// 기본색상입니다
    /// </summary>
    [SerializeField]
    private Color nomal;

    /// <summary>
    /// 적중시 색상입니다.
    /// </summary>
    [SerializeField]
    private Color color1;

    private void Awake()
    {
        nomal = GetComponent<Image>().color;
        //gameObject.GetComponent<Image>().DOColor(color1, 0.4f);
    }

    private void Update()
    {
        //if(Contact == true)
        //{
        //    Contact = false;
        //    gameObject.SetActive(false);
        //}
        //

    }

    /// <summary>
    /// 성공적인 체크했을때 입니다
    /// </summary>
    public void Contacted()
    {
        gameObject.GetComponent<Image>().DOColor(color1, 0.4f);
        Contact = true;

    }

    /// <summary>
    /// 체크를 되돌립니다
    /// </summary>
    public void ReCont()
    {
        if(gameObject.activeSelf == true)
        {
            gameObject.GetComponent<Image>().DOColor(nomal, 0.4f);
            Contact = false;
        }


    }


}
