using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region �̱���
    /* // �̱��� //
 * instance��� ������ static���� ������ �Ͽ� �ٸ� ������Ʈ ���� ��ũ��Ʈ������ instance�� �ҷ��� �� �ְ� �մϴ� 
 */
    public static GameManager instance = null;

    private void singleAwake()
    {
        if (instance == null) //instance�� null. ��, �ý��ۻ� �����ϰ� ���� ������
        {
            instance = this; //���ڽ��� instance�� �־��ݴϴ�.
            DontDestroyOnLoad(gameObject); //OnLoad(���� �ε� �Ǿ�����) �ڽ��� �ı����� �ʰ� ����
        }
        else
        {
            if (instance != this) //instance�� ���� �ƴ϶�� �̹� instance�� �ϳ� �����ϰ� �ִٴ� �ǹ�
                Destroy(this.gameObject); //�� �̻� �����ϸ� �ȵǴ� ��ü�̴� ��� AWake�� �ڽ��� ����
        }
    }
    #endregion

    //[field: SerializeField]
    [SerializeField]
    public EffectManager EffectManager;

    private void Awake()
    {
        singleAwake();
        
    }


    #region �����͵�

    #region ü�¹� ���õ�
    /// <summary>
    /// �÷��̾��� �ִ� ü��
    /// </summary>
    [field: SerializeField]
    public float MaxHp { get; set; }
    /// <summary>
    /// �÷��̾��� ���� ü��
    /// </summary>
    [field: SerializeField] 
    public float Hp { get; set; }



    #endregion

    #region sp�� ���õ�
    /// <summary>
    /// �÷��̾��� �ִ� �������Ʈ
    /// </summary>
    [field: SerializeField] 
    public float MaxSp { get; set; }
    /// <summary>
    /// �÷��̾��� ���� �������Ʈ
    /// </summary>
    [field: SerializeField] 
    public float Sp { get; set; }


    #endregion

    #region ������â ���õ�
    /// <summary>
    /// ������ �� ����� ������ ��������Ʈ��
    /// </summary>
    [field: SerializeField]
    public Sprite[] ActiveSprites { get; set; }

    /// <summary>
    /// ������ �� ����� ������ ��������Ʈ��
    /// </summary>
    [field: SerializeField]
    public Sprite[] PassiveSprites { get; set; }

    /// <summary>
    /// ����� �����۵��� ��Ʈ������ �����س��� �迭
    /// </summary>
    [field: SerializeField]
    public int[] Activeint { get; set; }

    /// <summary>
    /// �нú� �������� ��Ʈ������ ������
    /// </summary>
    [field: SerializeField]
    public int Passiveint { get; set; }

    /// <summary>
    /// ���� ui���� ����Ű�� �ִ� ������
    /// </summary>
    [field: SerializeField]
    public int TargetItem { get; set; }
    #endregion



    #endregion




}
