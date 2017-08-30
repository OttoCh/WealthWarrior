using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostInterraction : MonoBehaviour {

    public int startLine;
    public int finishLine;
    public TextAsset textAsset;
    public GameObject DialogManager;
    Dialog_1 dialog1;

    // Use this for initialization
    void Start () {
        dialog1 = DialogManager.GetComponent<Dialog_1>();
        initialInterraction();
    }
	
    void initialInterraction()
    {
        if (startLine == 0)
        {
            dialog1.Atrigger(textAsset, 0, 3);
        }
    } 

	// Update is called once per frame
	void Update () {
		
	}

    public void Interract()
    {
        dialog1.Atrigger(textAsset, startLine, finishLine);
    }
}
