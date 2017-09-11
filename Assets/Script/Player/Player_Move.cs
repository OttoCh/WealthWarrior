using UnityEngine;
using System.Collections;
//using System.Collections.Generic;
//using System.Reflection;
using UnityStandardAssets.CrossPlatformInput;

public class Player_Move : Player_Schema {

    public Player_Schema.BodyPart bodypart;
    public Player_Schema.BodyAnimator bodyanimator = new Player_Schema.BodyAnimator();
    Basic_GameManager b_gm;
    GameObject charinfo_gameobj;
    CharacterInformation CharInfo;
    public GameObject gameManager;
    public GameObject cam;
    public GameObject Coll_Box;

    public float waitUntilGetDamageAgainTime;

    private bool facingRight = true;
    public bool EnableMove;
    public bool alreadyGetDamage = false;
    public float moveForce = 5;
    public float rad = 1.0f;
    public int dmg;
    Rigidbody2D myBody;

    private bool battleMode = false;

    void Awake()
    {
        //load infromation about skin
        //GameObject charinfo_gameobj = GameObject.Find("CharacterInfo").gameObject;
        charinfo_gameobj = GameObject.Find("CharacterInfo").gameObject;
        //Debug.Log((charinfo_gameobj.GetComponent<CharacterInformation>()).GetType());
        CharInfo = charinfo_gameobj.GetComponent<CharacterInformation>() as CharacterInformation;   //as chracterinformation dibutuhkan karena kalau tidak mengakibatkan crash. seperitnya kalau tidak ada itu yg di assign malah gameobjectnya
    }

    // Use this for initialization
    void Start () {
        bodypart = new Player_Schema.BodyPart(base.gameObject);
        //init all new variable
        myBody = base.gameObject.GetComponent<Rigidbody2D>();
        //myBody = cube.GetComponent<Rigidbody2D>();
        initCustomPlayerInformation();
        bodypart.Weapon.SetActive(false);
        if (gameManager != null)
        {
            b_gm = gameManager.GetComponent<Basic_GameManager>();
        }
    }

    void initCustomPlayerInformation()
    {
        //masalah crash taunya muncul disini dimana ntah kenapa malah null
        //Debug.Log(CharInfo.playerController_persist.Torso_Ctrl);
        if (CharInfo.playerController_persist.Torso_Ctrl != null)
        {
            bodyanimator.Torso_Animator.runtimeAnimatorController = CharInfo.playerController_persist.Torso_Ctrl;
            bodyanimator.FrontHair_Animator.runtimeAnimatorController = CharInfo.playerController_persist.FrontHair_Ctrl;
            bodyanimator.BackHair_Animator.runtimeAnimatorController = CharInfo.playerController_persist.BackHair_Ctrl;
            bodyanimator.Eye_Animator.runtimeAnimatorController = CharInfo.playerController_persist.Eye_Ctrl;
            bodyanimator.Weapon_Animator.runtimeAnimatorController = CharInfo.playerController_persist.Weapon_Ctrl;
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (EnableMove)
        {
            //Debug.Log(CrossPlatformInputManager.GetAxis("Horizontal"));
            Vector2 moveVec = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical")) * moveForce * Time.deltaTime;
            /*
            Vector2 moveVec = new Vector2();
            if (Input.GetKey(KeyCode.A))
            {
                moveVec.x = -1 * moveForce * Time.deltaTime;
                moveVec.y = 0;
            }
            else if(Input.GetKey(KeyCode.D))
            {
                moveVec.x = 1 * moveForce * Time.deltaTime;
                moveVec.y = 0;
            }
            else if(Input.GetKey(KeyCode.W))
            {
                moveVec.x = 0;
                moveVec.y = 1 * moveForce * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                moveVec.x = 0;
                moveVec.y = -1 * moveForce * Time.deltaTime;
            }
            */
            myBody.velocity = moveVec;
            //MoveManager();    //awalnya ini
            if (EnableMove == true) //HAPUS INI KALO CAMERA JADI KACAU
            {
                Vector3 camPos = base.gameObject.transform.position;
                camPos.y += 1;
                cam.transform.position = camPos;
            }
        }
        MoveManager();  //coba ganti sini agar bisa otomatis animasinya
        //ini nanti kembalikan ke awal
        /*
        del all di bawah ini
        */
        /*
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (!bodypart.Weapon.activeInHierarchy) bodypart.Weapon.SetActive(true);
            else if (bodypart.Weapon.activeInHierarchy) bodypart.Weapon.SetActive(false);
            if (!battleMode)
            {
                changeAnimCond(3);
                //changeAnimCond(Battle,1);
                battleMode = true;
            }
            else {
                changeAnimCond(0);
                //changeAnimCond(Napas,1);
                battleMode = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            //Attack
            changeAnimCond(4);
            //changeAnimCond(Attack,2);
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            //Attacked
            changeAnimCond(5);
            //changeAnimCond(Attacked,2);
        }
        */
        
	}

    void MoveManager()
    {
        //ganti arah animasi
        if (myBody.velocity.x != 0 || myBody.velocity.y != 0)
        {
            if (myBody.velocity.x < 0)
            {
                if (facingRight) flip();
            }
            else if (myBody.velocity.x > 0)
            {
                if (!facingRight) flip();
            }
            //changeAnimCond(1);
            bodyanimator.changeAnimCond(1, bodypart.Weapon);
            //changeAnimCond(Lari, 1);
        }
        else
        {
            //ini nanti kembalikan ke awal
            /*
            changeAnimCond(0);
            */
            if (!battleMode)
                //changeAnimCond(0);
                bodyanimator.changeAnimCond(0, bodypart.Weapon);
                //changeAnimCond(Napas, 1);
            else if (battleMode)
                //changeAnimCond(3);
                bodyanimator.changeAnimCond(3, bodypart.Weapon);
                //changeAnimCond(Battle, 1);
        }
    }

    void flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        theScale.z = 1; //ini ada agar memastikan nilai z player tidak jadi 0, sehingga malah hilang dari pandangan
        transform.localScale = theScale;
    }

