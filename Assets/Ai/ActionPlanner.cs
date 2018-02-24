using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPlanner : MonoBehaviour {

    List<Node> closedNodes;
    List<Node> openNodes;
    List<Node> bestNodes;

    void Start()
    {
        closedNodes = new List<Node>();
        openNodes = new List<Node>();
        bestNodes = new List<Node>();
    }

	public List<AAction> GetBestDecision (List<AAction> actions, WorldState start, WorldState finish) {
        List<AAction> bestActions = new List<AAction>();

        Node currentNode = new Node(finish);

        //Prepare the graph that will be traveled.
        openNodes.Clear();
        closedNodes.Clear();
        openNodes.Add(currentNode);
        int loop = 0, max = 1000;

        /*
            While (there are more open nodes)
                __Select a node from open nodes with lowest cost
                __Check if the world states in this node is a subset of current world states; if so, planning is done.
                __Move the node from open nodes to closed nodes
                __For each action from available actions
                ____Check if action satisfies any of the unsatisfied conditions and if context precondtions check passes. If not the case, move on to next action
                ____Move the action’s effects from unsatisfied conditions to satisfied conditions
                ____Add the action’s preconditions to unsatisfied conditions
                ____If the combination of unsatisfied/satisfied conditions exist in open nodes then discard this action if it costs more than other paths
                ____Otherwise create a new open node and record the accumulated cost and the selected action
                If planning is successful, generate a path of actions based on the chain of “come from action”
        */
        Debug.Log("Number of actions " + actions.Count);
        while (loop < max && openNodes.Count > 0) {
            loop++;
            int minFval = int.MaxValue,
                minIndex = 0;

            //Select a node from open nodes with lowest cost
            //change this to a collection that orders it by cost from the get go
            for (int i = 0; i < openNodes.Count; i++)
            {
                if(minFval < openNodes[i].combinedCost)
                {
                    minIndex = i;
                    minFval = openNodes[i].combinedCost;
                }
            }

            currentNode = openNodes[minIndex];
            //remove from open
            openNodes.RemoveAt(minIndex);
            //add to closed
            closedNodes.Add(currentNode);

            Debug.Log(currentNode.Equals(start));
            //Check if the world states in this node is a subset of current world states; if so, planning is done.
            if (currentNode.Equals(start))
            {
                Debug.Log("Reached start, end planning");
                break;
            }

            //For each action from available actions
            for (int i = 0; i < actions.Count; i++) {
                Debug.Log("Check action" + actions[i].name);
                if (isActionAlreadyClosed(actions[i])) break;
                Debug.Log("Is not in closed list");
                //Check if action satisfies any of the unsatisfied conditions and if context precondtions check passes. If not the case, move on to next action
                Debug.Log(actions[i].result.getAllProperties().Count);
                int matchingReq = currentNode.currentWorldState.MatchingRequirements(actions[i].result);
                Debug.Log("matchingReq " + matchingReq);
                if (matchingReq == 0) break;
                Debug.Log("helps fixing a requirement");
                //Add the action’s preconditions to unsatisfied conditions
                WorldState worldState = new WorldState(currentNode.currentWorldState);
                worldState.RemoveProperty(actions[i].result);
                worldState.AddProperty(actions[i].requirements);
                //Calculate cost
                int heuristicCost = currentNode.currentWorldState.getAllProperties().Count - matchingReq;
                int totalCost = actions[i].cost + heuristicCost;
                //If the combination of unsatisfied/satisfied conditions exist in open nodes then discard this action if it costs more than other paths
                //if thisCost + currentCost < 
                //Otherwise create a new open node and record the accumulated cost and the selected action
                Node newNode = new Node(worldState, actions[i], totalCost + currentNode.accCost);
                openNodes.Add(newNode);
            }

        }

        // If planning is successful, generate a path of actions based on the chain of “come from action”

        Node node = closedNodes[closedNodes.Count - 1];
        while (node != null)
        {
            bestActions.Add(node.action);
            node = node.ParentNode;
        }

        Debug.Log("Number of best actions: " + bestActions.Count);
        return bestActions;
    }


    public bool isActionAlreadyClosed(AAction action)
    {
        for (int j = 0; j < closedNodes.Count; j++)
        {
            if (closedNodes[j].action != null && closedNodes[j].action.Equals(action))
            {
                return true;
            }
        }
        return false;
    }


}


public class Node
{
    public int accCost;
    public int heuristicCost;
    public int combinedCost;

    Node parentNode;
    public Node ParentNode { get { return parentNode; } }
    public WorldState currentWorldState;
    public AAction action;

    public Node(WorldState currentWP, AAction action, int accCost)
    {
        this.currentWorldState = currentWP;
        this.action = action;
        this.accCost = accCost;
        heuristicCost = 0;
    }

    public Node(WorldState currentWP)
    {
        this.currentWorldState = currentWP;
        accCost = 0;
        heuristicCost = 0;
    }

}
