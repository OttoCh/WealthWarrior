using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeToBossStage : MonoBehaviour {

    public int bossLevelIndex;
    public GameObject controlJoystick;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            controlJoystick.SendMessage("disableThings");
            SceneManager.LoadScene(bossLevelIndex);
        }
    }
}
