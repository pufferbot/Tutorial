using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PlayerStats))]
public class PlayerStatsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PlayerStats myScript = (PlayerStats)target;
        if (GUILayout.Button("Equip Test Item"))
            myScript.EquipItem(myScript.testItem);
    }
}
