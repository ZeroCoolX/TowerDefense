using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Baddie))]
public class BaddieMovement : MonoBehaviour {

    private Baddie baddie;

    private Transform target;
    private int waypointIndex = 0;


    private void Start() {
        baddie = GetComponent<Baddie>();
        target = Waypoints.waypoints[0];
    }

    //move baddie closer and closer
    private void Update() {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * baddie.speed * Time.deltaTime, Space.World);

        //check if we reached waypoint 
        if (Vector3.Distance(transform.position, target.position) <= 0.4f) {
            handleNextWaypoint();
        }

        baddie.speed = baddie.startSpeed;
    }

    //move to next waypoint
    private void handleNextWaypoint() {
        if (waypointIndex >= Waypoints.waypoints.Length - 1) {
            baddie.killDashNine(true);
        } else {
            target = Waypoints.waypoints[++waypointIndex];
        }
    }

}
