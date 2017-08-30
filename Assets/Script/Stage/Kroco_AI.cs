using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kroco_AI : MonoBehaviour {

    Rigidbody2D Body;
    Animator Creep_Animator;
    public GameObject Player;
    public GameObject gameManager;
    private bool facingRight = false;
    public bool enableMove = true;
    public bool enableMove_Attacked = true;
    public AudioSource AttackSound;

    public GameObject Coll_Box;
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
    public float waitAfterAttack;
    public float waitAfterAttacked;


    // Use this for initialization
    void Start () {
        Body = gameObject.GetComponent<Rigidbody2D>();
        Creep_Animator = gameObject.GetComponent<Animator>();
        Health = maxHealth;
        Coll_Box = gameObject.transform.Find("Coll_Box").gameObject;
        Player = GameObject.Find("Player").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if(enableMove && enableMove_Attacked)
        {
            float randomNumber = Random.Range(0.0f, 1.0f);
            if (randomNumber < actionTresholdProbability)
            {
                chaseAndAttackPlayer();
            }
            MoveManager();
        }

	}

    void chaseAndAttackPlayer()
    {
        targetDistance = Vector2.Distance(gameObject.transform.position, Player.transform.position);
        if (targetDistance > attackDistance)
        {
            //bergerak untuk mendekati player
            Vector2 playerPos = Player.transform.position;
            beginMove(playerPos);
        }
        if(targetDistance < attackDistance)
        {
            beginAttack();
        }
    }

    void beginMove(Vector2 destCoor)
    {
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
    }

    IEnumerator stopWalk(float walkTime)
    {
        yield return new WaitForSeconds(walkTime);
        Vector2 zero = new Vector2(0, 0);
        Body.velocity = zero;
        changeAnimCond(0);
    }

    void beginAttack()
    {
        Vector2 zero = new Vector2(0, 0);
        Body.velocity = zero;
        changeAnimCond(0);
        Vector2 CollBox_WorldCoor = gameObject.transform.TransformPoint(Coll_Box.transform.localPosition);
        Vector2 Pos = CollBox_WorldCoor;
        //Debug.Log(Pos);
        Collider2D[] hitCollider = Physics2D.OverlapCircleAll(Pos, rad);
        if (hitCollider != null || hitCollider.Length != 0)
        {
            for (int i = 0; i < hitCollider.Length; i++)
            {
                if (hitCollider[i].tag == "Player")
                {
                    enableMove = false;
                   hitCollider[i].SendMessage("getDamage", dmg);
                    changeAnimCond(2);
                    AttackSound.Play();
                    StartCoroutine(waitUntilAttackFinish());
                }
            }
        }
    }

    IEnumerator waitUntilAttackFinish()
    {
        yield return new WaitForSeconds(waitAfterAttack);
        enableMove = true;
    }

    IEnumerator waitUntilAttackedFinish()
    {
        yield return new WaitForSeconds(waitAfterAttacked);
        enableMove_Attacked = true;
    }

    public void getDamage(int getDamage)
    {
            StartCoroutine(stopWalk(0));
            enableMove_Attacked = false;
            Health -= getDamage;
            if (Health <= 0)
            {
                Basic_GameManager bm = gameManager.GetComponent<Basic_GameManager>();
                bm.creepDefeated();
                bm.givePlayerMoney(prizeMoney);
                Destroy(gameObject);
            }
            changeAnimCond(3);
            StartCoroutine(waitUntilAttackedFinish());
    }

    void changeAnimCond(int cond)
    {
        if(cond==0)
        {
            //Idle
            Creep_Animator.SetBool("Walk", false);
        }
        else if(cond == 1)
        {
             //Walk
            Creep_Animator.SetBool("Walk", true);
        }
        else if(cond == 2)
        {
            //Attack
            Creep_Animator.SetTrigger("Attack");
        }
        else if(cond == 3)
        {
            //Attacked
            Creep_Animator.SetTrigger("Attacked");
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
            changeAnimCond(1);
        }
    }

    void flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.lossyScale;
        Debug.Log(theScale);
        theScale.x *= -1;
        theScale.z = 1; //ini ada agar memastikan nilai z player tidak jadi 0, sehingga malah hilang dari pandangan
        transform.localScale = theScale;
    }
}
