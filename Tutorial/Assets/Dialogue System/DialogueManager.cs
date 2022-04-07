using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public PlayerStats playerStats;
    public PlayerUI playerUI;
    [SerializeField] TextMeshProUGUI dialogueNameText;
    [SerializeField] TextMeshProUGUI dialogueText;

    [SerializeField] GameObject ReplyHolder;
    [SerializeField] GameObject ReplyPrefab;
    public ReplyOption[] replies;

    DialogueComponent dialogueComponent;
    DialogueNode dialogueNode;

    bool isSpeaking = false;

    public void Speak(DialogueComponent _dialogueComponent, DialogueNode _dialogueNode)
    {
        Clear();
        dialogueComponent = _dialogueComponent;
        dialogueNode = _dialogueNode;

        isSpeaking = true;
        dialogueText.SetText(dialogueNode.speakText);
        dialogueNameText.SetText(dialogueComponent.name);
    }

    public void skipSpeak()
    {
        if (isSpeaking)
        {
            isSpeaking = false;
            playerUI.ToggleSpeak();
            DisplayReplies();
        }
    }

    public void DisplayReplies()
    {
        for (int i = 0; i < dialogueNode.replies.Length; i++)
        {
            GameObject newReply = Instantiate(ReplyPrefab, ReplyHolder.transform);
        }

        replies = ReplyHolder.GetComponentsInChildren<ReplyOption>();

        for (int i = 0; i < replies.Length; i++)
        {
            replies[i].playerStats = playerStats;
            replies[i].dialogueManager = this;
            replies[i].index = i;
            replies[i].SetReply(dialogueNode.replies[i]);
        }
    }

    public void ReplySelected(int index)
    {
        if (isSpeaking) return;

        //If it has script behavior
        if (replies[index].reply.runScript) dialogueComponent.RunScript(replies[index].reply.scriptToRun);

        //Either trigger another node or exit
        if (replies[index].reply.nextAction == Reply.NextAction.Node)
        {
            playerUI.speak.SetActive(true);
            playerUI.reply.SetActive(false);
            Speak(dialogueComponent, replies[index].reply.nextNode);
        }
        else if (replies[index].reply.nextAction == Reply.NextAction.Quit)
        {
            playerUI.gameUI.SetActive(true);
            playerUI.gameManager.SetGameState(GameManager.GameState.Running);
            playerUI.mouseLook.ToggleMouseLock();
            playerUI.dialogueInterface.SetActive(false);
        }

    }

    public void Clear()
    {
        foreach (Transform child in ReplyHolder.transform)
            GameObject.Destroy(child.gameObject);
        replies = new ReplyOption[0];
    }

}
