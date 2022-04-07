using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReplyOption : MonoBehaviour
{
    public Reply reply;
    public DialogueManager dialogueManager;
    public CharacterStats playerStats;
    public int index = 0;

    public TextMeshProUGUI textBox;

    public void SetReply(Reply _reply)
    {
        reply = _reply;

        //need to show skill checks and such
        string displayText = "";
        if(reply.skillCheck != Reply.SkillCheck.None)
        {
            displayText += "[" + reply.skillCheck.ToString() + " " + reply.checkDC.ToString() + "] ";
        }

        if (reply.skillCheck == Reply.SkillCheck.None) displayText += reply.replyText;
        else
        {
            if (playerStats.CheckSkill(reply.skillCheck, reply.checkDC)) displayText += reply.replyText;
            else displayText += reply.failText;
        }

        textBox.SetText(displayText);
    }

    public void Selected()
    {
        if (dialogueManager != null)
        {
            dialogueManager.ReplySelected(index);
        }
    }
}
