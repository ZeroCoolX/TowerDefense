using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour {

    private Node _target;
    public GameObject UI;

    public Text upgradeCost;
    public Button upgradeButton;

    public Text sellCost;

    public void setTarget(Node target) {
        //store target
        _target = target;
        //set UI position
        transform.position = target.getBuildPosition();//get buildpos takes the center of the object and offsets by 0.5

        upgradeButton.interactable = !_target.isUpgraded;
        if (!_target.isUpgraded) {
            upgradeCost.text = "$" + _target.turretBlueprint.upgradeCost;
        }else {
            upgradeCost.text = "UPGRADE MAXED";
        }

        sellCost.text = "$" + _target.turretBlueprint.getSellPrice().ToString();

        //show ui
        UI.SetActive(true);


    }

    public void hide() {
        UI.SetActive(false);
    }

    public void upgrade() {
        //upgrade turret
        _target.upgradeTurret();
        //close menu
        BuildManager.instance.deselectNode();
    }

    public void sell() {
        //sell turret
        _target.sellTurret();
        //close menu
        BuildManager.instance.deselectNode();
    }
}
