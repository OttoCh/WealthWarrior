using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Player_Interract : MonoBehaviour {

    public float player_attackdamage;
    public float player_health;
    public float rad;
    
	
	// Update is called once per frame
	void Update () {
        if(CrossPlatformInputManager.GetButtonDown("Interract"))
        {
            Interract();
        }
        if(CrossPlatformInputManager.GetButtonDown("Attack"))
        {
            Player_Attack();
        }

    }

    void Interract()
    {
        Vector2 Pos = new Vector2(gameObject.transform.position.x+2, gameObject.transform.position.y);
        Collider2D[] InterractCollider = Physics2D.OverlapCircleAll(Pos, rad);
        if (InterractCollider.Length != 0)
        {
            Debug.Log("Interest");
            bool InterractExist = false;
            for (int i = 0; i < InterractCollider.Length; i++)
            {
                if (InterractCollider[i].gameObject.tag == "InterestPoint") InterractCollider[i].SendMessage("Interract");
                InterractExist = true;
            }
            if(InterractExist)
            {
                Player_Move playerMove = gameObject.GetComponent<Player_Move>();
                playerMove.changeAnimCond(2);
            }
        }
    }

    void Player_Attack()
    {
        Vector2 Pos = new Vector2(gameObject.transform.position.x+2, gameObject.transform.position.y);
        Collider2D[] HitCollider = Physics2D.OverlapCircleAll(Pos, rad);
        Debug.Log("Attack");
        if (HitCollider.Length != 0)
        {
            bool AttackExist = false;
            for (int i = 0; i < HitCollider.Length; i++)
            {
                if (HitCollider[i].gameObject.tag == "Enemy") HitCollider[i].SendMessage("Attack", player_attackdamage);
                AttackExist = true;
            }
            if (AttackExist)
            {
                Player_Move playerMove = gameObject.GetComponent<Player_Move>();
                playerMove.changeAnimCond(1);
            }
        }
    }

    void Player_Attacked(float dmg)
    {
        player_health -= dmg;
        if(player_health <= 0)
        {
            //death
            //start loading things
        }
    }

}
