using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour {

    public GameObject UI;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) {
            togglePauseMenu();
        }
    }

    public void togglePauseMenu() {
        //enable or disable ui
        UI.SetActive(!UI.activeSelf);

        if (UI.activeSelf) {
            //freeze time
            Time.timeScale = 0f;
        }else {
            Time.timeScale = 1f;
        }
    }

    //restart the game
    public void retry() {
        togglePauseMenu();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void menu() {
        Debug.Log("Go to menu");
    }
}
