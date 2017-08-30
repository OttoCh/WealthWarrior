using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class bossStageManager : MonoBehaviour {

    public int numberOfStage;
    public bool BossAlreadyDefeated = false;
    public bool beginStage = true;
    public bool beginFight = true;
    public GameObject BossSpawnPosition;
    public GameObject Stagemanager;
    public GameObject GameManager;
    public GameObject characterInfo;
    public GameObject Boss;
    public GameObject YouWin_Image;
    public GameObject joystick;
    public GameObject fanfare_Audio;

    private GameObject BossObject;
    Basic_GameManager b_gm;
    CharacterInformation cf;
    Boss_AI b_AI;
    StageManager sm;

	// Use this for initialization
	void Start () {
        characterInfo = GameObject.Find("CharacterInfo").gameObject;
        sm = Stagemanager.GetComponent<StageManager>();
        b_AI = Boss.GetComponent<Boss_AI>();
        cf = characterInfo.GetComponent<CharacterInformation>();
        b_gm = GameManager.GetComponent<Basic_GameManager>();
	}

    private void Update()
    {
        if(beginFight)
        {
            StartBossFight();
            beginFight = false;
        }
        if(beginStage)
        {
            startStage();
            beginStage = false;
        }
    }

    public void startStage()
    {
        sm.beginStage(numberOfStage);
    }

    public void StartBossFight()
    {
        Debug.Log("StartBossFight");
        Basic_GameManager b_gm = GameManager.GetComponent<Basic_GameManager>();
        b_gm.toggleBattle(true);
        BossObject = Instantiate(Boss, BossSpawnPosition.transform.position, Quaternion.identity) as GameObject;
    }

    public void endGame()
    {
        if(BossAlreadyDefeated)
        {
            Debug.Log("Already End");
            cf.saving = b_gm.savings;
            cf.currentHP = b_gm.currentHP_real;
            GameObject[] allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in allEnemy)
            {
                Destroy(enemy);
            }
            StartCoroutine(fadeInYouWin());
            //langsung pindahin scene ke score
            
        }
    }

    IEnumerator fadeInYouWin()
    {
        YouWin_Image.SetActive(true);
        float a = 0.0f;
        Image sp = YouWin_Image.GetComponent<Image>();
        sp.color = new Color(1f, 1f, 1f, 0);
        while (a < 1)
        {
            a += 0.1f;
            yield return new WaitForSeconds(0.1f);
            sp.color = new Color(1f, 1f, 1f, a);
        }
        b_gm.silent();
        fanfare_Audio.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(6);
    }


}
