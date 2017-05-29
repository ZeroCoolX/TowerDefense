using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour {

    BuildManager buildManager;

    private void Start() {
        buildManager = BuildManager.instance;
    }


    public void purchaseBaseTurret() {
        Debug.Log("base turet selected");
        buildManager.turretToBuild = buildManager.baseTurretPrefab;
    }

    public void purchaseMissileTurret() {
        Debug.Log("missile turet selected");
        buildManager.turretToBuild = buildManager.missileTurretPrefab;
    }

    public void purchaseLazerTurret() {
        Debug.Log("lazer turet selected");
        buildManager.turretToBuild = buildManager.lazerTurretPrefab;
    }
}
