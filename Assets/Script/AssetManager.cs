using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AssetManager : MonoBehaviour {

    public GameObject allAsset;
    public GameObject WindowsUI;
    public GameObject windowsUI_Ornament;
    public GameObject InitialPart_Button;

    int curAsset = 1;
    public int maxAsset;
    public Text assetText;

    private bool windowsUIFinishAnimate = false;

    // Use this for initialization
    void Start () {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
	
	// Update is called once per frame
	void Update () {
        if (!windowsUIFinishAnimate) showUIWindowOrnament();
    }

    void changeAssetButton()
    {
        if(curAsset==1)
        {
            assetText.text = "All Text";
            allAsset.SetActive(true);
        }
    }
    
    public void changeAsset(int curAsset)
    {
        if(curAsset==1)
        {
            this.curAsset += 1;
            if (this.curAsset > maxAsset) this.curAsset = 1;
        }
        else if(curAsset==0)
        {
            this.curAsset -= 0;
            if (this.curAsset < 1) this.curAsset = maxAsset;
        }
        changeAssetButton();
        return;
    }

    void showUIWindowOrnament()
    {
        Animator windowsUI_Anim = WindowsUI.GetComponent<Animator>();
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

}
