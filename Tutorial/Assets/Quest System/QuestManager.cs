using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    public Quest[] quests;

    //methods for other scripts wanting to know which quests are active
    public Quest[] GetActiveQuests()
    {
        List<Quest> activeQuests = new List<Quest>();
        for(int i = 0; i < quests.Length; i++)
        {
            if(quests[i].questState == Quest.QuestState.Active) 
                activeQuests.Add(quests[i]);
        }
        return activeQuests.ToArray();
    }
    public Quest[] GetCompletedQuests()
    {
        List<Quest> completedQuests = new List<Quest>();
        for (int i = 0; i < quests.Length; i++)
        {
            if (quests[i].questState == Quest.QuestState.Completed)
                completedQuests.Add(quests[i]);
        }
        return completedQuests.ToArray();
    }
    public Quest[] GetFailedQuests()
    {
        List<Quest> failedQuests = new List<Quest>();
        for (int i = 0; i < quests.Length; i++)
        {
            if (quests[i].questState == Quest.QuestState.Failed)
                failedQuests.Add(quests[i]);
        }
        return failedQuests.ToArray();
    }

    public bool SlotEmpty(int index) {
        if (quests[index] == null)
            return true; //triggers if there is no quest

        return false;
    }

    // Get a quest if it exists.
    public bool GetQuest(int index, out Quest _quest) {
        // quests[index] doesn't return null, so check quest instead.
        if (SlotEmpty(index)) {
            _quest = null;
            return false;
        }

        _quest = quests[index];
        return true;
    }
    public void StartQuest()
    {
        quests[0].Begin();
    }

    public void BeginQuest(string questName)
    {
        for(int i = 0; i < quests.Length; i++)
        {
            if(quests[i].questName == questName)
                quests[i].Begin();
            else
                Debug.LogError("No quest with the name " + questName + " was found! Check for typos or improperly deleted quests.");
        }
    }
    public void CompleteQuest(string questName)
    {
        for (int i = 0; i < quests.Length; i++)
        {
            if (quests[i].questName == questName)
                quests[i].Complete();
            else
                Debug.LogError("No quest with the name " + questName + " was found! Check for typos or improperly deleted quests.");
        }
    }
    public void FailQuest(string questName)
    {
        for (int i = 0; i < quests.Length; i++)
        {
            if (quests[i].questName == questName)
                quests[i].Complete();
            else
                Debug.LogError("No quest with the name " + questName + " was found! Check for typos or improperly deleted quests.");
        }
    }

}
