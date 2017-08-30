using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Audio_Manager : MonoBehaviour {

    public GameObject mainMenuAudio;
    public GameObject battleAudio;
    public float durationWaitTime;
    AudioSource clip1;
    AudioSource clip2;

	// Use this for initialization
	void Start () {
        clip1 = mainMenuAudio.GetComponent<AudioSource>();
        clip2 = battleAudio.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changeAudio(bool transitionMode)
    {
        StartCoroutine(beginCrossfade(clip1, clip2, transitionMode));
    }

    public void silent()
    {
        clip1.volume = 0;
        clip2.volume = 0;
    }

    IEnumerator beginCrossfade(AudioSource audio1, AudioSource audio2, bool mode) 
    {
        for(int i=0; i<10; i++)
        {
            yield return new WaitForSeconds(durationWaitTime);
            if(mode)
            {
                if(clip1.volume > 0) clip1.volume -= 0.1f;
                if (clip2.volume < 1) clip2.volume += 0.1f;
            }
            else
            {
                if (clip1.volume < 1) clip1.volume += 0.1f;
                if (clip2.volume > 0) clip2.volume -= 0.1f;
            }
        }

    }
}
