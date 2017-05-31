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
    public Color notEnoughMoneyColor;
    //so it sits perfectly on the node
    public Vector3 posOffset;

    private Renderer rend;

    //turret on this node
    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;


    private void Start() {
        buildManager = BuildManager.instance;

        rend = GetComponent<Renderer>();
        baseColor = rend.material.color;
    }

    public Vector3 getBuildPosition() { 
        return (transform.position + posOffset);
    }

    //user clicks on the node
    private void OnMouseDown() {
        //ensure when ui elelment is over game board only ui elements are selected
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        if(turret != null) {
            buildManager.setNode(this);
            return;
        }

        if (!buildManager.canBuild) {//cannot build there there is already a turret on the node
            return;
        }
        //got past everything, build turret retrieved from Buildmanager
        buildTurret(buildManager.turretToBuild);
    }

    public void buildTurret(TurretBlueprint blueprint) {
        if (PlayerStats.currency < blueprint.cost) {
            return;
        }

        //charge the player
        PlayerStats.currency -= blueprint.cost;

        //Build a turret
        GameObject clone = Instantiate(blueprint.prefab, getBuildPosition(), Quaternion.identity) as GameObject;
        turret = clone;
        turretBlueprint = blueprint;

        //Create build effect
        GameObject effect = Instantiate(buildManager.buildEffect, getBuildPosition(), Quaternion.identity) as GameObject;
        Destroy(effect, 5f);
    }

    public void upgradeTurret() {
        if (PlayerStats.currency < turretBlueprint.upgradeCost) {
            Debug.Log("not enough money to upgrade");
            return;
        }

        //charge the player
        PlayerStats.currency -= turretBlueprint.upgradeCost;

        //remove the current one so we can spawn a BETTER ONE
        Destroy(turret);

        //Build a new turret
        GameObject clone = Instantiate(turretBlueprint.upgradedPrefab, getBuildPosition(), Quaternion.identity) as GameObject;
        turret = clone;

        //Create build effect
        GameObject effect = Instantiate(buildManager.buildEffect, getBuildPosition(), Quaternion.identity) as GameObject;
        Destroy(effect, 5f);

        isUpgraded = true;

        Debug.Log("turret upgraded");
    }

    public void sellTurret() {
        PlayerStats.currency += turretBlueprint.getSellPrice();//just like the irl xD
        //destroy the turret
        Destroy(turret);
        turretBlueprint = null;

        //reset nodes upgraded property
        isUpgraded = false;

        //Create sell effect
        GameObject effect = Instantiate(buildManager.sellEffect, getBuildPosition(), Quaternion.identity) as GameObject;
        Destroy(effect, 5f);
    }

    //let the user know they can interact with this node
    private void OnMouseEnter() {
        //ensure when ui elelment is over game board only ui elements are selected
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        //only show user if there IS a turret to build
        if (!buildManager.canBuild) {
            return;
        }
        if (!buildManager.canAfford) {
            rend.material.color = notEnoughMoneyColor;
        } else {
            rend.material.color = hoverColor;
        }
    }

    //obv
    private void OnMouseExit() {
        rend.material.color = baseColor;
    }
}
