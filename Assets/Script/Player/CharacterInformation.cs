using UnityEngine;
using System.Collections;

public class CharacterInformation : MonoBehaviour {

    public class ControllerPlayerAnim
    {
        public RuntimeAnimatorController Eye_Ctrl;
        public RuntimeAnimatorController FrontHair_Ctrl;
        public RuntimeAnimatorController BackHair_Ctrl;
        public RuntimeAnimatorController Torso_Ctrl;
        public RuntimeAnimatorController Weapon_Ctrl;
    }

    public ControllerPlayerAnim playerController_persist = new ControllerPlayerAnim();

    public float saving;
    public float currentHP;
    public float totalPengeluaran;
    public float totalPemasukan;

    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(this);
        string uri = "All Controller/";
        string eye = "GreenEyes_Controller".ToLower();
        string fronthair = "FrontBlackHair_Controller".ToLower();
        string backhair = "BackBlackHair_Controller".ToLower();
        string torso = "Baju1a_Controller".ToLower();
        string weapon = "Weapon1_Controller".ToLower();
        playerController_persist.Eye_Ctrl = Resources.Load(uri + eye) as RuntimeAnimatorController;
        playerController_persist.FrontHair_Ctrl = Resources.Load(uri + fronthair) as RuntimeAnimatorController;
        playerController_persist.BackHair_Ctrl = Resources.Load(uri + backhair) as RuntimeAnimatorController;
        playerController_persist.Torso_Ctrl = Resources.Load(uri + torso) as RuntimeAnimatorController;
        playerController_persist.Weapon_Ctrl = Resources.Load(uri + weapon) as RuntimeAnimatorController;
    }

	void Start () {

	}

    public void plusTotalPengeluaran(float pengeluaran)
    {
        totalPengeluaran += pengeluaran;
    }

    public void plusTotalPemasukan(float pemasukan)
    {
        Debug.Log("ada pemasukan");
        totalPemasukan += pemasukan;
    }
}
