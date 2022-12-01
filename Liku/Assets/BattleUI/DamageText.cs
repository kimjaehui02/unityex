using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    /// <summary>
    /// 자신의 리지드바디입니다
    /// </summary>
    public Rigidbody2D Get2D;

    public string sortingLayerName;
    public int sortingOrder;

    private void Awake()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        mesh.sortingLayerName = sortingLayerName;
        mesh.sortingOrder = sortingOrder;
    }

    /// <summary>
    /// 재생성시 작동하는것 입니다
    /// </summary>
    public void Starting()
    {
        Get2D.velocity = Vector2.zero;

        Get2D.AddForce(new Vector3 (40* Random.Range(-3f, 3f)
            , 2*180* Random.Range(0.8f, 1.2f)));

    }

    // 크기가 일정 위치를 벗어나면 비활성화됩니다
    private void Update()
    {
        if(transform.position.y < -5)
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 자신의 불값에 따라서 다른 색상이 나옵니다
    /// </summary>
    public void Startingtext(float HPPlus, bool greeen = false)
    {

        // 생성된 텍스트의 텍스트를 조정합니다
        gameObject.GetComponent<TextMesh>().text = HPPlus.ToString();


        if (greeen == true)
        {

            // 생성된 텍스트의 색상을 조정합니다
            gameObject.GetComponent<TextMesh>().color = new Color(100 / 255f, 255 / 255f, 100 / 255f);

        }

        gameObject.GetComponent<DamageText>().Starting();

    }

}
