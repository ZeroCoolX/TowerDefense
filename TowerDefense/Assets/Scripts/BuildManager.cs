using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {
    //singleton for nodes to access
    public static BuildManager instance;
    private void Awake() {
        if (instance != null) {
            Debug.LogError("More than one BuildManager - PANIC");
        }
        instance = this;
    }

    private GameObject _turretToBuild;
    public GameObject turretToBuild { get { return _turretToBuild; } set { _turretToBuild = value; } }

    public GameObject baseTurretPrefab;
    public GameObject missileTurretPrefab;
    public GameObject lazerTurretPrefab;

}
