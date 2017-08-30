using UnityEngine;
using System.Collections;

public class Enemy_Battle : MonoBehaviour {

    public float Enemy_Health;
    public float Enemy_attackdamage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Attack(float dmg)
    {
        Enemy_Health -= dmg;
        if(Enemy_Health<=0)
        {
            //mati
            Destroy(gameObject);
        }
    }
}
