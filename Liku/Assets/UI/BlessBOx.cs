using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlessBOx : MonoBehaviour
{
    // 블레스의 프리펩과 블레스의 기능들을 담은 함수를 보관합니다
    #region 블레스 관련 선언



    /// <summary>
    /// 블레스 프리펩입니다 이걸 기반으로 블레스를 생성합니다
    /// </summary>
    public List<GameObject> BlessPrefab;

    #endregion

    #region 장비들 관련 선언


    /// <summary>
    /// 머리장비 프리펩입니다 이걸 기반으로 장비를 생성합니다
    /// </summary>
    public List<GameObject> EquipsPrefab1;

    /// <summary>
    /// 장비 프리펩입니다 이걸 기반으로 장비를 생성합니다
    /// </summary>
    public List<GameObject> EquipsPrefab2;

    /// <summary>
    /// 머리의 장착 부위입니다
    /// </summary>
    public List<GameObject> EquipsH;

    /// <summary>
    /// 무기의 장착 부위입니다
    /// </summary>
    public List<GameObject> EquipsW;

    #endregion

    private void Start()
    {
        RandBless(); RandBless();
        InstanceEquips1(0, 0);
        InstanceEquips2(0, 0);
    }


    #region Blesses

    /// <summary>
    /// 블레스 프리펩을 생성하는 함수입니다
    /// </summary>
    public void InstanceBless(int index = 0)
    {
        // 블레스를 생성하고 자식으로 만듭니다
        GameManager.G_M.
Blesses.Add(
            Instantiate(BlessPrefab[index], transform)
            );

        // 생성한 블레스의 위치를 겹치지 않게 조절합니다
        GameManager.G_M.
Blesses[GameManager.G_M.Blesses.Count - 1].GetComponent<RectTransform>().anchoredPosition =
            new Vector2(40 * GameManager.G_M.Blesses.Count - 40, 0);


    }

    /// <summary>
    /// 랜덤으로 블레스를 생성합니다
    /// </summary>
    public void RandBless()
    {
        // 랜덤으로 생성장비를 고릅니다
        int numb = Random.Range(0, BlessPrefab.Count - 1);

        // 장비를 생성하는 함수입니다
        InstanceBless(numb);
    }

    #endregion

    #region Equips


    /// <summary>
    /// 머리장비를 생성합니다
    /// </summary>
    /// <param name="pongnumb">장착할 퐁의 숫자입니다</param>
    /// <param name="index">장착할 장비의 번호입니다</param>
    public void InstanceEquips1(int pongnumb, int index = 0)
    {
        // 만약 이미 장비가 있다면 교체합니다
        if (EquipsH[pongnumb].transform.childCount == 1)
        {
            // 해당 차일드을 없애버립니다
            Destroy(EquipsH[pongnumb].transform.GetChild(0).gameObject);
        }

        // 게임매니저의 리스트에 장비를 추가시킵니다
        GameManager.G_M.Equips1[pongnumb] = (
        // 장비를 올바른 위치에 생성시킵니다
        Instantiate(EquipsPrefab1[index], EquipsH[pongnumb].transform)
        );

        GameManager.G_M.Equips1[pongnumb].GetComponent<SimpleEquip>().Equipint = pongnumb;

        switch (EquipsPrefab1[index].GetComponent<SimpleEquip>().Rare)
        {
            case 0:
                EquipsH[pongnumb].GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
                break;
            case 1:
                EquipsH[pongnumb].GetComponent<Image>().color = new Color(80 / 255f, 255 / 255f, 80 / 255f);
                break;
            case 2:
                EquipsH[pongnumb].GetComponent<Image>().color = new Color(255 / 255f, 80 / 255f, 80 / 255f);
                break;
        }

    }

    /// <summary>
    /// 무기장비를 생성합니다
    /// </summary>
    /// <param name="pongnumb">장착할 퐁의 숫자입니다</param>
    /// <param name="index">장착할 장비의 번호입니다</param>
    public void InstanceEquips2(int pongnumb, int index = 0)
    {
        // 만약 이미 장비가 있다면 교체합니다
        if (EquipsW[pongnumb].transform.childCount == 1)
        {
            // 해당 차일드을 없애버립니다
            Destroy(EquipsW[pongnumb].transform.GetChild(0).gameObject);
        }

        // 게임매니저의 리스트에 장비를 추가시킵니다
        GameManager.G_M.Equips2[pongnumb] = (
        // 장비를 올바른 위치에 생성시킵니다
        Instantiate(EquipsPrefab2[index], EquipsW[pongnumb].transform)
        );


        switch (EquipsPrefab2[index].GetComponent<SimpleEquip>().Rare)
        {
            case 0:
                EquipsW[pongnumb].GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
                break;
            case 1:
                EquipsW[pongnumb].GetComponent<Image>().color = new Color(80 / 255f, 255 / 255f, 80 / 255f);
                break;
            case 2:
                EquipsW[pongnumb].GetComponent<Image>().color = new Color(255 / 255f, 80 / 255f, 80 / 255f);
                break;
            default:
                break;
        }

    }

    /// <summary>
    /// 장비를 무작위로 생성합니다
    /// </summary>
    /// <param name="Pongnumb">장착할 퐁의 숫자입니다</param>
    /// <param name="Head">장착할 부위입니다</param>
    public void RanEquips(int Pongnumb, int Head)
    {

        // 장비를 무작위로 정의합니다
        int numb;

        // 만드는 장비의 타입을 달리합니다
        switch (Head)
        {
            case 0:        // 장비를 무작위로 정의합니다
                numb = Random.Range(0, EquipsPrefab1.Count - 1);
                InstanceEquips1(Pongnumb, numb);
                break;
            case 1:        // 장비를 무작위로 정의합니다
                numb = Random.Range(0, EquipsPrefab2.Count - 1);
                InstanceEquips2(Pongnumb, numb);
                break;

        }

        
    }

    /// <summary>
    /// 장착한 장비를 강화합니다
    /// </summary>
    public void ReinforceEqu(int Pongnumb, int Head)
    {



        switch (Head)
        {
            case 0:
                // 장비가 비어있다면 무효가 됩니다
                if (GameManager.G_M.Equips1[Pongnumb] == null)
                {
                    return;
                }

                // 대상 장비가 이미 최대레벨이라면 취소됩니다
                if (GameManager.G_M.Equips1[Pongnumb].GetComponent<SimpleEquip>().Rare == 2)
                {

                    return;
                }
                break;

            case 1:
                // 장비가 비어있다면 무효가 됩니다
                if (GameManager.G_M.Equips1[Pongnumb] == null)
                {

                    return;
                }

                // 대상 장비가 이미 최대레벨이라면 취소됩니다
                if (GameManager.G_M.Equips2[Pongnumb].GetComponent<SimpleEquip>().Rare == 2)
                {

                    return;
                }
                break;
        }

        // 장비를 생성합니다
        switch (Head)
        {
            case 0:
                InstanceEquips1(Pongnumb, GameManager.G_M.Equips1[Pongnumb].GetComponent<SimpleEquip>().Rare + 1);
                break;

            case 1:


                InstanceEquips2(Pongnumb, GameManager.G_M.Equips2[Pongnumb].GetComponent<SimpleEquip>().Rare + 1);
                break;
        }

    }

    #endregion

}
