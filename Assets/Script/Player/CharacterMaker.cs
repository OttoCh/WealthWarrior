using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CharacterMaker : MonoBehaviour {
    class BodyPart
    {
        public GameObject Face;
        public GameObject Eyebrow;
        public GameObject Eye;
        public GameObject FrontHair;
        public GameObject BackHair;
        public GameObject Torso;
        public GameObject Mouth;
        public GameObject Weapon;
    }

    class BodyAnimator
    {
        public Animator Eyebrow_Animator;
        public Animator Eye_Animator;
        public Animator FrontHair_Animator;
        public Animator BackHair_Animator;
        public Animator Torso_Animator;
    }

    class BodySprite
    {
        public SpriteRenderer Face_Sprite;
        public SpriteRenderer Eyebrow_Sprite;
        public SpriteRenderer Eye_Sprite;
        public SpriteRenderer FrontHair_Sprite;
        public SpriteRenderer BackHair_Sprite;
        public SpriteRenderer Torso_Sprite;
        public SpriteRenderer Mouth_Sprite;
    }

    public GameObject Hair_Button;
    public GameObject Torso_Button;
    public GameObject Eye_Button;
    public GameObject Weapon_Button;
    public GameObject playerinf;
    public GameObject WindowsUI;
    public GameObject windowsUI_Ornament;
    public GameObject InitialPart_Button;

    public Text partText;

    CharacterInformation CharInfo;

    BodyPart bodypart = new BodyPart();
    BodyAnimator bodyanimator = new BodyAnimator();
    BodySprite bodysprite = new BodySprite();

    public GameObject Target;
    public int part;

    private bool windowsUIFinishAnimate = false;

    // Use this for initialization
    void Start () {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        playerinf = GameObject.Find("CharacterInfo").gameObject;
        CharInfo = playerinf.GetComponent<CharacterInformation>();
        initBodyPartGameObject();
        initAnim();
        //initSprite();
    }

    void Update()
    {
        if(!windowsUIFinishAnimate) showUIWindowOrnament();
    }

    
    void initAnim()
    {
        bodyanimator.Eyebrow_Animator = bodypart.Eyebrow.GetComponent<Animator>();
        bodyanimator.Eye_Animator = bodypart.Eye.GetComponent<Animator>();
        bodyanimator.BackHair_Animator = bodypart.BackHair.GetComponent<Animator>();
        bodyanimator.FrontHair_Animator = bodypart.FrontHair.GetComponent<Animator>();
        bodyanimator.Torso_Animator = bodypart.Torso.GetComponent<Animator>();
    }

    void initSprite()
    {
        bodysprite.BackHair_Sprite = bodypart.BackHair.GetComponent<SpriteRenderer>();
        bodysprite.FrontHair_Sprite = bodypart.FrontHair.GetComponent<SpriteRenderer>();
        bodysprite.Torso_Sprite = bodypart.Torso.GetComponent<SpriteRenderer>();
    }

    void initBodyPartGameObject()
    {
        bodypart.Face = Target.transform.Find("Face").gameObject;
        bodypart.Eyebrow = Target.transform.Find("Eyebrow").gameObject;
        bodypart.Eye = Target.transform.Find("Eye").gameObject;
        bodypart.FrontHair = Target.transform.Find("FrontHair").gameObject;
        bodypart.BackHair = Target.transform.Find("BackHair").gameObject;
        bodypart.Torso = Target.transform.Find("Torso").gameObject;
        bodypart.Mouth = Target.transform.Find("Mouth").gameObject;
        bodypart.Weapon = Target.transform.Find("Weapon").gameObject;
    }

    public void changeBodyPart(string selection)
    {
        //initAnim();
        string uri = "All Controller/" + selection.ToLower();
        if (part == 1)  //rambut
        {
            bodypart.FrontHair = Target.transform.Find("FrontHair").gameObject;
            uri = "All Controller/front" + selection.ToLower();
            var fh = Resources.Load(uri) as RuntimeAnimatorController;
            bodyanimator.FrontHair_Animator.runtimeAnimatorController = fh;
            Debug.Log(CharInfo.playerController_persist.FrontHair_Ctrl);

            bodypart.BackHair = Target.transform.Find("BackHair").gameObject;
            uri = "All Controller/back" + selection.ToLower();
            var bh = Resources.Load(uri) as RuntimeAnimatorController;
            bodyanimator.BackHair_Animator.runtimeAnimatorController = bh;
        }
        else if (part == 2)  //torso
        {
            var t = Resources.Load(uri) as RuntimeAnimatorController;
            bodypart.Torso = Target.transform.Find("Torso").gameObject;
            bodyanimator.Torso_Animator.runtimeAnimatorController = t;
        }
        else if (part == 3) //weapon
        {
            var w = Resources.Load(uri) as RuntimeAnimatorController;
            bodypart.Weapon = Target.transform.Find("Weapon").gameObject;
            //bodyanimator.Weapon_Animator.runtimeAnimatorController = w;
        }
        else if(part == 4) //eye
        {
            var w = Resources.Load(uri) as RuntimeAnimatorController;
            bodypart.Weapon = Target.transform.Find("Eye").gameObject;
            //bodyanimator.Eye_Animator.runtimeAnimatorController = w;
        }
        Resources.UnloadUnusedAssets();
        return;
    }

    
    public void ChangePart(int part)
    {
        if(part==1)
        {
            //right button
            this.part += 1;
            if (this.part > 4) this.part = 0;
        }
        else if(part==0)
        {
            //left button
            this.part -= 1;
            if (this.part < 1) this.part = 4;
        }
        //and then change part and the text
        if(this.part==1)
        {
            partText.text = "HAIR";
            Hair_Button.SetActive(true);
            Torso_Button.SetActive(false);
            Weapon_Button.SetActive(false);
            Eye_Button.SetActive(false);

        }
        else if(this.part==2)
        {
            partText.text = "BODY";
            Hair_Button.SetActive(false);
            Torso_Button.SetActive(true);
            Weapon_Button.SetActive(false);
            Eye_Button.SetActive(false);
        }
        else if(this.part==3)
        {
            partText.text = "WEAPON";
            Hair_Button.SetActive(false);
            Torso_Button.SetActive(false);
            Weapon_Button.SetActive(true);
            Eye_Button.SetActive(false);
        }
        else if (this.part == 4)
        {
            partText.text = "EYE";
            Hair_Button.SetActive(false);
            Torso_Button.SetActive(false);
            Weapon_Button.SetActive(false);
            Eye_Button.SetActive(true);
        }
        return;
    }

    public void SaveCharacterChange()
    {
        CharInfo.playerController_persist.Eye_Ctrl = bodyanimator.Eye_Animator.runtimeAnimatorController;
        CharInfo.playerController_persist.Torso_Ctrl = bodyanimator.Torso_Animator.runtimeAnimatorController;
        CharInfo.playerController_persist.BackHair_Ctrl = bodyanimator.BackHair_Animator.runtimeAnimatorController;
        CharInfo.playerController_persist.FrontHair_Ctrl = bodyanimator.FrontHair_Animator.runtimeAnimatorController;
    }

    void showUIWindowOrnament()
    {
        Animator windowsUI_Anim = WindowsUI.GetComponent<Animator>();
        //Animation an = WindowsUI.GetComponent<Animation>();
        var currentAnim = windowsUI_Anim.GetCurrentAnimatorStateInfo(0);
        //if (windowsUI_Anim.GetCurrentAnimatorStateInfo(0).IsName("WindowOpen"))
        if (currentAnim.normalizedTime > 1.0f)  //ini cek apakah animasi sudah pernah looping dengan nilai desimal adalah jumlah loop dan floatnya adlaah proses animasi yg baru lagi
            //artinya begitu animasi yg skrg sudah dijalankan 1 kali maka lgsg masuk ke sini
        {
            windowsUIFinishAnimate = true;
            windowsUI_Ornament.SetActive(true);
            InitialPart_Button.SetActive(true);
        }
    }

    public void changeScene(int target)
    {
        SceneManager.LoadScene(target);
    }
}
