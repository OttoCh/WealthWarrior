using UnityEngine;
using System.Collections;

public class BulletAI : MonoBehaviour {

    public int mode;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (mode == 1)
        {
            if (other.tag == "Boundary" || other.tag == "Enemy")
            {
                Destroy(gameObject);
            }
        }
        else if(mode == 2)
        {
            if (other.tag == "Boundary" || other.tag == "Tower")
            {
                Destroy(gameObject);
            }
        }
    }
}
