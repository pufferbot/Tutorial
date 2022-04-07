using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Quest/Objective Single Node", fileName = "New Single Node")]
public class ObjectiveSingleNode : ObjectiveNode
{

    public ObjectiveNode outputNode;

    public override ObjectiveNode[] GetNextNodes()
    {
        ObjectiveNode[] _outputNodes = new ObjectiveNode[1];
        _outputNodes[0] = outputNode;
        return _outputNodes;
    }

}
