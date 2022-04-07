using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveNode : ScriptableObject
{
    public enum InputType
    {
        Start, Single, Merge, Join
    }
    public enum OutputType
    {
        End, Single, Branch, Fork
    }

    public QuestObjective questObjective;

    public CustomNodeBehavior customScript;

    public InputType inputType;
    public OutputType outputType;
    public ObjectiveNode[] inputNodes;

    public virtual ObjectiveNode[] GetNextNodes() //This is only for overriding. DO NOT CALL THIS VERSION!
    {
        return new ObjectiveNode[0];
    }

}
