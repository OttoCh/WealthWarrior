using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Interraction : MonoBehaviour {

    public int startLine;
    public int finishLine;
    public TextAsset textAsset;
    public GameObject DialogManager;
    public bool alreadyCall = false;
    Dialog_1 dialog1;

	// Use this for initialization
	void Start () {
        dialog1 = DialogManager.GetComponent<Dialog_1>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Interract()
    {
        if (!alreadyCall)
        {
            alreadyCall = true;
            dialog1.Atrigger(textAsset, startLine, finishLine);
            StartCoroutine(waitUntilTalkAgain());
        }
    }

    IEnumerator waitUntilTalkAgain()
    {
        yield return new WaitForSeconds(2.0f);
        alreadyCall = false;
    }
}
