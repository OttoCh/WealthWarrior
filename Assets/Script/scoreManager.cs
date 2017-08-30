using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scoreManager : MonoBehaviour {

    public GameObject Devisit;
    public GameObject Surplus;
    public Text Pemasukan;
    public Text Pengeluaran;
    private GameObject characterInfo;
    CharacterInformation cf;


	// Use this for initialization
	void Start () {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        characterInfo = GameObject.Find("CharacterInfo").gameObject;
        cf = characterInfo.GetComponent<CharacterInformation>();
        Pemasukan.text = cf.totalPemasukan.ToString();
        Pengeluaran.text = cf.totalPengeluaran.ToString();
        if(cf.totalPemasukan > cf.totalPengeluaran)
        {
            Surplus.SetActive(true);
        }
        else
        {
            Devisit.SetActive(true);
        }
        StartCoroutine(changetoPrizeScene());
    }
	
    IEnumerator changetoPrizeScene()
    {
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene(7);

    }

	// Update is called once per frame
	void Update () {
		
	}


}
