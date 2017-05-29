using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    //passed in by Turret
    private Transform target;
    //bullet speed
    public float speed = 70f;

    public GameObject bulletParticles;

    //setup everythign for the bullet to chase the target
    public void seek(Transform _target) {
        target = _target;
    }
	
	// Update is called once per frame
	void Update () {
		if(target == null) {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        //length of dir is distance to target. if thats less than distancethisframe we've already hit the target
        if(dir.magnitude <= distanceThisFrame) {
            hitTarget();
            return;
        }

        //move as a constant speed
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
	}

    private void hitTarget() {
        //destroy the bullet and spawn the particles
        Destroy(gameObject);

        //spawn and destroy particles
        GameObject effectInst = Instantiate(bulletParticles, transform.position, transform.rotation) as GameObject;
        Destroy(effectInst, 2f);

        //Destroy baddie
        Destroy(target.gameObject);
    }
}
