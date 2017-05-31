using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {
    //singleton for nodes to access
    public static BuildManager instance;

    private TurretBlueprint _turretToBuild;
    public TurretBlueprint turretToBuild { set { _turretToBuild = value; _selectedNode = null; nodeUI.hide(); } get { return _turretToBuild; } }

    private Node _selectedNode;//selecting turret to sell or upgrade

    public GameObject buildEffect;
    public GameObject sellEffect;
    public NodeUI nodeUI;

    private void Awake() {
        if (instance != null) {
            Debug.LogError("More than one BuildManager - PANIC");
        }
        instance = this;
    }

    public bool canBuild { get { return _turretToBuild != null; } }
    public bool canAfford { get { return PlayerStats.currency >= _turretToBuild.cost; } }

    public void setNode(Node node) {
        if(_selectedNode == node) {
            deselectNode();
            return;
        }
        _selectedNode = node;
        _turretToBuild = null;
        nodeUI.setTarget(node);
    }

    public void deselectNode() {
        _selectedNode = null;
        nodeUI.hide();
    }


}
