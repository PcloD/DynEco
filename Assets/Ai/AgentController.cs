using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{

    public Agent agent;

    List<AAction> planOfAction;
    ActionPlanner actionPlanner;
    int currentActionIndex;
    public bool currentlyDoingAction;

    float deltaTime;
    float timeRange = .5f;

    WorldState currWorldState;

    WorldState goalWorldState;
    IGoalManager[] needManagers;

    public WorldState GoalWorldState
    {
        get
        {
            return goalWorldState;
        }

        set
        {
            goalWorldState = value;
        }
    }

    public List<AAction> PlanOfAction
    {
        get
        {
            return planOfAction;
        }
    }

    // Use this for initialization
    void Start () {
        currWorldState = new WorldState();
        goalWorldState = new WorldState();
        actionPlanner = GetComponent<ActionPlanner>();
        currentActionIndex = 0;
        currentlyDoingAction = false;

        needManagers = GetComponents<IGoalManager>();
        prepareInitialWorldState();

        GetComponent<MeshRenderer>().material = agent.color;
        transform.name = agent.NameGenerator();
        deltaTime = 0;

        agent.PrepareAllActions();
        for(int i = 0; i < agent.actions.Count; i ++)
        {
            Debug.Log(agent.actions[i].name +" .. " + agent.actions[i].cost + " .. " + agent.actions[i].result.getAllProperties().Count);
        }
    }

    public void prepareInitialWorldState()
    {
        for (int i = 0; i < needManagers.Length; i++)
        {
            currWorldState.AddProperty(needManagers[i].WorldProperty);
        }
    }


    // Update is called once per frame
    void Update () {
        deltaTime += Time.deltaTime;
        if(timeRange < deltaTime)
        {
            deltaTime = 0;
            PassTime();
        }

        VerifyNewGoals();

        if (planOfAction == null && goalWorldState.getNumberOfGoals() > 0)
        {
            planOfAction = DecideNextAction();
            Debug.Log(planOfAction.Count);
        } else if(planOfAction != null)
        {
            ManagerCurrentAction();
        }
    }

    public void PassTime()
    {
        for (int i = 0; i < needManagers.Length; i++)
        {
            needManagers[i].ProgressValue();
        }
    }

    public void ManagerCurrentAction()
    {
        if(currentActionIndex == 0)
        {
            currentActionIndex++;
        }
        else if (currentActionIndex >= planOfAction.Count)
        {
            planOfAction = null;
            currentActionIndex = 0;
        }
        else if (!currentlyDoingAction)
        {
            currentlyDoingAction = true;
            planOfAction[currentActionIndex].ActivateAction(this);
        }
    }

    public List<AAction> DecideNextAction()
    {
        return actionPlanner.GetBestDecision(agent.actions, currWorldState, goalWorldState);
    }

    public void VerifyNewGoals()
    {
        for (int i = 0; i < needManagers.Length; i++)
        {
            if (needManagers[i].CheckTriggered())
            {
                goalWorldState.AddProperty(needManagers[i].GoalTrigger);
            }
        }
    }


    public void ChangeWorldState(EGoals goalKey, int value)
    {
        currWorldState.getGoalFromType(goalKey).iValue = value;
    }

}
