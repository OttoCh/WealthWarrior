using UnityEngine;
using System.Collections;

public class MesinAI : MonoBehaviour {

    public float health;
    float maxHealth;
    float maxHealthScale;
    bool notDestroyedYet = true;

    public GameObject bullet;
    public float shootDelay;
    public float speed;
    public float waitShootAnim;

    GameObject mesin;
    GameObject healthBar;
    public GameObject SM;
    Animator mesin_Anim = new Animator();

    // Use this for initialization
    void Start () {
        mesin = gameObject.transform.Find("theMesin").gameObject;
        mesin_Anim = mesin.GetComponent<Animator>();
        healthBar = gameObject.transform.Find("HP mesin").gameObject;
        maxHealth = health;
        maxHealthScale = healthBar.transform.localScale.y;
    }

    public void beginShoot(int currentStage)
    {
        if(currentStage!=1)
        {
            StartCoroutine(StartShoot());
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (notDestroyedYet)
        {
            if (other.tag == "Bullet")
            {
                health -= 10;
                AssignHealthBar();
                if (health <= 0.1f)
                {
                    destroyMesin();
                    notDestroyedYet = false;
                }
            }
        }
    }

    void destroyMesin()
    {
        mesin_Anim.SetTrigger("Out");
        StartCoroutine(waitIt());
    }

    IEnumerator waitIt()
    {
        yield return new WaitForSeconds(2.0f);
        BeginDestroySequence();
        Destroy(gameObject);
    }

    void waitAnim()
    {
        var currentAnim = mesin_Anim.GetCurrentAnimatorStateInfo(0);
        while (currentAnim.normalizedTime < 1.0f) ;
        if (currentAnim.normalizedTime >= 1.0f)  //ini cek apakah animasi sudah pernah looping dengan nilai desimal adalah jumlah loop dan floatnya adlaah proses animasi yg baru lagi
                                                 //artinya begitu animasi yg skrg sudah dijalankan 1 kali maka lgsg masuk ke sini
        {
            BeginDestroySequence();
            Destroy(gameObject);
        }
    }

    void AssignHealthBar()
    {
        float newScale = maxHealthScale * (health / maxHealth);
        healthBar.transform.localScale = new Vector2(healthBar.transform.localScale.x, newScale);
    }

    void BeginDestroySequence()
    {
        StageManager sm = SM.GetComponent<StageManager>();
        sm.beginDestroy();
    }

    void Shoot(int dirrection)
    {
        GameObject newBullet = Instantiate(bullet, gameObject.transform.position, Quaternion.identity) as GameObject;
        Rigidbody2D rb = new Rigidbody2D();
        rb = newBullet.GetComponent<Rigidbody2D>();
        Vector2 vel = new Vector2(0, 0);
        if (dirrection == 0)
        {
            vel.x = -1 * speed * Time.deltaTime * Mathf.Cos(0.48869f);
            vel.y = -1 * speed * Time.deltaTime * Mathf.Sin(0.48869f);
        }
        else if (dirrection == 1)
        {
            vel.x = -1 * speed * Time.deltaTime * Mathf.Cos(0.48869f);
            vel.y = 1 * speed * Time.deltaTime * Mathf.Sin(0.48869f);
        }
        else if (dirrection == 2)
        {
            vel.x = 1 * speed * Time.deltaTime * Mathf.Cos(0.48869f);
            vel.y = 1 * speed * Time.deltaTime * Mathf.Sin(0.48869f);
        }
        else if (dirrection == 3)
        {
            vel.x = 1 * speed * Time.deltaTime * Mathf.Cos(0.48869f);
            vel.y = -1 * speed * Time.deltaTime * Mathf.Sin(0.48869f);
        }
        rb.velocity = vel;
    }

    IEnumerator StartShoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootDelay);
            mesin_Anim.SetTrigger("Attack");
            yield return new WaitForSeconds(waitShootAnim);
            Shoot(0);
            Shoot(1);
            Shoot(2);
            Shoot(3);
        }
    }
}
