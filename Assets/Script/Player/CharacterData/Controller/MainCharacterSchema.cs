using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterSchema : CharacterData {

    public MainCharacterSchema(persistentCharacterData CharInfo)
    {
        //init dan berikan variabel datanya
        
        if (CharInfo.playerController_persist.Torso_Ctrl != null)
        {
            this.Torso_Animator.runtimeAnimatorController = CharInfo.playerController_persist.Torso_Ctrl;
            this.FrontHair_Animator.runtimeAnimatorController = CharInfo.playerController_persist.FrontHair_Ctrl;
            this.BackHair_Animator.runtimeAnimatorController = CharInfo.playerController_persist.BackHair_Ctrl;
            this.Eye_Animator.runtimeAnimatorController = CharInfo.playerController_persist.Eye_Ctrl;
            this.Weapon_Animator.runtimeAnimatorController = CharInfo.playerController_persist.Weapon_Ctrl;
        }
        else
        {
            throw new System.ArgumentException("body controller is null", "original");
        }
    }

	public void decreaseHealth(int health)
    {
        this.health -= health;
        if (this.health < 0) this.health = 0;
        return;
    }

    public void decreaseMoney(int money)
    {
        this.health -= health;
        if (this.health < 0) this.health = 0;
        return;
    }

    public void addIncome(int income)
    {
        if(income > 0) this.income += income;
    }

    public void addOutcome(int outcome)
    {
        if (outcome > 0) this.outcome += outcome;
    }

    public void changeAnimation(int cond)
    {
        string Interract = "Interract";
        string Attack = "Attack";
        string Attacked = "Attacked";

        if(cond == 5 || cond==4 || cond==2)
        {
            string state = "null";
            if (cond == 5) state = Attacked;
            else if (cond == 4) state = Attack;
            else if (cond == 2) state = Interract;

            Torso_Animator.SetTrigger(state);
            Face_Animator.SetTrigger(state);
            Eye_Animator.SetTrigger(state);
            Eyebrow_Animator.SetTrigger(state);
            BackHair_Animator.SetTrigger(state);
            FrontHair_Animator.SetTrigger(state);
            Mouth_Animator.SetTrigger(state);
            Weapon_Animator.SetTrigger(state);
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
            Weapon_Animator.SetInteger("cond", cond);
        }
    }

}
   