    public void beginInterractOrAttack(int mode)
    {
        //logic untuk attack dan interract
        Vector2 CollBox_WorldCoor = base.gameObject.transform.TransformPoint(Coll_Box.transform.localPosition);
            Vector2 Pos = CollBox_WorldCoor;
            Collider2D[] hitCollider = Physics2D.OverlapCircleAll(Pos, rad);
            //masuk ke mode interract
            if (mode == 1)
            {
            myBody.velocity = new Vector3(0, 0, 0);
            StartCoroutine(freezeMovementUntilAnimEnd());
                //get only the first collider on list
                if (hitCollider != null || hitCollider.Length != 0)
                {
                    for (int i = 0; i <= hitCollider.Length; i++)
                    {
                        if (hitCollider[i].tag == "InterestPoint")
                        {
                            hitCollider[i].SendMessage("Interract");
                            break;
                        }
                        if (hitCollider[i].tag == "MesinPoint")
                        {
                            hitCollider[i].SendMessage("beginMesin");
                            break;
                        }
                    }
                }
            }
            //masuk ke mode attack
            else if (mode == 2)
            {
                //get all collider and ddeal damage to all
                if (hitCollider != null || hitCollider.Length > 0)
                {
                    for (int i = 0; i <= hitCollider.Length; i++)
                    {
                        if (hitCollider[i].tag == "Enemy")
                        {
                            hitCollider[i].SendMessage("getDamage", dmg);
                        }
                    }
                }

            }
    }

    IEnumerator freezeMovementUntilAnimEnd()
    {
        EnableMove = false;
        yield return new WaitForSeconds(1.3f);
        EnableMove = true;
    }

    public void startCoroutine()
    {
        StartCoroutine(freezeMovementUntilAnimEnd());
    }

    public void changeanimcond(int cond)
    {
        bodyanimator.changeAnimCond(cond, bodypart.Weapon);
    }

    public void movePlayer(Vector2 vel)
    {
        myBody.velocity = vel;
        return;
    }

    public void getDamage(int damage)
    {
        //player kena damage
        if(!alreadyGetDamage)
        {
            //changeAnimCond(5);
            bodyanimator.changeAnimCond(5, bodypart.Weapon);
            StartCoroutine(freezeMovementUntilAnimEnd());
            Vector2 zero = new Vector2(0, 0);
            myBody.velocity = zero;
            EnableMove = false;
            alreadyGetDamage = true;
            b_gm.currentHP_real -= damage;
            CharInfo.currentHP -= damage;
            StartCoroutine(waitUntilAbleToGetDamageAgain());
        }
    }

    IEnumerator waitUntilAbleToGetDamageAgain()
    {
        yield return new WaitForSeconds(1.0f);
        EnableMove = true;
        yield return new WaitForSeconds(waitUntilGetDamageAgainTime);
        alreadyGetDamage = false;
    }

    public void toggleBattleMode(bool cond)
    {
        battleMode = !cond;
        if (!bodypart.Weapon.activeInHierarchy) bodypart.Weapon.SetActive(true);
        else if (bodypart.Weapon.activeInHierarchy) bodypart.Weapon.SetActive(false);
        if (!battleMode)
        {
            Coll_Box.tag = "Untagged";
            //changeAnimCond(3);
            bodyanimator.changeAnimCond(3, bodypart.Weapon);
            //changeAnimCond(Battle,1);
            battleMode = true;
        }
        else
        {
            Coll_Box.tag = "Player";
            //changeAnimCond(0);
            bodyanimator.changeAnimCond(0, bodypart.Weapon);
            //changeAnimCond(Napas,1);
            battleMode = false;
        }
    }
}
