using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

    List<AgentGroup> allComunities;

    public List<GameObject> prefabAgents;

	// Use this for initialization
	void Start () {
        allComunities = new List<AgentGroup>();

        for (int i = 0; i < prefabAgents.Count; i++) {
            CreateCreature(prefabAgents[i]);
        }
    }

    // Refactor this to not use an Enum to control group size?
    private void CreateCreature(GameObject prefabAgent)
    {
        int size;
        AgentController agentController = prefabAgent.GetComponent<AgentController>();
        Agent prefabProto = agentController.agent;
        if (prefabProto.SocialLevel == SocialLevel.SOLO) {
            size = 1;
        } else if (prefabProto.SocialLevel == SocialLevel.SMALL_GROUPS)
        {
            size = 5;
        }
        else // if (prefabProto.SocialLevel == SocialLevel.BIG_GROUPS)
        {
            size = 1;
        }

        GameObject parentObject = new GameObject(prefabProto.race);
        GameObject newCreature;
        Vector3 spawnPos = GameObject.Find(prefabProto.spawnName).transform.position;
        AgentGroup group = new AgentGroup();

        for (int i = 0; i < size; i++) {
            spawnPos = new Vector3(spawnPos.x + Random.Range(-0.2f, 0.2f), prefabAgent.transform.position.y, spawnPos.z + Random.Range(-0.2f, 0.2f));
            newCreature = Instantiate(prefabAgent, spawnPos, Quaternion.identity);
            newCreature.transform.parent = parentObject.transform;
            group.AddAgent(prefabProto);
        }

        group.SelectLeader();
        allComunities.Add(group);
    }


	// Update is called once per frame
	void Update () {
        for (int i = 0; i < allComunities.Count; i++) {
            allComunities[i].Update();
        }
    }



}

