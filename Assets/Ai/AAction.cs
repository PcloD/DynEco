using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AAction : ScriptableObject
{

    public WorldState requirements;
    public WorldState result;
    public int cost;

    public abstract bool OnDemandRequirements();
    public abstract void ActivateAction(AgentController controller);

    public void Prepare()
    {
        requirements.Init();
        result.Init();
    }
}