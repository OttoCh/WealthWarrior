using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_AI : MonoBehaviour {

    Rigidbody2D Body;
    Animator Boss_Animator;
    public GameObject Player;
    public GameObject gameManager;
    private bool facingRight = false;
    public bool enableMove = true;

    public GameObject Coll_Box;
    public GameObject Attack_Box;
    public int Health;
    public int maxHealth;
    public int dmg;
    public float rad;
    public float moveForce = 100.0f;
    public float prizeMoney;

    //variabel untuk simple enemy ai
    public float targetDistance;
    public float attackDistance;
    public float actionTresholdProbability;
    public float landingTresholdProbability;
    public float waitAfterAttack;
    public float laserAttackDuration;
    public float waitUntilBossFly;
    public float landingDuration;
    public bool fly = false;
    public bool alreadyCallCommand;

    // Use this for initialization
    void Start () {
        Body = gameObject.GetComponent<Rigidbody2D>();
        Boss_Animator = gameObject.transform.Find("Boss").gameObject.GetComponent<Animator>();
        Health = maxHealth;
        Player = GameObject.Find("Player").gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        if (enableMove)
        {
            if (!alreadyCallCommand)
            {
                float randomNumber = Random.Range(0.0f, 1.0f);
                //Debug.Log(randomNumber);
                if (randomNumber < actionTresholdProbability)
                {
                    alreadyCallCommand = true;
                    chaseAndAttackPlayer();
                }
                else if (randomNumber > landingTresholdProbability)
                {
                    if (fly)
                    {
                        alreadyCallCommand = true;
                        goDown();
                    }
                }
                MoveManager();
            }
        }
    }

    void goDown()
    {
        changeAnimCond(4);
        changeAnimCond(0);
        Vector2 zero = new Vector2(0, 0);
        Body.velocity = zero;
        enableMove = false;
        fly = false;
        StartCoroutine(getDownandWait());
    }

    IEnumerator getDownandWait()
    {
        yield return new WaitForSeconds(landingDuration);
        changeAnimCond(3);
        yield return new WaitForSeconds(1.0f);
        fly = true;
        alreadyCallCommand = false;
        enableMove = true;
    }

    void chaseAndAttackPlayer()
    {
        if (fly)
        {
            targetDistance = Vector2.Distance(gameObject.transform.position, Player.transform.position);
            if (targetDistance > attackDistance)
            {
                //bergerak untuk mendekati player
                Vector2 playerPos = Player.transform.position;
                beginMove(playerPos);
            }
            if (targetDistance < attackDistance)
            {
                beginAttack_Boss();
            }
        }
        else if(!fly)
        {
            changeAnimCond(3);
            enableMove = false;
            StartCoroutine(waitUntilFly());
        }
    }

    IEnumerator waitUntilFly()
    {
        yield return new WaitForSeconds(waitUntilBossFly);
        fly = true;
        alreadyCallCommand = false;
        enableMove = true;
    }

    void beginMove(Vector2 destCoor)
    {
        fly = true;
        changeAnimCond(3);
        //StartCoroutine(waitUntilFly());
        //buat grafiknya dahulu
        Vector2 initCoor = gameObject.transform.position;
        //Rigidbody2D playerBody = Player.GetComponent<Rigidbody2D>()

        Vector2 deltaMovement = new Vector2((destCoor.x - initCoor.x), (destCoor.y - initCoor.y));
        float radianOfMovement = Mathf.Atan(deltaMovement.y / deltaMovement.x);

        float x_movementSpeed = moveForce * Time.deltaTime * Mathf.Cos(radianOfMovement);
        float y_movementSpeed = moveForce * Time.deltaTime * Mathf.Sin(radianOfMovement);
        if (deltaMovement.x < 0)
        {
            x_movementSpeed *= -1;
        }
        if (deltaMovement.y < 0)
        {
            if (y_movementSpeed > 0) y_movementSpeed *= -1;
        }
        else if (deltaMovement.y > 0)
        {
            if (y_movementSpeed < 0) y_movementSpeed *= -1;
        }
        float deltaStraightMove = Mathf.Sqrt(deltaMovement.x * deltaMovement.x + deltaMovement.y * deltaMovement.y);
        float walkTime = deltaStraightMove / (moveForce * Time.deltaTime);
        Vector2 movementSpeed = new Vector2(x_movementSpeed, y_movementSpeed);
        Body.velocity = movementSpeed;
        MoveManager();
        alreadyCallCommand = false;
    }

    void beginAttack_Boss()
    {
        Vector2 attackPos = gameObject.transform.TransformPoint(Attack_Box.transform.localPosition);
        Vector2 Pos = attackPos;
        Collider2D[] hitCollider = Physics2D.OverlapCircleAll(Pos, rad);
        if (hitCollider != null || hitCollider.Length != 0)
        {
            for (int i = 0; i < hitCollider.Length; i++)
            {

                if (hitCollider[i].tag == "Player")
                {
                    hitCollider[i].SendMessage("getDamage", dmg);
                }
            }
        }
        changeAnimCond(1);
        alreadyCallCommand = false;
    }

    public void getDamage(int getDamage)
    {
        Health -= getDamage;
        if (Health <= 0)
        {
            Basic_GameManager b_gm = gameManager.GetComponent<Basic_GameManager>();
            b_gm.bossDefeated();
            b_gm.givePlayerMoney(prizeMoney);
            Destroy(gameObject);
        }
        changeAnimCond(2);
    }

    void changeAnimCond(int cond)
    {
        if (cond == 0)
        {
            //Idle
            Boss_Animator.SetTrigger("Land");
        }
        else if (cond == 1)
        {
            //Attack
            Boss_Animator.SetTrigger("Attack");
        }
        else if (cond == 2)
        {
            //Attacked
            Boss_Animator.SetTrigger("Attacked");
        }
        else if(cond == 3)
        {
            Boss_Animator.SetTrigger("Fly");
        }
        else if(cond == 4)
        {
            Boss_Animator.ResetTrigger("Attack");
            Boss_Animator.ResetTrigger("Fly");
        }
    }

    void MoveManager()
    {
        //ganti arah animasi
        if (Body.velocity.x != 0 || Body.velocity.y != 0)
        {
            if (Body.velocity.x < 0)
            {
                if (facingRight) flip();
            }
            else if (Body.velocity.x > 0)
            {
                if (!facingRight) flip();
            }
        }
    }

    void flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.lossyScale;
        theScale.x *= -1;
        theScale.z = 1; //ini ada agar memastikan nilai z player tidak jadi 0, sehingga malah hilang dari pandangan
        transform.localScale = theScale;
    }
}
