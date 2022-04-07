using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(QuestManager))]
public class QuestManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        QuestManager myScript = (QuestManager)target;
        if (GUILayout.Button("Start Quest"))
            myScript.StartQuest();
        if (GUILayout.Button("Complete Objective"))
            myScript.quests[0].CompleteObjective(0);
    }

}
