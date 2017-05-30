using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
    private Transform target;

    [Header("General")]
    public float range = 15f;

    [Header("Use Bullets (default)")]
    public float fireRate = 1f; //1 bullet each second - speed up probably
    private float fireCountdown = 0f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Use Lazer")]
    public bool useLazer = false;
    public LineRenderer lazerLineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

    [Header("Unity Required Fields")]
    public string baddieTag = "Baddie";
    public Transform rotationAxis;
    public float turnSpeed = 10f;


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
            //check if lazer is firing - if so destroy it
            if(useLazer && lazerLineRenderer.enabled) {
                lazerLineRenderer.enabled = false;
                impactEffect.Stop();//stop particles
                impactLight.enabled = false;//stop light
            }
            return;
        }

        lockOnTarget();


        if (useLazer) {
            lazer();
        }else {
            //fire bullets
            if (fireCountdown <= 0) {
                shoot();
                fireCountdown = 1 / fireRate; //fireRate = per second. so 2 means 2 times a second...etc
            }

            fireCountdown -= Time.deltaTime;
        }
	}


    void lockOnTarget() {
        //rotate the turrets using Lerp for smooth transition from one to another (not jumping)
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);//how do we need to rotate ourselves to look in the directino "dir"
        Vector3 rotation = Quaternion.Lerp(rotationAxis.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;//convert into X, Y, Z rotation
        rotationAxis.rotation = Quaternion.Euler(0f, rotation.y, 0f);//set the actual rotation
    }

    void lazer() {
        if (!lazerLineRenderer.enabled) {
            lazerLineRenderer.enabled = true;
            impactEffect.Play();//play partciles
            impactLight.enabled = true;//play light
        }
        lazerLineRenderer.SetPosition(0, firePoint.position);
        lazerLineRenderer.SetPosition(1, target.position);

        //set particles on baddie
        Vector3 dir = firePoint.position - target.position;//point from baddie back towards turret
        //set position on baddie nbody
        impactEffect.transform.position = target.position + dir.normalized;//0.5f because the baddie size is 1. TODO: generalize this to just use targets scale / 2
        //rotate towards the turret so the particles are like "shooting" off the body
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
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
