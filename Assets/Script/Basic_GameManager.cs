using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Basic_GameManager : MonoBehaviour {

    //for creep
    public bool CreepDefeated = false;
    public int numbOfCreep = 1;
    public float towerCost;
    public float pengeluaran;
    public float pendapatan;

    public GameObject StageManage;
    public GameObject BossStageManager;
    public GameObject Player;
    public GameObject Tower;
    public GameObject Shield;
    public GameObject lamp;
    public GameObject tong;
    public GameObject Cam;
    public GameObject joystickControl;
    public GameObject InterractButton;
    public GameObject AttackButton;
    public GameObject AudioManager;
    public Text savingText;
    public Image HPBar;

    Audio_Manager AudioM;
    CharacterInformation charInfo;
    GameObject summoningTower;
    GameObject summoningShield;
    bool klicked = false;
    float initOrtho;
    Vector3 initPos;
    bool disableTower = false;
    bool disableShield = false;
    public int numberOfStage;
    private bool ATMShow = false;
    public GameObject ATMButton;

    //hp bar manager
    float maxHP_width;
    int AcceptMode = 3;
    public float maxHP_real;
    public float savings;
    public float currentHP_real;

        // Use this for initialization
    void Start () {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        AudioM = AudioManager.GetComponent<Audio_Manager>();
        maxHP_width = 1.0f;
        setActiveJoystickControl(true);
        charInfo = GameObject.Find("CharacterInfo").gameObject.GetComponent<CharacterInformation>();
        //currentHP_real = maxHP_real;
        currentHP_real = charInfo.currentHP;
        savings = charInfo.saving;
        savingText.text = savings.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        manageHPSize();
    }

    public void GetCoin(int val)
    {
        //coin_val += val;
        savings += val;
        charInfo.saving = savings;
        givePlayerMoney((float)val);
        return;
    }

    public void changeTowerDir()
    {

        Collider2D[] listCol = Physics2D.OverlapCircleAll(Player.transform.position, 0.2f);
        int i = 0;
        while(i < listCol.Length)
        {
            if(listCol[i].tag == "Tower")
            {
                TowerAI ta = listCol[i].GetComponent<TowerAI>();
                ta.changeDirrection();
            }
            i++;
        }
    }

    public void summonTower(int intAccept)
    {
        if (!klicked)
        {
            if (!disableTower)
            {
                AcceptMode = intAccept;
                StageManager sm = StageManage.GetComponent<StageManager>();
                sm.allowMove = false;
                Vector2 pos = Player.gameObject.transform.position;
                summoningTower = Instantiate(Tower, pos, Quaternion.identity) as GameObject;
                klicked = true;
            }
        }
    }

    public void createShield(int intAccept)
    {
        if(!klicked)
        {
            if (!disableShield)
            {
                AcceptMode = intAccept;
                StageManager sm = StageManage.GetComponent<StageManager>();
                sm.allowMove = false;
                Vector2 pos = Player.gameObject.transform.position;
                summoningShield = Instantiate(Shield, pos, Quaternion.identity) as GameObject;
                klicked = true;
            }
        }
    }

    public void disableSummon(int disableMode)
    {
        if (numberOfStage == 2)
        {
            if (disableMode == 1)
            {
                disableTower = true;
            }
            else if (disableMode == 2)
            {
                disableShield = true;
            }
            else
            {
                disableTower = false;
                disableShield = false;
            }
        }
    }

    public void cancelSummonTower()
    {
        if (AcceptMode == 1)
        {
            klicked = false;
            StageManager sm = StageManage.GetComponent<StageManager>();
            sm.allowMove = true;
            Destroy(summoningTower);
        }
        else if(AcceptMode == 2)
        {
            klicked = false;
            StageManager sm = StageManage.GetComponent<StageManager>();
            sm.allowMove = true;
            Destroy(summoningShield);
        }
        AcceptMode = 3;
    }

    public void changePlayerDrawOrder(int increaseNum)
    {
        Component[] AllChildren = Player.GetComponentsInChildren(typeof(SpriteRenderer));
        string sortinglayername;
        if (increaseNum>0)
        {
            sortinglayername = "Stage";
        }
        else
        {
            sortinglayername = "Character";
        }
        foreach (SpriteRenderer child in AllChildren)
        {
            child.sortingLayerName = sortinglayername;
        }
    }

    public void turnOffWalk(bool cond)
    {
        Player_Move pm = Player.GetComponent<Player_Move>();
        pm.EnableMove = cond;
    }

    public void setActiveLamp(bool cond)
    {
        if (lamp != null)
        {
            lamp.SetActive(cond);
        }
    }

    public void setActiveTong(bool cond)
    {
        if (tong != null)
        {
            tong.SetActive(cond);
        }
    }

    public void setActiveJoystickControl(bool cond)
    {
        joystickControl.SetActive(cond);
    }

    public void setCamera(bool cond, Vector3 stagePosition)
    {
        Camera cm = Cam.GetComponent<Camera>();
        if (cond == true)
        {
            initOrtho = cm.orthographicSize;
            initPos = cm.transform.position;
            cm.orthographicSize = 3.23f;
            Vector3 stageInitPos = stagePosition;
            stageInitPos.z = Cam.transform.position.z;
            //Cam.transform.position = new Vector3(2.15f, 3.9f, Cam.transform.position.z);
            Cam.transform.position = stageInitPos;
        }
        else
        {
            cm.orthographicSize = 3.23f;
            Cam.transform.position = initPos;
        }
    }

    void manageHPSize()
    {
        float currentHP_width = maxHP_width * (currentHP_real / maxHP_real);
        Image img = HPBar.GetComponent<Image>();
        img.fillAmount = currentHP_width;
        if(currentHP_real <= 0)
        {
            Destroy(Player);
        }
        return;
    }

    public void increaseHPfromSaving()
    {
        //fungsi ini dipanggil lewat tombol atm 
        if (savings >= 0)
        {
            currentHP_real += 100.0f;
            savings -= 100.0f;
            manageHPandSaving();
        }
    }

    void manageHPandSaving()
    {
        charInfo.saving = savings;
        charInfo.currentHP = currentHP_real;
        savingText.text = savings.ToString();
    }

    public void AcceptSummon()
    {
        if (AcceptMode != 3)
        {
            givePlayerMoney(-1*towerCost);
            StageManager sm = StageManage.GetComponent<StageManager>();
            sm.allowMove = true;
            if (AcceptMode == 1)
            {
                Collider2D[] listCol = Physics2D.OverlapCircleAll(Player.transform.position, 1.0f);
                int i = 0;
                while (i < listCol.Length)
                {
                    if (listCol[i].tag == "Tower")
                    {
                        TowerAI ta = listCol[i].GetComponent<TowerAI>();
                        ta.changeAnim();
                    }
                    i++;
                }
                AcceptMode = 3;
                klicked = false;
            }

            else if(AcceptMode == 2)
            {
                givePlayerMoney(-1 * towerCost);
                Animator shieldAnimator = summoningShield.transform.Find("Shield").gameObject.GetComponent<Animator>();
                shieldAnimator.SetTrigger("In");
                AcceptMode = 3;
                klicked = false;
            }
        }
    }


    public void beginCreepAttack()
    {
        toggleBattle(true);
        GameObject[] allCreep = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject creep in allCreep) {
            Kroco_AI kAI = creep.GetComponent<Kroco_AI>();
            if(kAI != null)
            {
                kAI.enableMove = true;
            }
        }
    }

    public void creepDefeated()
    {
        numbOfCreep += 1;
        if (numbOfCreep >= 1)
        {
            toggleBattle(false);
            CreepDefeated = true;
        }
    }

    public void bossDefeated()
    {
        bossStageManager b_AI = GameObject.Find("bossStageManager").gameObject.GetComponent<bossStageManager>();
        b_AI.BossAlreadyDefeated = true;
        b_AI.endGame();
    }

    public void toggleBattle(bool cond)
    {
        Player_Move pm = Player.GetComponent<Player_Move>();
        AttackButton.SetActive(cond);
        InterractButton.SetActive(!cond);
        pm.toggleBattleMode(cond);
        changeAudio(cond);
    }

    public void changeAudio(bool cond)
    {
        AudioM.changeAudio(cond);
    }

    public void stageFinished(int currentStage)
    {
        if(currentStage==3)
        {
            //mulai boss fight
            Debug.Log("Begin boss fight");
            bossStageManager bsm = BossStageManager.GetComponent<bossStageManager>();
            bsm.StartBossFight();
        }
    }

    public void silent()
    {
        AudioM.silent();
    }

    public void showATMButton()
    {
        ATMButton.SetActive(!ATMShow);
        ATMShow = !ATMShow;
    }

    public void getATM(float getMoney)
    {
        if (currentHP_real < maxHP_real)
        {
            float diffHP = maxHP_real - currentHP_real;
            if (savings >= 0.0f)
            {
                if (diffHP <= getMoney)
                {
                    if (diffHP <= savings)
                    {
                        currentHP_real += diffHP;
                        givePlayerMoney(-1 * diffHP);
                    }
                    else
                    {
                        currentHP_real += savings;
                        givePlayerMoney(-1 * savings);
                    }
                }
                else if (diffHP > getMoney)
                {
                    if (savings >= getMoney)
                    {
                        currentHP_real += getMoney;
                        givePlayerMoney(-getMoney);
                    }
                    else
                    {
                        currentHP_real += savings;
                        givePlayerMoney(-savings);
                    }
                }
            }
        }
    }

    public void givePlayerMoney(float totalMoney)
    {
        if(totalMoney <0)
        {
            charInfo.plusTotalPengeluaran(-1 * totalMoney);
        }
        else if(totalMoney > 0)
        {
            charInfo.plusTotalPemasukan(totalMoney);
        }
        savings += totalMoney;
        manageHPandSaving();
    }
}
