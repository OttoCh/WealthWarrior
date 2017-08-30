using UnityEngine;
using System.Collections;

public class TowerAI : MonoBehaviour {

    GameObject Player;
    public GameObject Tower;
    public GameObject bullet;
    public float shootDelay;
    public int dirrection;
    public float speed;
    public float towerDmg;
    GameObject[] Arrow = new GameObject[4];

    public float towerHealth;
    float maxHealth = new float();
    float maxHealthScale = new float();
    GameObject tower;
    public GameObject healthBar;

    // Use this for initialization
    void Start () {
        tower = gameObject.transform.Find("Tower").gameObject;
        healthBar = gameObject.transform.Find("HP kartu").gameObject;
        maxHealth = towerHealth;
        maxHealthScale = healthBar.transform.localScale.y;
        for (int i = 0; i < 4; i++)
        {
            string childName = "Arrow" + (i + 1).ToString();
            Arrow[i] = gameObject.transform.Find(childName).gameObject;
            Arrow[i].SetActive(false);
        }
        Arrow[0].SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, gameObject.transform.position, Quaternion.identity) as GameObject;
        Rigidbody2D rb = new Rigidbody2D();
        rb = newBullet.GetComponent<Rigidbody2D>();
        Vector2 vel = new Vector2(0,0);
        if (dirrection==0)
        {
            vel.x = -1 * speed * Time.deltaTime * Mathf.Cos(0.48869f);
            vel.y = -1 * speed * Time.deltaTime * Mathf.Sin(0.48869f);
        }
        else if(dirrection==1)
        {
            vel.x = -1 * speed * Time.deltaTime * Mathf.Cos(0.48869f);
            vel.y = 1 * speed * Time.deltaTime * Mathf.Sin(0.48869f);
        }
        else if(dirrection==2)
        {
            vel.x = 1 * speed * Time.deltaTime * Mathf.Cos(0.48869f);
            vel.y = 1 * speed * Time.deltaTime * Mathf.Sin(0.48869f);
        }
        else if(dirrection==3)
        {
            vel.x = 1 * speed * Time.deltaTime * Mathf.Cos(0.48869f);
            vel.y = -1 * speed * Time.deltaTime * Mathf.Sin(0.48869f);
        }
        rb.velocity = vel;
    }

    IEnumerator StartShoot()
    {
        while(true)
        {
            yield return new WaitForSeconds(shootDelay);
            Shoot();
        }
    }

    public void changeDirrection()
    {
        dirrection += 1;
        if(dirrection>3)
        {
            dirrection = 0;
        }
        for(int i=0; i<4; i++)
        {
            Arrow[i].SetActive(false);
        }
        Arrow[dirrection].SetActive(true);
    }

    public void changeAnim()
    {
        Animator towerAnim = gameObject.transform.Find("Tower").GetComponent<Animator>();
        towerAnim.SetTrigger("In");
        StartCoroutine(StartShoot());
        for(int i=0; i<4; i++)
        {
            Arrow[i].SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BulletEnemy")
        {
            //Destroy(collision);
            reduceTowerHP(towerDmg);
        }
    }

    void reduceTowerHP(float damage)
    {
        towerHealth -= damage;
        AssignHealthBar();
        if (towerHealth <= 0)
        {
            Destroy(gameObject);
        }
    }


    void AssignHealthBar()
    {
        float newScale = maxHealthScale * (towerHealth / maxHealth);
        healthBar.transform.localScale = new Vector2(healthBar.transform.localScale.x, newScale);
    }

}
