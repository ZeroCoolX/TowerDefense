using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    //passed in by Turret
    private Transform target;
    //bullet speed
    public float speed = 70f;
    public float explosionRadius = 0f;
    public GameObject bulletParticles;

    public int bulletDamage = 50;

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
        //rotate to face the target
        transform.LookAt(target);
	}

    private void hitTarget() {
        //spawn and destroy particles
        GameObject effectInst = Instantiate(bulletParticles, transform.position, transform.rotation) as GameObject;
       // Debug.Log("creating particles");
     //   effectInst.GetComponent<Renderer>().material.color = target.gameObject.GetComponent<Renderer>().material.color;
      //  Debug.Log("color is: " + effectInst.GetComponent<Renderer>().material.color);
        Destroy(effectInst, 5f);

        if(explosionRadius > 0f) {
            explode();
        }else {
            damage(target);
        }

        //destroy the bullet and spawn the particles
        Destroy(gameObject);
    }

    void explode() {
        //collect a sphere of collisions
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider baddie in colliders) {
            if(baddie.tag == "Baddie") {
                damage(baddie.transform);
            }
        }
    }

    void damage(Transform baddie) {//entire transform
        Baddie baddieScript = baddie.GetComponent<Baddie>();

        if (baddieScript != null) {
            //hurt the enemy
            baddieScript.takeDamage(bulletDamage);
        }
    }

    //draw turret range to see in editor
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
