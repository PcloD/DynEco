using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/DeerDrink")]
public class Drink : AAction
{

    public override bool OnDemandRequirements()
    {
        return false;
    }

    public override void ActivateAction(AgentController controller)
    {
        Debug.Log("DRINK");
        foreach (var resAction in result.getAllProperties())
        {
            controller.ChangeWorldState(resAction.Key, resAction.Value.iValue);
        }
        controller.currentlyDoingAction = false;
    }
}
