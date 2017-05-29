using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public static int currency;
    public int startCurrency = 400;

    void Start() {
        currency = startCurrency;
    }
}
