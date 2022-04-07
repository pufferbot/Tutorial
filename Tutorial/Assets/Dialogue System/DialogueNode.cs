using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Dialogue Node", fileName = "NewNode.asset")]
public class DialogueNode : ScriptableObject
{
    public string speakText;
    public Reply[] replies;
}
