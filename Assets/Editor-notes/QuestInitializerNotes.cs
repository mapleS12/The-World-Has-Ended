using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(QuestInitializer))]
public class QuestInitializerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox(
            "=== Quest Initializer ===\n" +
            "• Automatically loads region quest upon scene load.\n" +
            "• For now, only used for tutorial, so the tutorial quest loads upon start.\n" +
            "• Do not use if there exists a quest trigger interaction, npc, etc.\n",
            MessageType.Info
        );

        DrawDefaultInspector();
    }
}
