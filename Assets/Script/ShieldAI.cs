using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAI : MonoBehaviour {

    public float shieldHealth;
    public float shieldDmg;
    public GameObject healthBar;

    private float maxHealthScale;
    private float maxHealth;

	// Use this for initialization
	void Start () {
        maxHealth = shieldHealth;
        maxHealthScale = healthBar.transform.localScale.y;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "BulletEnemy")
        {
            //Destroy(collision);
            reduceShieldHP(shieldDmg);
        }
    }

    void reduceShieldHP(float damage)
    {
        shieldHealth -= damage;
        AssignHealthBar();
        if (shieldHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void AssignHealthBar()
    {
        float newScale = maxHealthScale * (shieldHealth / maxHealth);
        healthBar.transform.localScale = new Vector2(healthBar.transform.localScale.x, newScale);
    }
}
