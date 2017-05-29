using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {
    //singleton for nodes to access
    public static BuildManager instance;

    private TurretBlueprint _turretToBuild;
    public TurretBlueprint turretToBuild { set { _turretToBuild = value; } }

    public GameObject baseTurretPrefab;
    public GameObject missileTurretPrefab;
    public GameObject lazerTurretPrefab;

    public GameObject buildEffect;


    private void Awake() {
        if (instance != null) {
            Debug.LogError("More than one BuildManager - PANIC");
        }
        instance = this;
    }

    public bool canBuild { get { return _turretToBuild != null; } }
    public bool canAfford { get {
            Debug.Log("currency = " + PlayerStats.currency + " and build turred cost = " + _turretToBuild.cost);
            return PlayerStats.currency >= _turretToBuild.cost;
        } }

    public void buildTurretOn(Node node) {

        if(PlayerStats.currency < _turretToBuild.cost) {
            Debug.Log("Not enough currency to build");
            return;
        }

        //charge the player
        PlayerStats.currency -= _turretToBuild.cost;

        //Build a turret
        GameObject clone = Instantiate(_turretToBuild.prefab, node.getBuildPosition(), Quaternion.identity) as GameObject;
        node.turret = clone;
        //Create build effect
        GameObject effect = Instantiate(buildEffect, node.getBuildPosition(), Quaternion.identity) as GameObject;
        Destroy(effect, 5f);
    }

}
