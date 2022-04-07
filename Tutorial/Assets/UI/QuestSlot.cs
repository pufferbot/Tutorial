using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestSlot : MonoBehaviour
{
    public int index = 0;
    public Quest quest = null; //holds the quest and info; eg. the objective tree or whether it is complete

    public Image panel;
    public Color notSelectedColor;
    public Color selectedColor;

    public TextMeshProUGUI nameBox;

    public void SetQuest(Quest _quest) //setting the slot to display a quest
    {
        nameBox.text = _quest.questName;
        quest = _quest;

        quest = _quest;
    }

    public void RemoveQuest()  //the slot will clear itself of the quest it currently is storing
    {
        this.quest = null;
        this.nameBox.text = null;
    }

    public void Selected()
    {
        if (GetComponentInParent<QuestDisplay>())
        {
            GetComponentInParent<QuestDisplay>().SelectSlot(this);
        }
    }

    public void Select()
    {
        panel.color = selectedColor;
    }

    public void Deselect()
    {
        panel.color = notSelectedColor;
    }
}
