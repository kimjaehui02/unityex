using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Itemcolor : MonoBehaviour
{

    /// <summary>
    /// 내 아이템의 등급입니다
    /// </summary>
    public int myitem;

    /// <summary>
    /// 자신의 위치입니다
    /// </summary>
    public int selfe;

    /// <summary>
    /// 자신이 머리인지 아닌지입니다
    /// </summary>
    public bool Headtype;

    private void Awake()
    {
        selfe = GetComponentInParent<PongUI>().inputint-1;
    }

    // Update is called once per frame

    void Update()
    {
        if(transform.childCount == 0)
        {
            GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
        }
        else
        {
            myitem = transform.GetChild(0).GetComponent<SimpleEquip>().Rare;

            switch (myitem)
            {
                case 0:
                    GetComponent<Image>().color = new Color(0 / 255f, 0 / 255f, 0 / 255f);
                    break;
                case 1:
                    GetComponent<Image>().color = new Color(80 / 255f, 255 / 255f, 80 / 255f);
                    break;
                case 2:
                    GetComponent<Image>().color = new Color(255 / 255f, 80 / 255f, 80 / 255f);
                    break;
                default:
                    break;
            }

        }

        InputEquipGM();
    }

    /// <summary>
    /// 자신의 아이템을 게임매니저에 집어 넣습니다
    /// </summary>
    private void InputEquipGM()
    {
        // 자신에게 자식이 있을경우
        if(transform.childCount == 1)
        {
            // 머리일경우엔 머리에 넣습니다
            if(transform.GetChild(0).GetComponent<SimpleEquip>().ItemType2 == 0)
            {
                GameManager.G_M.Equips1[selfe] = transform.GetChild(0).gameObject;
            }
            // 아니면 무기에 넣습니다
            else
            {
                GameManager.G_M.Equips2[selfe] = transform.GetChild(0).gameObject;

            }
        }
        
        // 자신에게 자식이 없을경우
        if(transform.childCount == 0)
        {
            if(Headtype == true)
            {
                GameManager.G_M.Equips1[selfe] = null;
            }
            else
            {
                GameManager.G_M.Equips2[selfe] = null;
            }
        }
    }

}
