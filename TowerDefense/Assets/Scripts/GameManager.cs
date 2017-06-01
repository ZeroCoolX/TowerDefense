using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static bool gameIsOver = false;

    public GameObject gameOverUI;

    public GameObject completedLevelUI;


    private void Start() {
        gameIsOver = false;
    }

    // Update is called once per frame
    void Update () {
        if (gameIsOver) {
            return;
        }
		if(PlayerStats.lives <= 0) {
            endGame();
        }
	}

    public void winLevel() {
        //display complete level screen
        gameIsOver = true;//just a safe sanity check for stopping motion
        completedLevelUI.SetActive(true);
    }

    private void endGame() {
        gameIsOver = true;
        gameOverUI.SetActive(true);
    }
}
