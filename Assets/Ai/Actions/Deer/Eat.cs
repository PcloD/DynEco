using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/DeerEat")]
public class Eat : AAction
{

    public override bool OnDemandRequirements() {
        return false;
    }

    public override void ActivateAction(AgentController controller) {
        Debug.Log("EAT");
        foreach(var resAction in result.getAllProperties())
        {
            controller.ChangeWorldState(resAction.Key, resAction.Value.iValue);
        }
        controller.currentlyDoingAction = false;
    }
}
