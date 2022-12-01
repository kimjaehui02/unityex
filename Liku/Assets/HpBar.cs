using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HpBar : MonoBehaviour
{
    /// <summary>
    /// 자신의 스크롤바입니다
    /// </summary>
    public Scrollbar Scrollbar;

    /// <summary>
    /// 체력바입니다
    /// </summary>
    public GameObject BAr;

    /// <summary>
    /// 체력의 수치입니다
    /// </summary>
    public Text GetText;

    /// <summary>
    /// 색상을 지정합니다
    /// </summary>
    public bool RED;

    /// <summary>
    /// 트윈들의 묶음입니다
    /// </summary>
    private List<Tween> GetTweens;

    /// <summary>
    /// 불이 꺼진 상태입니다 색상변환이 되지 않습니다
    /// </summary>
    private bool fadeOut;

    private void Awake()
    {
        GetTweens = new List<Tween>();
    }

    private void Update()
    {

        ColorControll();

    }

    /// <summary>
    /// 체력바의 색상을 조절합니다
    /// </summary>
    private void ColorControll()
    {
        // 불이 꺼진 동안은 색상 변화가 일어나지 않습니다
        if(fadeOut == true)
        {
            return;
        }

        float PLUSH = 50;
        if (Scrollbar.size == 1)
        {
            PLUSH = 0;
        }

        float test = (80 - PLUSH) / 255f;
        Color color = new Color((-105 * Scrollbar.size + 240) / 255f
                               , test
                               , test);

        if (RED == false)
        {
            color = new Color((-130 * Scrollbar.size + 170) / 255f
                                   , (130 * Scrollbar.size + 30) / 255f
                                   , test);

        }


        BAr.GetComponent<Image>().color = color;
    }

    /// <summary>
    /// 체력수치를 숫자로 보여줍니다
    /// </summary>
    public void Hptext(float HP, float MaxHP)
    {
        GetText.text = (HP + " / " + MaxHP);
    }

    /// <summary>
    /// 체력바가 사라집니다
    /// </summary>
    public void FadeOut(float time = 0.6f)
    {
        // 보이는것들을 안보이게 합니다
        GetComponent<Image>().DOFade(0, time);
        BAr.GetComponentInChildren<Image>().GetComponentInChildren<Image>().DOFade(0, time);
        GetText.DOFade(0, time);

        // 페이드아웃이라고 알립니다
        fadeOut = true;

    }

    /// <summary>
    /// 체력바가 나타납니다
    /// </summary>
    public void FadeIN(float time = 0.6f)
    {
        // 보이는것들을 보이게 합니다
        GetComponent<Image>().DOFade(1, time);
        BAr.GetComponentInChildren<Image>().GetComponentInChildren<Image>().DOFade(1, time);
        GetText.DOFade(1, time);

        // 페이드인이라고 알립니다
        fadeOut = false;
    }

    private void OnDestroy()
    {
        for(int i =0; i < GetTweens.Count; i++)
        {
            GetTweens[i].Kill();
        }
    }

}
