using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Quest/Quest", fileName = "New Quest")]
public class Quest : ScriptableObject
{
    public enum QuestState
    {
        Inactive, Active, Completed, Failed
    }

    public string questName;
    public string questDescription;

    public int xpReward;
    
    public QuestObjective[] questObjectives;
    public ObjectiveTree objectiveTree;

    public QuestState questState;

    public void CompleteObjective(int objective)
    {
        questObjectives[objective].Complete();
        for (int i = 0; i < objectiveTree.nodes.Length; i++)
            if(objectiveTree.nodes[i].questObjective == questObjectives[objective])
                objectiveTree.NodeOutput(objectiveTree.nodes[i]);
    }

    public void Begin()
    {
        if (questState == QuestState.Inactive)
        {
            questState = QuestState.Active;
            objectiveTree.StartTree();
            Debug.Log("Started quest " + questName + ".");
        }
        else
            Debug.LogError("Quest " + questName + " has already been started.");
    }

    public void Complete()
    {

        if (questState == QuestState.Active)
        {
            questState = QuestState.Completed;
            Debug.Log("Completed quest " + questName + ".");
        }
        else if (questState == QuestState.Inactive)
            Debug.LogError("Quest " + questName + " has not been started yet.");
        else
            Debug.LogError("Quest " + questName + " has already been finished.");
    }

    public void Fail()
    {
        if (questState == QuestState.Active)
        {
            questState = QuestState.Failed;
            Debug.Log("Failed quest " + questName + ".");
        }
        else if (questState == QuestState.Inactive)
            Debug.LogError("Quest " + questName + " has not been started yet.");
        else
            Debug.LogError("Quest " + questName + " has already been finished.");
    }

}