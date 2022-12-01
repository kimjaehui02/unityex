using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUi : MonoBehaviour
{


    #region value
    /// <summary>
    /// ui박스들을 가지고 있는 부모 오브젝트
    /// </summary>
    public GameObject itemBoxUi;





    /// <summary>
    /// 체력바를 나타내는 ui오브젝트의 렉트트랜스폼 컴포넌트
    /// </summary>
    public RectTransform Hpbar;

    /// <summary>
    /// 기술포인트를 나타내는 ui오브젝트의 랙트트랜스폼 컴포넌트
    /// </summary>
    public RectTransform Spbar;


    #region 아이템 바 관련들
    /// <summary>
    /// 사용형 아이템 오브젝트중 선택된게 어딘지 알려주기 위한 컬러
    /// </summary>
    public Image[] colors;

    /// <summary>
    /// 사용형 아이템 ui오브젝트의 이미지 컴포넌트 배열
    /// </summary>
    public Image[] box;

    /// <summary>
    /// 패시브 아이템 ui오브젝트의 이미지 컴포넌트
    /// </summary>
    public Image passivebox;



    #endregion

    #endregion

    #region default    


    // Update is called once per frame
    void Update()
    {
        UiboxUpdate();
        HpUpdate();
    }
    #endregion

    /// <summary>
    /// ui박스를 업데이트 하는 함수
    /// </summary>
    public void UiboxUpdate()
    {
        for(int i =0; i < 3; i++)
        {


            box[i].sprite = GameManager.instance.ActiveSprites[GameManager.instance.Activeint[i]];
        }

        passivebox.sprite = GameManager.instance.PassiveSprites[GameManager.instance.Passiveint];

    }

    public void HpUpdate()
    {
        Vector2 vector2 = new Vector2(350 * (GameManager.instance.Hp / GameManager.instance.MaxHp), 60);
        Hpbar.sizeDelta = vector2;

        Vector2 vector22 = new Vector2(300 * (GameManager.instance.Sp / GameManager.instance.MaxSp), 15);
        Spbar.sizeDelta = vector22;
    }

    public void HpChange(float input)
    {
        GameManager.instance.Hp += input;

    }

    public void ItemChange(ItemManager itemManager)
    {
        if(itemManager == null)
        {
            return;
        }

        if(itemManager.passiveItem)
        {
            int index = GameManager.instance.Passiveint;
            GameManager.instance.Passiveint = itemManager.itemNumber;
            itemManager.itemNumber = index;
        }
        else
        {
            bool empty = false;
            int emptyint = -1;

            for (int i = 0; i < 3; i++)
            {
                if (GameManager.instance.Activeint[i] == 0)
                {
                    empty = true;
                    emptyint = i;
                    break;
                }
            }

            //Debug.Log(emptyint);


            if(empty == true && GameManager.instance.Activeint[GameManager.instance.TargetItem] != 0)
            {
                GameManager.instance.Activeint[emptyint] = itemManager.itemNumber;
                Destroy(itemManager.gameObject);
            }
            else
            {
                int index = GameManager.instance.Activeint[GameManager.instance.TargetItem];
                GameManager.instance.Activeint[GameManager.instance.TargetItem] = itemManager.itemNumber;
                itemManager.itemNumber = index;
            }



        }

        if(itemManager != null)
        {
            itemManager.SpriteChange();
        }

    }
}
