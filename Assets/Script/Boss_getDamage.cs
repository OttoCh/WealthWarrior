using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_getDamage : MonoBehaviour {

    Boss_AI bAI;
    public GameObject boss;

	// Use this for initialization
	void Start () {
		bAI = boss.GetComponent<Boss_AI>();
    }

    public void getDamage(int dmg)
    {
        bAI.getDamage(dmg);
    }
}
