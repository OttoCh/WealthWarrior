using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpperIcon_Button : MonoBehaviour {

    Button thisButton;

	// Use this for initialization
	void Start () {
        thisButton = gameObject.GetComponent<Button>();
        changeButtonColor();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void changeButtonColor()
    {
        if(Application.loadedLevelName == "Scene_Achievement")
        {
            thisButton.image.color = Color.white;
        }
    }
    
    public void changeScene_Asset()
    {
        if(Application.loadedLevelName == "Scene_Assets")
        {
            Application.LoadLevel("Scene_CharacterCreation");
        }
        else if(Application.loadedLevelName == "Scene_CharacterCreation")
        {
            Application.LoadLevel("Scene_Assets");
        }
            
    }
}

/*
 * public void changeScene(int target) {
 *  SceneManager.loadScene(target);
 * }
 * 
 * 
 */
