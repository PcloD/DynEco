using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour {

    Slider[] sliders;
    Text[] text;

    HungerManager hungerManager;
    ThirstManager thirstManager;
    AgentController agentController;

    // Use this for initialization
    void Start () {
        sliders = GetComponentsInChildren<Slider>();
        text = GetComponentsInChildren<Text>();
        hungerManager = GetComponentInParent<HungerManager>();
        thirstManager = GetComponentInParent<ThirstManager>();
        agentController = GetComponentInParent<AgentController>();

        sliders[0].maxValue = 100;
        sliders[1].maxValue = 100;
    }
	
	// Update is called once per frame
	void Update () {
        sliders[0].value = hungerManager.WorldProperty.iValue;
        sliders[1].value = thirstManager.WorldProperty.iValue;

        string textToDisplay = "Goals: ";
        foreach(var goal in agentController.GoalWorldState.getAllProperties())
        {
            textToDisplay += goal.Key.ToString() + "\n ";
        }

        text[0].text = textToDisplay;

        string textToDisplayForActions = "Actions: ";
        if (agentController.PlanOfAction != null)
        {
            foreach (var action in agentController.PlanOfAction)
            {
                Debug.Log(agentController.PlanOfAction.Count);
                textToDisplayForActions += action.name + "\n ";
            }
        }

        text[1].text = textToDisplayForActions;


    }
}
