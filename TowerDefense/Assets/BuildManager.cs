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
    public GameObject turretToBuild { get { return _turretToBuild; } }

    public GameObject baseTurretPrefab;

    private void Start() {
        _turretToBuild = baseTurretPrefab;
    }
}
