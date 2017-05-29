using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
    private Transform target;

    [Header("Attributes")]
    public float fireRate = 1f; //1 bullet each second - speed up probably
    private float fireCountdown = 0f;
    public float range = 15f;

    [Header("Unity REquired Fields")]
    public string baddieTag = "Baddie";
    public Transform rotationAxis;
    public float turnSpeed = 10f;

    public GameObject bulletPrefab;
    public Transform firePoint;

	// Use this for initialization
	void Start () {
        InvokeRepeating("updateTarget", 0f, 0.5f);
	}

    //do a renewed search for a target
    void updateTarget() {
        GameObject[] baddies = GameObject.FindGameObjectsWithTag(baddieTag);
        //just to make sure our shortest distance is set
        float shortestDistance = Mathf.Infinity;
        //once we find one
        GameObject nearestBaddie = null;

        foreach (GameObject baddie in baddies) {
            float distanceToBaddie = Vector3.Distance(transform.position, baddie.transform.position);
            //Check if there is one closer
            if(distanceToBaddie < shortestDistance) {
                //update distance and store nearest
                shortestDistance = distanceToBaddie;
                nearestBaddie = baddie;
            }
        }
        
        //found baddie in range
        if(nearestBaddie != null && shortestDistance <= range) {
            target = nearestBaddie.transform;
        }else {
            target = null;
        }
    }
	
	// Update is called once per frame
	void Update () {
        //failsafe
		if(target == null) {
            return;
        }

        //rotate the turrets using Lerp for smooth transition from one to another (not jumping)
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);//how do we need to rotate ourselves to look in the directino "dir"
        Vector3 rotation = Quaternion.Lerp(rotationAxis.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;//convert into X, Y, Z rotation
        rotationAxis.rotation = Quaternion.Euler(0f, rotation.y, 0f);//set the actual rotation

        //fire bullets
        if(fireCountdown <= 0) {
            shoot();
            fireCountdown = 1 / fireRate; //fireRate = per second. so 2 means 2 times a second...etc
        }

        fireCountdown -= Time.deltaTime;
	}

    private void shoot() {
        //spawn bulletS
        GameObject clone = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation) as GameObject;
        Bullet bullet = clone.GetComponent<Bullet>();//get the bullet script off the instantiated object
        if(bullet != null) {
            //set the bullets target to the turrets target
            bullet.seek(target);
        }
    }

    //draw turret range to see in editor
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
