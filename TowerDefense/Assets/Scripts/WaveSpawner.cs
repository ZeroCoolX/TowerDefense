using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {
    //baddie to spawn
    public Transform baddiePrefab;
    //where to spawn
    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    //seconds until it spawns the first wave
    private float countdown = 2f;
    //current wave
    private int waveIndex = 0;

    public Text waveCountdownText;

    private void Update() {
        //spawn baddies
        if (countdown <= 0) {
            StartCoroutine(spawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;    //amount of time passed since the last time we drew a frame
        //Clamp
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        //update ui
        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }

    private IEnumerator spawnWave() {
        //increment wave index and store rounds
        ++waveIndex;
        ++PlayerStats.roundsSurvived;
        //spawn twice as many baddies as round number
        for (int i = 0; i < waveIndex * 2; ++i) {
            yield return new WaitForSeconds(0.5f);//Baddie spacing
            spawnBaddie();
        }
    }

    private void spawnBaddie() {
        Instantiate(baddiePrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
