using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creepDetector : MonoBehaviour {

    public GameObject gm;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Basic_GameManager b_gm = gm.GetComponent<Basic_GameManager>();
            b_gm.beginCreepAttack();
            Destroy(gameObject);
        }
    }
}
