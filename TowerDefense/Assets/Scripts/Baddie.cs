using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Baddie : MonoBehaviour {
    [Header("Attributes")]
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;//baddie speed
    public int baddieHubris = 50;
    public float health;
    private float startHealth = 100f;
    public GameObject deathEffect;

    [Header("Unity Stuff")]
    public Image healthBar;

    private void Start() {
        speed = startSpeed;
        health = startHealth;
    }

    public void slow(float slowPercent) {
        speed = startSpeed - (startSpeed * slowPercent);//speed=10, slowPercent=0.3, after speed = 10 - (10 * 0.3) = 7
    }

    public void takeDamage(float damage) {
        health -= damage;
        healthBar.fillAmount = health / startHealth;
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
