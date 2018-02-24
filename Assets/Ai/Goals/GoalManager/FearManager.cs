using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearManager : MonoBehaviour, IGoalManager
{
    [SerializeField]
    private bool isAfraid;
    [SerializeField]
    private float distanceFear;
    [SerializeField]
    private List<string> fearOfTags;

    [SerializeField]
    private WorldProperty worldProperty;
    public WorldProperty WorldProperty { get { return worldProperty; } }

    [SerializeField]
    private WorldProperty goalTrigger;
    public WorldProperty GoalTrigger { get { return goalTrigger; } }

    public void ProgressValue()
    {
    }

    public bool CheckTriggered()
    {
        return false;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
