using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevel_Manager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changeScene(int target)
    {
        if(target==3 || target == 0)
        {
            GameObject audioMainMenu = GameObject.Find("MainMenuAudioSource").gameObject;
            Destroy(audioMainMenu);
        }
        SceneManager.LoadScene(target);
    }
}
