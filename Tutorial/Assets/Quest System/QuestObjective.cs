using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Quest/Quest Objective", fileName = "New Objective")]
public class QuestObjective : ScriptableObject
{
    public string objectiveDescription;
    public int xpReward;
    public Quest.QuestState questState;

    public void Begin()
    {
        if (questState == Quest.QuestState.Inactive)
        {
            questState = Quest.QuestState.Active;
            Debug.Log("Started quest objective " + objectiveDescription + ".");
        }
        else
            Debug.LogError("Quest objective " + objectiveDescription + " has already been started.");
    }

    public void Complete()
    {

        if (questState == Quest.QuestState.Active)
        {
            questState = Quest.QuestState.Completed;
            Debug.Log("Completed quest objective " + objectiveDescription + ".");
        }
        else if (questState == Quest.QuestState.Inactive)
            Debug.LogError("Quest objective " + objectiveDescription + " has not been started yet.");
        else
            Debug.LogError("Quest objective " + objectiveDescription + " has already been finished.");
    }

    public void Fail()
    {
        if (questState == Quest.QuestState.Active)
        {
            questState = Quest.QuestState.Failed;
            Debug.Log("Failed quest objective " + objectiveDescription + ".");
        }
        else if (questState == Quest.QuestState.Inactive)
            Debug.LogError("Quest objective " + objectiveDescription + " has not been started yet.");
        else
            Debug.LogError("Quest objective " + objectiveDescription + " has already been finished.");
    }
}
