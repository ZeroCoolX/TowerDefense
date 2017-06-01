using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    public Text roundsText;
    public SceneFader sceneFader;
    public string menuSceneName = "MainMenu";

    private void OnEnable() {
        roundsText.text = PlayerStats.roundsSurvived.ToString();
    }

    public void retry() {
        sceneFader.fadeTo(SceneManager.GetActiveScene().name);
    }

    public void goToMenu() {
        sceneFader.fadeTo(menuSceneName);
    }
}
