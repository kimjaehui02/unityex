using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUi : MonoBehaviour
{


    #region value
    /// <summary>
    /// ui�ڽ����� ������ �ִ� �θ� ������Ʈ
    /// </summary>
    public GameObject itemBoxUi;





    /// <summary>
    /// ü�¹ٸ� ��Ÿ���� ui������Ʈ�� ��ƮƮ������ ������Ʈ
    /// </summary>
    public RectTransform Hpbar;

    /// <summary>
    /// �������Ʈ�� ��Ÿ���� ui������Ʈ�� ��ƮƮ������ ������Ʈ
    /// </summary>
    public RectTransform Spbar;


    #region ������ �� ���õ�
    /// <summary>
    /// ����� ������ ������Ʈ�� ���õȰ� ����� �˷��ֱ� ���� �÷�
    /// </summary>
    public Image[] colors;

    /// <summary>
    /// ����� ������ ui������Ʈ�� �̹��� ������Ʈ �迭
    /// </summary>
    public Image[] box;

    /// <summary>
    /// �нú� ������ ui������Ʈ�� �̹��� ������Ʈ
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
    /// ui�ڽ��� ������Ʈ �ϴ� �Լ�
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
