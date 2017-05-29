using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baddie : MonoBehaviour {
    public float speed = 10f;   //baddie speed

    private Transform target;
    private int waypointIndex = 0;
    public int baddieHubris = 50;
    public int health = 100;

    public GameObject deathEffect;

    private void Start() {
        target = Waypoints.waypoints[0];
    }

    public void takeDamage(int damage) {
        health -= damage;
        lifeCheck();
    }

    private void lifeCheck() {
        if(health <= 0) {
            killDashNine(false);
        }
    }

    //move baddie closer and closer
    private void Update() {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        //check if we reached waypoint 
        if(Vector3.Distance(transform.position, target.position) <= 0.4f) {
            handleNextWaypoint();
        }
    }

    //move to next waypoint
    private void handleNextWaypoint() {
        if (waypointIndex >= Waypoints.waypoints.Length - 1) {
            killDashNine(true);
        } else {
            target = Waypoints.waypoints[++waypointIndex];
        }
    }

    void killDashNine(bool damagePlayer) {
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
