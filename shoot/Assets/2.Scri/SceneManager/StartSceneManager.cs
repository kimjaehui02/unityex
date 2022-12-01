using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneManager : MonoBehaviour
{
    // 배경화면의 이동속도입니다.
    public float speed;


    // 배경화면을 배열로 담아두어 관리합니다.
    public Transform[] sprites;

    // 카메라의 크기를 입력받고 이를 토대로 배경을 순환시킵니다.
    float viewHeight;

    private void Awake()
    {
        // 카메라의 크기를 입력받습니다.
        viewHeight = Camera.main.orthographicSize * 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 배경들을 지속적으로 이동시키는 함수를 호출합니다.
        Move();

    }

    private void Move()
    {
        // 별들을 관리합니다.
        for (int index = 0; index < sprites.Length; index++)
        {
            // 별이 너무 내려오면 다시 올려보냅니다.
            if (sprites[index].transform.position.y <= -viewHeight)
            {
                // 별들을 카메라길이의 2배만큼 올려보냅니다.
                sprites[index].transform.position = new Vector3(0, 10, 0);
            }

            // 별이 아래로 내려오게 합니다.
            sprites[index].transform.position += Vector3.down * speed * Time.deltaTime;
            

        }

        //Vector3 curPos = transform.position;
        //Vector3 nextPos = Vector3.down * speed * Time.deltaTime;
        //transform.position = curPos + nextPos;



    }


}
