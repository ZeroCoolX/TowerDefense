using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour {
    public static Transform[] waypoints;

    //find all object under this and store them
    private void Awake() {
        waypoints = new Transform[transform.childCount];
        for(int i = 0; i < waypoints.Length; ++i) {
            waypoints[i] = transform.GetChild(i);
        }
    }
}
