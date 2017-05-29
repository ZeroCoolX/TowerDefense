using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Keeps track things we have built on a node, and handles user input.
//Also checks whether the player has pressed a node, and building on the node
public class Node : MonoBehaviour {

    private Color baseColor;
    public Color hoverColor;
    //so it sits perfectly on the node
    public Vector3 posOffset;

    private Renderer rend;

    private GameObject turret;

    private void Start() {
        rend = GetComponent<Renderer>();
        baseColor = rend.material.color;
    }

    //user clicks on the node
    private void OnMouseDown() {
        if(turret != null) {
            Debug.Log("Can't build here");//TODO: show ui for this
            return;
        }

        //Build a turret
        GameObject buildTurret = BuildManager.instance.turretToBuild;
        turret = Instantiate(buildTurret, transform.position + posOffset, transform.rotation) as GameObject;
    }

    //let the user know they can interact with this node
    private void OnMouseEnter() {
        rend.material.color = hoverColor;
    }

    //obv
    private void OnMouseExit() {
        rend.material.color = baseColor;
    }
}
