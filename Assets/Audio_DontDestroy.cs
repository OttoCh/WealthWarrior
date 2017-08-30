using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_DontDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
