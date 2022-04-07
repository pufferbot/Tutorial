using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Quest/Objective Branch Node", fileName = "New Branch Node")]
public class ObjectiveBranchNode : ObjectiveNode
{

    public ObjectiveNode[] outputNodes;
    public int nodeToRun = 0;

    public override ObjectiveNode[] GetNextNodes()
    {
        ObjectiveNode[] _outputNodes = new ObjectiveNode[1];
        _outputNodes[0] = outputNodes[nodeToRun];
        return _outputNodes;
    }

}
