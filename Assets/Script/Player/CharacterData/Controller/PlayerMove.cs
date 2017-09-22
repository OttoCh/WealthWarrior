using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMove : MonoBehaviour {

    public bool enableMove;
    public float moveForce = 5;
    public GameObject cam;


    bool facingRight = true;
    bool battleMode = false;
    Rigidbody2D myBody;

    MainCharacterSchema mc;

    // Use this for initialization
    void Start () {
        initPlayer();
    }
	
	// Update is called once per frame
	void Update () {
		if(enableMove)
        {
            Vector2 moveVec = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical")) * moveForce * Time.deltaTime;
            myBody.velocity = moveVec;
            if (Anim_EnableMove)
            {
                Vector3 camPos = base.gameObject.transform.position;
                camPos.y += 1;
                cam.transform.position = camPos;
            }
        }
        
	}

    void initPlayer()
    {
        myBody = gameObject.GetComponent<Rigidbody2D>();
        GameObject persistentObject = GameObject.Find("characterPersistentInformation");
        persistentCharacterData pc = persistentObject.GetComponent<persistentCharacterData>();
        mc = new MainCharacterSchema(pc);
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
            mc.changeAnimation(1);
        }
        else
        {
            if (!battleMode)
                mc.changeAnimation(0);
            else if (battleMode)
                mc.changeAnimation(3);
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

    public void stopPlayer()
    {
        myBody.velocity = new Vector3(0, 0, 0);
    }
}
