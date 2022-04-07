using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestObjectiveSlot : MonoBehaviour
{
    public int index = 0;
    public QuestObjective questObjective = null; //holds the quest objective info; eg. the name or whether it is complete

    public Image panel;
    public Sprite activeCheckBox;
    public Sprite completeCheckBox;
    public Sprite failedCheckBox;

    public TextMeshProUGUI nameBox;

    public void SetQuestObjective(QuestObjective _questObjective) //setting the slot to display a quest objective
    {
        nameBox.text = _questObjective.objectiveDescription;
        questObjective = _questObjective;
    }

    public void RemoveQuestObjective()  //the slot will clear itself of the quest objective it currently is storing
    {
        this.questObjective= null;
        this.nameBox.text = null;
    }

}
