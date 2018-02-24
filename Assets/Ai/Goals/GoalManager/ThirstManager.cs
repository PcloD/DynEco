using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirstManager : MonoBehaviour, IGoalManager
{
    [SerializeField]
    private int thirstStart;
    [SerializeField]
    private int thirstRate;
    [SerializeField]
    private int thirstThreshold;

    [SerializeField]
    private WorldProperty worldProperty;
    public WorldProperty WorldProperty { get { return worldProperty; } }

    [SerializeField]
    private WorldProperty goalTrigger;
    public WorldProperty GoalTrigger { get {
            return goalTrigger;
        } }

    public void ProgressValue()
    {
        worldProperty.iValue += thirstRate;
    }

    public bool CheckTriggered()
    {
        return worldProperty.iValue > thirstThreshold;
    }

    // Use this for initialization
    void Start () {
        worldProperty = new WorldProperty();
        worldProperty.iValue = thirstStart;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
