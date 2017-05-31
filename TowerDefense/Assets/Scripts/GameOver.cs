using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    public Text roundsText;

    private void OnEnable() {
        roundsText.text = PlayerStats.roundsSurvived.ToString();
    }

    public void retry() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void goToMenu() {
        Debug.Log("go to menu");
    }
}
