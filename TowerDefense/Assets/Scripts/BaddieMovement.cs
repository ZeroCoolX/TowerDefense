using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaddieMovement : MonoBehaviour {
    public float speed = 10f;   //baddie speed

    private Transform target;
    private int waypointIndex = 0;

    private void Start() {
        target = Waypoints.waypoints[0];
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
            Destroy(gameObject);
            return;
        } else {
            target = Waypoints.waypoints[++waypointIndex];
        }
    }

}
