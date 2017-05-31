using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public static int currency;
    public int startCurrency = 400;

    public static int lives;
    public int startLives = 20;

    public static int roundsSurvived;

    void Start() {
        roundsSurvived = 0;
        currency = startCurrency;
        lives = startLives;
    }
}
