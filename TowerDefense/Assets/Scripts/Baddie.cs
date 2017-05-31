using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baddie : MonoBehaviour {
    [Header("Attributes")]
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;//baddie speed
    public int baddieHubris = 50;
    public float health = 100f;
    public GameObject deathEffect;


    private void Start() {
        speed = startSpeed;
    }

    public void slow(float slowPercent) {
        speed = startSpeed - (startSpeed * slowPercent);//speed=10, slowPercent=0.3, after speed = 10 - (10 * 0.3) = 7
    }

    public void takeDamage(float damage) {
        health -= damage;
        lifeCheck();
    }

    private void lifeCheck() {
        if(health <= 0) {
            killDashNine(false);
        }
    }

   public  void killDashNine(bool damagePlayer) {
        if (damagePlayer) {
            //decrement lives
            --PlayerStats.lives;
        }else {
            PlayerStats.currency += baddieHubris;
        }

        //generate effect
        GameObject effect =  Instantiate(deathEffect, transform.position, Quaternion.identity) as GameObject;
        Destroy(effect, 5f);
        //kill baddie
        Destroy(gameObject);
        return;
    }

}
