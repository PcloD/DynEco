using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerManager : MonoBehaviour, IGoalManager
{
    [SerializeField]
    private int hungerStart;
    [SerializeField]
    private int hungerRate;
    [SerializeField]
    private int hungerThreshold;

    [SerializeField]
    private WorldProperty worldProperty;
    public WorldProperty WorldProperty { get { return worldProperty; } }

    [SerializeField]
    private WorldProperty goalTrigger;
    public WorldProperty GoalTrigger { get { return goalTrigger; } }

    public void ProgressValue()
    {
        worldProperty.iValue += hungerRate;
    }

    public bool CheckTriggered()
    {
        return worldProperty.iValue > hungerThreshold;
    }

    // Use this for initialization
    void Start () {
        worldProperty = new WorldProperty();
        worldProperty.iValue = hungerStart;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
