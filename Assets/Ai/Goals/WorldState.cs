using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorldState
{
    public WorldProperty[] goals;
    Dictionary<EGoals, WorldProperty> myGoals;

    public void Init()
    {
        myGoals = new Dictionary<EGoals, WorldProperty>();
        if (goals != null)
        {
            for (int i = 0; i < goals.Length; i++)
            {
                myGoals.Add(goals[i].type, goals[i]);
            }
        }
    }

    public WorldState()
    {
        Init();
    }

    public WorldState(WorldState g)
    {
        myGoals = new Dictionary<EGoals, WorldProperty>(g.getAllProperties());
    }

    public override bool Equals(object obj)
    {
        WorldState item = obj as WorldState;

        bool isEqual = true;
        if (item == null)
        {
            return false;
        }

        Dictionary<EGoals, WorldProperty> target = item.getAllProperties();
        if (target.Count == myGoals.Count)
        {
            foreach (var pair in target)
            {
                WorldProperty value;
                if (myGoals.TryGetValue(pair.Key, out value))
                {
                    // Require value be equal.
                    if (value != pair.Value)
                    {
                        isEqual = false;
                        break;
                    }
                }
                else
                {
                    // Require key be present.
                    isEqual = false;
                    break;
                }
            }
        } else
        {
            // Require same size dictionaries;
            isEqual = false;
        }

        return isEqual;
    }

    public int MatchingRequirements(WorldState other)
    {
        Debug.Log("-----------------------------");
        Debug.Log(other.getAllProperties().Count);
        int matches = 0;
        foreach(var goal in other.getAllProperties())
        {
           Debug.Log(goal.Key + " ... " + myGoals.ContainsKey(goal.Key));
           if (myGoals.ContainsKey(goal.Key))
            {
                matches++;
            }
        }
        return matches;
    }

    public int getNumberOfGoals()
    {
        return myGoals == null ? 0 : myGoals.Count;
    }

    public WorldProperty getGoalFromType(EGoals goalType)
    {
        return myGoals[goalType];
    }

    public Dictionary<EGoals, WorldProperty> getAllProperties()
    {
        return myGoals;
    }

    public void AddProperty(WorldState state)
    {
        foreach (var goal in state.getAllProperties())
        {
            if (!myGoals.ContainsKey(goal.Key))
            {
                myGoals.Add(goal.Key, goal.Value);
            }
        }
    }

    public void AddProperty(WorldProperty goal)
    {
        if (!myGoals.ContainsKey(goal.type))
        {
            myGoals.Add(goal.type, goal);
        }
    }

    public void RemoveProperty(WorldState state)
    {
        foreach (var goal in state.getAllProperties())
        {
            if (myGoals.ContainsKey(goal.Key))
            {
                myGoals.Remove(goal.Key);
            }
        }
    }

    public void RemoveProperty(WorldProperty goal)
    {
        if (myGoals.ContainsKey(goal.type))
        {
            myGoals.Remove(goal.type);
        }
    }
}