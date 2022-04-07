using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueComponent : InteractComponent
{
    PlayerUI playerUI;
    [SerializeField] DialogueManager dialogueManager;
    public DialogueNode dialogueNode;

    public CustomNodeBehavior[] CustomScripts;

    public override void OnInteract(PlayerStats playerStats = null)
    {
        playerUI = dialogueManager.playerUI;
        playerUI.ToggleDialogue();
        dialogueManager.Speak(this, dialogueNode);
    }

    public void RunScript(int index)
    {
        CustomScripts[index].RunScript();
    }

}
