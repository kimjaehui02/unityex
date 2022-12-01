using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemcolider : MonoBehaviour
{
    /// <summary>
    /// 범위 내의 게임 오브젝트들
    /// </summary>
    public List<GameObject> gameObjects;

    public GameObject sqrMagnitudeObj;

    private void Update()
    {
        Check();
    }

    public void Check()
    {
        if(gameObjects.Count == 0)// || sqrMagnitudeObj == null)
        {

            return;
        }

        if (gameObjects.Count == 1)
        {
            sqrMagnitudeObj = gameObjects[0];
            sqrMagnitudeObj.GetComponent<ItemManager>().Lighted();
            return;
        }




        for (int i =0; i < gameObjects.Count; i++)
        {
            Vector3 vector3 = gameObject.transform.position - gameObjects[i].transform.position;
            float sqrlen = vector3.sqrMagnitude;
            if(sqrMagnitudeObj == null)
            {
                sqrMagnitudeObj = gameObjects[0];
            }
            Vector3 vector3sqr = gameObject.transform.position - sqrMagnitudeObj.transform.position;
            float sqrlensqr = vector3sqr.sqrMagnitude;

            if(sqrlen < sqrlensqr)
            {
                sqrMagnitudeObj.GetComponent<ItemManager>().Darked();
                sqrMagnitudeObj = gameObjects[i];
            }
        }

        sqrMagnitudeObj.GetComponent<ItemManager>().Lighted();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Item")
        {
            gameObjects.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Item")
        {
            gameObjects.Remove(collision.gameObject);

            collision.GetComponent<ItemManager>().Darked();
            if(sqrMagnitudeObj == collision)
            {
                sqrMagnitudeObj = null;
            }
        }

    }
}
