using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RegionManager))]
public class RegionManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox(
            "=== REGION MANAGER ===\n" +
            "• Define regions by ID.\n" +
            "• Add quests that belong to each region by tagging the quest ScriptableObject.\n" +
            "• Region completes automatically when all quests are done.\n" +
            "• Region can unlock next areas or levels.",
            MessageType.Info
        );

        DrawDefaultInspector();
    }
}
