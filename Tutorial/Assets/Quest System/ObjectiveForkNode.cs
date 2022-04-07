using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Quest/Objective Fork Node", fileName = "New Fork Node")]
public class ObjectiveForkNode : ObjectiveNode
{

    public ObjectiveNode[] outputNodes;

    public override ObjectiveNode[] GetNextNodes()
    {
        return outputNodes;
    }

}
