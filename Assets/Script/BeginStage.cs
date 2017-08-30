using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginStage : MonoBehaviour {

    StageManager sm;
    Basic_GameManager b_gm;
    public int numberOfStage;
    GameObject GameManager;
    GameObject mesin;
    MesinAI mAI;

    private void Start()
    {
        GameManager = GameObject.Find("gameManager").gameObject;
        b_gm = GameManager.GetComponent<Basic_GameManager>();
        mesin = gameObject.transform.parent.gameObject;
        mAI = mesin.GetComponent<MesinAI>();
        sm = GameObject.Find("StageManager").gameObject.GetComponent<StageManager>();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (b_gm.CreepDefeated || numberOfStage == 1) {
            if (other.tag == "Player")
            {
                mAI.beginShoot(numberOfStage);
                if (sm.beginStage(numberOfStage))
                {
                    Destroy(gameObject);
                }
            }
        }
        else if(numberOfStage==3)
        {
            if (other.tag == "Player")
            {
                mAI.beginShoot(numberOfStage);
                Destroy(gameObject);
            }
        }
    }

    /*
    public void beginMesin()
    {
        if (b_gm.CreepDefeated || numberOfStage == 1)
        {
                mAI.beginShoot(numberOfStage);
                if (sm.beginStage(numberOfStage))
                {
                    Destroy(gameObject);
                }
        }
        else if (numberOfStage == 3)
        {
                mAI.beginShoot(numberOfStage);
                Destroy(gameObject);
        }
    }
    */

}
