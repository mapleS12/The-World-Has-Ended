using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(QuestManager))]
public class QuestManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox(
            "=== QUEST MANAGER ===\n" +
            "• Should exist ONCE in the game, ideally just in Tutorial, since that will always be the start of a New Game.\n" +
            "• Is made persistent automatically (DontDestroyOnLoad). Do not add GameSystems GameObject to any other level, only in tut.\n" +
            "• Keep in same GameSystems GameObject as Region Manager, leave info empty, Region Manager will auto recognize it.\n" +
            "• Tracks active & completed quests.\n" +
            "• Handles quest and objective progression.",
            MessageType.Info
        );

        DrawDefaultInspector();
    }
}
