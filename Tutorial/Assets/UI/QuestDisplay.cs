using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestDisplay : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    [SerializeField] GameObject questSlotPrefab;
    public QuestManager questManager;
    public QuestSlot[] questSlots;
    public int selectedQuest = 0;

    [SerializeField] TextMeshProUGUI questNameText;
    [SerializeField] TextMeshProUGUI questDescriptionText;

    [SerializeField] GameObject questObjectiveHolder;
    [SerializeField] GameObject questObjectivePrefab;

    void OnEnable(){
        DisplayQuests();
    }

    private void OnDisable()
    {
        Clear();
    }

    public void DisplayQuests()
    {
        Clear();
        for (int i = 0; i < questManager.GetActiveQuests().Length; i++) //make a prefab for each active quest
        {
            GameObject newSlot = Instantiate(questSlotPrefab, transform);
        }

        questSlots = GetComponentsInChildren<QuestSlot>();

        for (int i = 0; i < questSlots.Length; i++)
        {
            questSlots[i].SetQuest(questManager.GetActiveQuests()[i]);
            if (i == selectedQuest)
                SelectSlot(questSlots[i]);
        }
    }

    public void SelectSlot(QuestSlot _slot)
    {
        for(int i = 0; i < questSlots.Length; i++)
            questSlots[i].Deselect();
        _slot.Select();

        //Set the selected quest display info
        questNameText.SetText ( _slot.quest.questName );
        questDescriptionText.SetText(_slot.quest.questDescription);

        ClearObjectives(); //clears the displayed objectives
        List<QuestObjective> activeObjectives = new List<QuestObjective>(); //sorts the objectives to display so they can be shown seperately
        List<QuestObjective> completedObjectives = new List<QuestObjective>();
        List<QuestObjective> failedObjectives = new List<QuestObjective>();
        for (int i = 0; i < _slot.quest.questObjectives.Length; i++)
        {
            QuestObjective _objective = _slot.quest.questObjectives[i];
            if (_objective.questState == Quest.QuestState.Active)
                activeObjectives.Add(_objective);
            else if (_objective.questState == Quest.QuestState.Completed)
                completedObjectives.Add(_objective);
            else if (_objective.questState == Quest.QuestState.Failed)
                failedObjectives.Add(_objective);
        }

        //display the sorted objectives
        for (int i = 0; i < activeObjectives.Count; i++)
        {
            GameObject newObjectiveSlot = Instantiate(questObjectivePrefab, questObjectiveHolder.transform);
            QuestObjectiveSlot newObjSlot = newObjectiveSlot.GetComponent<QuestObjectiveSlot>();
            newObjSlot.SetQuestObjective(activeObjectives[i]);
            newObjSlot.panel.sprite = newObjSlot.activeCheckBox;
        }
        for (int i = 0; i < completedObjectives.Count; i++)
        {
            GameObject newObjectiveSlot = Instantiate(questObjectivePrefab, questObjectiveHolder.transform);
            QuestObjectiveSlot newObjSlot = newObjectiveSlot.GetComponent<QuestObjectiveSlot>();
            newObjSlot.SetQuestObjective(completedObjectives[i]);
            newObjSlot.panel.sprite = newObjSlot.completeCheckBox;
        }
        for (int i = 0; i < failedObjectives.Count; i++)
        {
            GameObject newObjectiveSlot = Instantiate(questObjectivePrefab, questObjectiveHolder.transform);
            QuestObjectiveSlot newObjSlot = newObjectiveSlot.GetComponent<QuestObjectiveSlot>();
            newObjSlot.SetQuestObjective(failedObjectives[i]);
            newObjSlot.panel.sprite = newObjSlot.failedCheckBox;
        }

    }

    public void Clear()
    {
        foreach (Transform child in transform)
            GameObject.Destroy(child.gameObject);
        questSlots = new QuestSlot[0]; //creates an array with no items
        selectedQuest = 0;
        ClearObjectives();
    }
    public void ClearObjectives()
    {
        foreach (Transform child in questObjectiveHolder.transform)
            GameObject.Destroy(child.gameObject);
    }
}
