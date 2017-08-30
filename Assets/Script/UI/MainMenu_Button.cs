using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu_Button : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadLevel(int target)
    {
        SceneManager.LoadScene(target);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
