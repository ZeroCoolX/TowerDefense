using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour {

    BuildManager buildManager;

    public TurretBlueprint baseTurret;
    public TurretBlueprint missileTurret;

    private void Start() {
        buildManager = BuildManager.instance;
    }


    public void selectBaseTurret() {
        Debug.Log("base turet selected");
        buildManager.turretToBuild = baseTurret;
    }

    public void selectMissileTurret() {
        Debug.Log("missile turet selected");
        buildManager.turretToBuild = missileTurret;
    }

    public void selectLazerTurret() {
        Debug.Log("lazer turet selected");
        //buildManager.turretToBuild = buildManager.lazerTurretPrefab;
    }
}
