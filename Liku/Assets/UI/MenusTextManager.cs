using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenusTextManager : MonoBehaviour
{

    /// <summary>
    /// 효과음입니다
    /// </summary>
    public AudioSource AudioSource;


    /// <summary>
    /// 확대해야하는지 아닌지 여부를 따질떄 씁니다
    /// </summary>
    private bool BigBool;

    /// <summary>
    /// 트윈들을 관리하는 함수입니다
    /// </summary>
    private List<Tween> tweens;

    private void Awake()
    {
        tweens = new List<Tween>();
        // 박스 콜라이더가 없다면 박스 콜라이더를 추가합니다
        if (GetComponent<BoxCollider2D>() == null || GetComponent<CircleCollider2D>() == null)
        {
            gameObject.AddComponent<BoxCollider2D>();
        }
        // 콜라이더크기를 조절합니다
        Colliders();

        // 오디오 소스가 없다면 만듭니다
        if(GetComponent<AudioSource>() == null)
        {
            AudioSource = gameObject.AddComponent<AudioSource>();
            AudioSource.playOnAwake = false;

        }


    }

    private void Start()
    {
        if (GameManager.G_M.AudioClip != null)
        {
            gameObject.GetComponent<AudioSource>().clip = GameManager.G_M.AudioClip;
            gameObject.GetComponent<AudioSource>().pitch = 3;
        }
    }

    private void Update()
    {
        Scales();

        // 오디오가 있다면 실시합니다
        if(GetComponent<AudioSource>() != null)
        {
            GetComponent<AudioSource>().volume = (0.3f * GameManager.G_M.MainSound);
        }



    }

    private void OnMouseOver()
    {
        BigBool = true;
    }

    /// <summary>
    /// 씬이 교체되어 사라질때 처리합니다
    /// </summary>
    private void OnDestroy()
    {
        for (int i = 0; i < tweens.Count; i++)
        {
            tweens[i].Kill();
        }
    }

    private void OnMouseEnter()
    {

        AudioSource.Play();
        tweens.Add(GetComponent<RectTransform>().DOScale(1.2f, 0.5f));
    }

    private void OnMouseExit()
    {
        tweens.Add(GetComponent<RectTransform>().DOScale(1, 0.5f));
    }

    /// <summary>
    /// 스케일 조절하는 함수입니다
    /// </summary>
    private void Scales()
    {
        if (BigBool == true)
        {
            tweens.Add(GetComponent<RectTransform>().DOScale(1.2f, 0.5f));
        }
        else
        {
            //tweens.Add(GetComponent<RectTransform>().DOScale(1, 0.5f));
        }

        BigBool = false;
    }

    /// <summary>
    /// 충돌 크기를 관리해주는 함수입니다
    /// </summary>
    private void Colliders()
    {
        if(GetComponent<BoxCollider2D>() == null)
        {
            return;
        }

        GetComponent<BoxCollider2D>().size = new Vector2(GetComponent<RectTransform>().rect.width, GetComponent<RectTransform>().rect.height);
    }

}
