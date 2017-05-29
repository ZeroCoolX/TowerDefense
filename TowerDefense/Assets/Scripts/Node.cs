using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Keeps track things we have built on a node, and handles user input.
//Also checks whether the player has pressed a node, and building on the node
public class Node : MonoBehaviour {

    BuildManager buildManager;

    private Color baseColor;
    public Color hoverColor;
    //so it sits perfectly on the node
    public Vector3 posOffset;

    private Renderer rend;

    private GameObject turret;

    private void Start() {
        buildManager = BuildManager.instance;

        rend = GetComponent<Renderer>();
        baseColor = rend.material.color;
    }

    //user clicks on the node
    private void OnMouseDown() {
        //ensure when ui elelment is over game board only ui elements are selected
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        if (buildManager.turretToBuild == null) {
            return;
        }

        if(turret != null) {
            Debug.Log("Can't build here");//TODO: show ui for this
            return;
        }

        //Build a turret
        GameObject buildTurret = buildManager.turretToBuild;
        turret = Instantiate(buildTurret, transform.position + posOffset, transform.rotation) as GameObject;
    }

    //let the user know they can interact with this node
    private void OnMouseEnter() {
        //ensure when ui elelment is over game board only ui elements are selected
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        //only show user if there IS a turret to build
        if (buildManager.turretToBuild == null) {
            return;
        }

        rend.material.color = hoverColor;
    }

    //obv
    private void OnMouseExit() {
        rend.material.color = baseColor;
    }
}
