using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private bool gameHasEnded = false;
	
	// Update is called once per frame
	void Update () {
        if (gameHasEnded) {
            return;
        }
		if(PlayerStats.lives <= 0) {
            endGame();
        }
	}

    private void endGame() {
        gameHasEnded = true;
        Debug.Log("Game has ended");
    }
}
