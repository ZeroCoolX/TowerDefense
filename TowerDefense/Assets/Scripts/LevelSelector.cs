using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour {

    public SceneFader sceneFader;

    public Button[] levelButtons;

    private void Start() {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        //lock all levels at the beginning
        for (int i = 0; i < levelButtons.Length; ++i) {
            levelButtons[i].interactable = i < levelReached;
        }
    }

    public void select(string levelName) {
        sceneFader.fadeTo(levelName);
    }
}
