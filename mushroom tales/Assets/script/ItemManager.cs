using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject itemSprite;
    public SpriteRenderer itemColor;



    /// <summary>
    /// 아이템이 떠다니는듯한 느낌을 주기위해 조절하는 플로트값
    /// </summary>
    public float floating;
    public bool floatingUp;



    public float lightFloat;
    public bool lightUP;
    public bool lightBool;

    public bool passiveItem;
    public int itemNumber;

    private void Start()
    {
        SpriteChange();
        //itemColor.sprite = Sprite[itemNumber];
    }


    private void Update()
    {
        ItemFloat();
    }
    
    
    private void ItemFloat()
    {
        if(floatingUp == true)
        {
            floating += Time.deltaTime;
        }
        else
        {
            floating -= Time.deltaTime;
        }

        
        if(floating > 0.2f)
        {
            floatingUp = false;
        }
        
        if(floating < -0.2f)
        {
            floatingUp = true;
        }
        
        itemSprite.transform.localPosition = new Vector3(0, floating*0.2f, 0);

    }


    public void Lighted()
    {
        if(lightBool == true)
        {
            return;
        }
        lightBool = true;
        StartCoroutine(Lighting());
    }

    public void Darked()
    {
        lightBool = false;
    }

    public void SpriteChange()
    {
        if (itemNumber ==0)
        {
            Destroy(gameObject);
        }


        if (passiveItem)
        {
            itemColor.sprite = GameManager.instance.PassiveSprites[itemNumber];
        }
        else
        {
            itemColor.sprite = GameManager.instance.ActiveSprites[itemNumber];
        }
    }

    #region 코루틴

    IEnumerator Lighting()
    {

        while(true)
        {
            yield return null;
            if (lightBool == false)
            {
                lightFloat = 100f;
                itemColor.color = new Color(100 / 255f, 100 / 255f, 100 / 255f, 255 / 255f);
                break;
            }

            if(lightUP == true)
            {
                lightFloat += 300 * Time.deltaTime;
            }
            else
            {
                lightFloat -= 300 * Time.deltaTime;
            }


            if(lightFloat > 255)
            {
                lightUP = false;
            }

            if(lightFloat < 100)
            {
                lightUP = true;
            }

            itemColor.color = new Color(lightFloat / 255f, lightFloat / 255f, lightFloat / 255f, 255 / 255f);
        }


    }


    #endregion
}

