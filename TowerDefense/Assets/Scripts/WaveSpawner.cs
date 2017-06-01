using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {
    //baddies will change this
    public static int baddiesAlive = 0;

    public Wave[] waves;

    //where to spawn
    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    //seconds until it spawns the first wave
    private float countdown = 2f;
    //current wave
    private int waveIndex = 0;

    public Text waveCountdownText;

    private void Update() {
        //check wave beaten
        if (baddiesAlive > 0) {
            return;
        }

            //spawn baddies
            if (countdown <= 0) {
                StartCoroutine(spawnWave());
                countdown = timeBetweenWaves;
                return;
            }
            countdown -= Time.deltaTime;    //amount of time passed since the last time we drew a frame
                                            //Clamp
            countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
            //update ui
            waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }

    private IEnumerator spawnWave() {
        //increment wave index and store rounds
        ++PlayerStats.roundsSurvived;
        //spawn as many baddies as the wave needs
        for (int i = 0; i < waves[waveIndex].baddieCount; ++i) {
            //add an enemy to the stack
            ++baddiesAlive;
            yield return new WaitForSeconds(1f / waves[waveIndex].spawnRate);//Baddie spacing
            spawnBaddie();
        }
        //set for next wave
        ++waveIndex;
        if(waveIndex == waves.Length) {
            Debug.Log("LEVEL WON");
            this.enabled = false;
        }
    }

    private void spawnBaddie() {
        Instantiate(waves[waveIndex].baddie, spawnPoint.position, spawnPoint.rotation);
    }
}
