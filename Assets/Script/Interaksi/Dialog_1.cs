using UnityEngine;
using System.Collections;

public class Dialog_1 : MonoBehaviour {

	//public TextAsset theText;

	public Dialog_text dialogtext;
    /*
    public TextAsset tst;
    public int start;
    public int end;
    */
	
	// Use this for initialization
	void Start () {
        dialogtext = gameObject.GetComponent<Dialog_text>();
        //test
        //Atrigger(tst, start, end);
	}

	//Adakan trigger sesuatu di update agar Atrigger() bisa dipanggil

	public void Atrigger(TextAsset theText, int StartLine, int FinishLine) {
		dialogtext.ReloadText(theText);
		dialogtext.CurrentLine = StartLine;
		dialogtext.EndLine = FinishLine;
		dialogtext.EnableText();
	}
}
