using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {

    public string BoxOpenAnimName;
    public GameObject coin;
    GameObject BoxOpenAnim;

    // Use this for initialization
    void Start () {
        BoxOpenAnim = transform.Find("MysBox").Find(BoxOpenAnimName).gameObject;
        BoxOpenAnim.SetActive(false);
    }

    public void Interract()
    {
        BoxOpenAnim.SetActive(true);
        for(int i=0; i<10; i++)
        {
            float randomXY = Random.Range(-0.6f, 0.6f);
            Vector3 instantiatePos = gameObject.transform.position;
            instantiatePos.x = instantiatePos.x + randomXY;
            instantiatePos.y = instantiatePos.y + 2*randomXY;
            GameObject newCoin = Instantiate(coin, instantiatePos, Quaternion.identity) as GameObject;
            if(randomXY > 0)
            {
                flip(newCoin);
            }
        }
        StartCoroutine(destroyAfterOpen());
    }

    IEnumerator destroyAfterOpen()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    void flip(GameObject theObj)
    {
        Vector3 theScale = transform.lossyScale;
        theScale.x *= -1;
        theScale.z = 1; 
        theObj.transform.localScale = theScale;
    }
}
