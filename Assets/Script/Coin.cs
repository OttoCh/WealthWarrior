using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

    GameObject gameManager;
    public int coin_val;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("gameManager");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //call function on game manager and destroy this
            gameManager.SendMessage("GetCoin", coin_val);
            //other.SendMessage("GetCoin");
            Destroy(gameObject);
        }
    }
}
