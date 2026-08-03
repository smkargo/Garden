using UnityEditor;
using UnityEngine;

public class CreateLevelWindow : EditorWindow
{
    private LevelEditorWindow editor;

    private string levelName = "Level_001";
    private int width = 5;
    private int height = 5;

    public static void Open(LevelEditorWindow window)
    {
        CreateLevelWindow popup =
            CreateInstance<CreateLevelWindow>();

        popup.editor = window;

        popup.titleContent = new GUIContent("Create Level");

        popup.minSize = new Vector2(300, 180);

        popup.ShowUtility();
    }

    private void OnGUI()
    {
        GUILayout.Space(10);

        GUILayout.Label("Create New Level", EditorStyles.boldLabel);

        GUILayout.Space(10);

        levelName = EditorGUILayout.TextField("Level Name", levelName);

        width = EditorGUILayout.IntField("Width", width);
        height = EditorGUILayout.IntField("Height", height);

        GUILayout.FlexibleSpace();

        if (GUILayout.Button("Create", GUILayout.Height(35)))
        {
            Create();
        }
    }

    void Create()
    {
        string path = EditorUtility.SaveFilePanelInProject(
            "Save Level",
            levelName,
            "asset",
            "Save Level");

        if (string.IsNullOrEmpty(path))
            return;

        editor.CreateLevel(width, height, path);

        Close();
    }
}