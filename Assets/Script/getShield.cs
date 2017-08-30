using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getShield : MonoBehaviour {

    public GameObject getShield_Dialog;
    private GameObject player;
    Player_Move pm;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            getShield_Dialog.SetActive(true);
            player = collision.gameObject;
            pm = player.GetComponent<Player_Move>();
            pm.EnableMove = false;
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector3 ( 0, 0, 0 );
            StartCoroutine(waitAndDisappear());
        }
    }

    IEnumerator waitAndDisappear()
    {
        yield return new WaitForSeconds(2.0f);
        pm.EnableMove = true;
        Destroy(getShield_Dialog);
        Destroy(gameObject);
    }
}
