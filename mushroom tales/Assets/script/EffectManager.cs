using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    #region 오브젝트

    public List<GameObject> gameObjects;

    #endregion

    public void fire(GameObject target, float input = 5f)
    {
        var Effect = Instantiate(gameObjects[0]).transform;
        Effect.parent = target.transform;
        Effect.localPosition = new Vector3(0,2,0);
        //target.transform.setc
        StartCoroutine(FireEffect(target, input, Effect, input));

    }

    #region 코루틴

    //yield return new WaitForSecondsRealtime(0.25f);
    IEnumerator FireEffect(GameObject target, float input, Transform Effect, float maxtime)
    {
        target.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255 / 255f, 150 / 255f, 150 / 255f);
        if(input > 0.25f)
        {
            input -= 0.25f;
            yield return new WaitForSeconds(0.25f);
            Debug.Log(input);
            Effect.transform.GetChild(0).localScale = new Vector3(input/maxtime,1, 0);
            StartCoroutine(FireEffect(target, input, Effect, maxtime));
        }
        else
        {
            yield return new WaitForSeconds(input);
            target.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
            Destroy(Effect.gameObject);
        }
        

    }



        #endregion
}
