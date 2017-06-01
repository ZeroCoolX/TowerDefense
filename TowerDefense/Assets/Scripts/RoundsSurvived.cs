using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundsSurvived : MonoBehaviour {

    public Text roundsText;

    private void OnEnable() {
        StartCoroutine(animateText());
    }

    IEnumerator animateText() {
        roundsText.text = "0";
        int round = 0;

        yield return new WaitForSeconds(0.7f);
        while(round < PlayerStats.roundsSurvived) {
            ++round;
            roundsText.text = round.ToString();

            yield return new WaitForSeconds(0.05f);
        }
    }

}
