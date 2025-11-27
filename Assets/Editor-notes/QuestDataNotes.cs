using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(QuestData))]
public class QuestDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox(
            "=== QUEST SETUP ===\n" +
            "• questID must be unique.\n" +
            "• regionID must match a RegionManager entry.\n" +
            "• Add 1+ objectives.\n" +
            "• Quest status is auto-handled by QuestManager.\n" +
            "\n" +
            "Usage:\n" +
            "This ScriptableObject defines a single quest. You should attach this quest to a specific level in the GameSystem object.\n Assign it to a QuestGiver/Trigger to initiate quest.",
            MessageType.Info
        );

        DrawDefaultInspector();
    }
}
