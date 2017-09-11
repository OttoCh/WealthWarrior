using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Schema : MonoBehaviour {

    public class BodyPart
    {
        public GameObject Face;
        public GameObject Eyebrow;
        public GameObject Eye;
        public GameObject FrontHair;
        public GameObject BackHair;
        public GameObject Torso;
        public GameObject Mouth;
        public GameObject Weapon;

        public BodyPart(GameObject o)
        {
            Face = o.transform.Find("Face").gameObject;
            Eyebrow = o.transform.Find("Eyebrow").gameObject;
            Eye = o.transform.Find("Eye").gameObject;
            FrontHair = o.transform.Find("FrontHair").gameObject;
            BackHair = o.transform.Find("BackHair").gameObject;
            Torso = o.transform.Find("Torso").gameObject;
            Mouth = o.transform.Find("Mouth").gameObject;
            Weapon = o.transform.Find("Weapon").gameObject;
        }
    }

    public class BodyAnimator
    {
        public Animator Face_Animator;
        public Animator Eyebrow_Animator;
        public Animator Eye_Animator;
        public Animator FrontHair_Animator;
        public Animator BackHair_Animator;
        public Animator Torso_Animator;
        public Animator Mouth_Animator;
        public Animator Weapon_Animator;

        private string Lari = "Lari";
        private string Interract = "Interract";
        private string Napas = "Napas";
        private string Battle = "Battle";
        private string Attack = "Attack";
        private string Attacked = "Attacked";

        public void changeAnimCond(int cond, GameObject o)
        {
            //Debug.Log(cond);
            if (cond == 5) //attacked
            {
                Torso_Animator.SetTrigger(Attacked);
                Face_Animator.SetTrigger(Attacked);
                Eye_Animator.SetTrigger(Attacked);
                Eyebrow_Animator.SetTrigger(Attacked);
                BackHair_Animator.SetTrigger(Attacked);
                FrontHair_Animator.SetTrigger(Attacked);
                Mouth_Animator.SetTrigger(Attacked);
                //if (bodypart.Weapon.activeInHierarchy) Weapon_Animator.SetTrigger(Attacked);

                if (o.activeInHierarchy) Weapon_Animator.SetTrigger(Attacked);
                //StartCoroutine(freezeMovementUntilAnimEnd());
            }
            else if (cond == 4)
            {
                Torso_Animator.SetTrigger(Attack);
                Face_Animator.SetTrigger(Attack);
                Eye_Animator.SetTrigger(Attack);
                Eyebrow_Animator.SetTrigger(Attack);
                BackHair_Animator.SetTrigger(Attack);
                FrontHair_Animator.SetTrigger(Attack);
                Mouth_Animator.SetTrigger(Attack);
                //if (Weapon.activeInHierarchy) Weapon_Animator.SetTrigger(Attack);
                //beginInterractOrAttack(2);
                //StartCoroutine(freezeMovementUntilAnimEnd());

                if (o.activeInHierarchy) Weapon_Animator.SetTrigger(Attack);

            }
            else if (cond == 2)
            {
                Torso_Animator.SetTrigger(Interract);
                Face_Animator.SetTrigger(Interract);
                Eye_Animator.SetTrigger(Interract);
                Eyebrow_Animator.SetTrigger(Interract);
                BackHair_Animator.SetTrigger(Interract);
                FrontHair_Animator.SetTrigger(Interract);
                Mouth_Animator.SetTrigger(Interract);
                //interract
                //beginInterractOrAttack(1);
                //StartCoroutine(freezeMovementUntilAnimEnd());
            }
            else
            {
                Torso_Animator.SetInteger("cond", cond);
                Face_Animator.SetInteger("cond", cond);
                Eye_Animator.SetInteger("cond", cond);
                Eyebrow_Animator.SetInteger("cond", cond);
                BackHair_Animator.SetInteger("cond", cond);
                FrontHair_Animator.SetInteger("cond", cond);
                Mouth_Animator.SetInteger("cond", cond);
                //if (bodypart.Weapon.activeInHierarchy) Weapon_Animator.SetInteger("cond", cond);
                if (o.activeInHierarchy) Weapon_Animator.SetInteger("cond", cond);
            }
            //bodyanimator.Weapon_Animator.SetInteger("cond", cond);
        }

    }

   


}

