using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentGroup {

    private int leaderAgent;
    private List<Agent> agents;

    private int maxSize;

	public AgentGroup()
    {
        agents = new List<Agent>();
    }

	// Update is called once per frame
	public void Update () {
		
	}

    public void AddAgent(Agent agent)
    {
        agents.Add(agent);
    }

    public void SelectLeader()
    {
        leaderAgent = 0;
    }

}
