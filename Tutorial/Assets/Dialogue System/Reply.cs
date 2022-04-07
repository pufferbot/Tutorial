using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Reply
{
    public string replyText;
    public enum NextAction{
        Node, Quit,
    }

    public enum SkillCheck{
        None, 
        Strength, Melee, Unarmed, 
        Dexterity, Accuracy, Ranged, SleightOfHand, Stealth, 
        Constitution, Defense, Willpower, 
        Intelligence, Crafting, Observation, Magic, Medicine, 
        Charisma, Barter, Deception, Intimidation, Persuasion
    }

    public SkillCheck skillCheck;
    public int checkDC;
    public string failText;
    
    public bool runScript;
    public int scriptToRun;

    public NextAction nextAction;
    public DialogueNode nextNode;

}
