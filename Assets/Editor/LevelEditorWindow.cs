using UnityEditor;
using UnityEngine;

public class LevelEditorWindow : EditorWindow
{
    private LevelData level;

    [MenuItem("Garden Fill/Level Editor")]
    public static void Open()
    {
        GetWindow<LevelEditorWindow>("Level Editor");
    }

    private void OnGUI()
    {
        level = (LevelData)EditorGUILayout.ObjectField(
            "Level",
            level,
            typeof(LevelData),
            false);

        if (level == null)
        {
            EditorGUILayout.HelpBox("Assign a LevelData asset.", MessageType.Info);
            return;
        }

        GUILayout.Space(10);

        DrawGrid();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(level);
        }
    }

    private void DrawGrid()
    {
        for (int y = 0; y < level.Height; y++)
        {
            GUILayout.BeginHorizontal();

            for (int x = 0; x < level.Width; x++)
            {
                int index = y * level.Width + x;

                CellData cell = level.Cells[index];

                GUI.backgroundColor = cell.Exists
                    ? new Color(0.55f,0.35f,0.18f)
                    : Color.gray;

                if (GUILayout.Button("", GUILayout.Width(30), GUILayout.Height(30)))
                {
                    cell.Exists = !cell.Exists;

                    if (!cell.Exists)
                    {
                        cell.IsNumberTile = false;
                        cell.RegionSize = 0;
                    }
                }
            }

            GUILayout.EndHorizontal();
        }

        GUI.backgroundColor = Color.white;
    }
}