using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Dialog_text : MonoBehaviour {

	public GameObject TextBox;

	public Text theText;	

	public int CurrentLine;   //!!!!
	public int EndLine;		//!!!!

	public TextAsset TextFile;	//!!!!
	public string[] TextLines;

	public bool isTyping;
	private bool cancelTyping;

	public float typingSpeed;

	public bool appear;	//only true if the text box must appear without any trigger

	void Start() {
		if(EndLine == 0) {
            if (TextFile != null)
            {
                TextLines = new string[1];
                TextLines = TextFile.text.Split('\n');
            }
            EndLine = TextLines.Length;
		}

		if(appear) {
			EnableText();
		}

		if(!appear) {
			DisableText();
		}
	}

	void Update() {
		if(!appear) {
			return;
		}
        //theText = TextLines[CurrentLine];

        //if(Input.GetKeyDown(KeyCode.Return)) {
        if (Input.GetButtonDown("Fire1")) {
            if (!isTyping) {
				CurrentLine += 1;

				if(CurrentLine > EndLine) {
					DisableText();
				}
				else {
					StartCoroutine(TextScroll(TextLines[CurrentLine])) ;
					}
				}

		}
		else if(isTyping && !cancelTyping) {
				cancelTyping = true;
		}
	}

	public IEnumerator TextScroll (string lineofText) {
			//ketik satu per satu hurufnya
			int letter = 0;	//mulai dari awal baris
			theText.text = "";
			isTyping = true;
			cancelTyping = false;
			while(isTyping && !cancelTyping && letter < lineofText.Length - 1) {
				theText.text += lineofText[letter];
				letter += 1;
				yield return new WaitForSeconds(typingSpeed);
			}
			theText.text = lineofText;
			isTyping = false;
			cancelTyping = false;
			
		}

	void DisableText() {
		TextBox.SetActive(false);
		appear = false;
		}

	public void EnableText() {
		TextBox.SetActive(true);
		appear = true;
		}

	public void ReloadText(TextAsset newText) {
		Debug.Log("Reload Text");
		if(newText != null) {
				TextLines = new string[1];
				TextLines = newText.text.Split ('\n');
			}
		}
}
