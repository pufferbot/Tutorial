using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Quest/Objective Tree", fileName = "New Objective Tree")]
public class ObjectiveTree : ScriptableObject
{
    public ObjectiveNode[] nodes;
    public int[] startingNodes;

    public void StartTree()
    {
        for(int i = 0; i < startingNodes.Length; i++)
            nodes[startingNodes[i]].questObjective.Begin();
    }

    public void NodeOutput(ObjectiveNode completedNode) //Run when a node's objective completes, and you want to run its outputs. Input the completed node
    {
        ObjectiveNode[] nodesToRun = completedNode.GetNextNodes(); //checks with the node to see which nodes come next
        foreach (ObjectiveNode node in nodesToRun)
            CheckInputs(node); //checks each to see if it should be started
    }

    public void CheckInputs(ObjectiveNode nodeToRun) //Checks to see if a node's input conditions have been met
    {
        if (nodeToRun.inputType == ObjectiveNode.InputType.Single || nodeToRun.inputType == ObjectiveNode.InputType.Merge) //Both single input and merge input nodes always run
            nodeToRun.questObjective.Begin();
        else if (nodeToRun.inputType == ObjectiveNode.InputType.Join) //Join input nodes will only run if all inputs are complete
        {
            bool allComplete = true;
            foreach (ObjectiveNode node in nodeToRun.inputNodes)
            {
                if (node.questObjective.questState != Quest.QuestState.Completed)
                    allComplete = false; //Checks each input to see if any are incomplete
            }
            if (allComplete) //if none are, then it can run
                nodeToRun.questObjective.Begin();
        }
    }

}
