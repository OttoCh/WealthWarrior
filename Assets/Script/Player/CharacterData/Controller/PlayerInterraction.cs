using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterraction : MonoBehaviour {

    bool Anim_EnableMove;

    public float dmg;
    public GameObject Coll_Box;
    public float rad;

    IEnumerator freezeMovementUntilAnimEnd()
    {
        Anim_EnableMove = false;
        yield return new WaitForSeconds(1.3f);
        Anim_EnableMove = true;
    }

    public void beginInterractOrAttack(int mode)
    {
        Vector2 CollBox_WorldCoor = base.gameObject.transform.TransformPoint(Coll_Box.transform.localPosition);
        Vector2 Pos = CollBox_WorldCoor;
        Collider2D[] hitCollider = Physics2D.OverlapCircleAll(Pos, rad);

        //masuk ke mode interract
        if (mode == 1)
        {
            PlayerMove pm = gameObject.GetComponent<PlayerMove>();
            pm.stopPlayer();

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

}
