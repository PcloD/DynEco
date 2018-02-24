using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGoalManager
{
    WorldProperty WorldProperty { get; }
    WorldProperty GoalTrigger { get;}
    void ProgressValue();
    bool CheckTriggered();
}
