using UnityEditor;
using UnityEngine;

public class InspectorPanel
{
    public void Draw(LevelData level, Vector2Int selected)
    {
        GUILayout.BeginVertical(GUILayout.Width(220));

        GUILayout.Label("Selected Cell", EditorStyles.boldLabel);

        if (selected.x < 0)
        {
            EditorGUILayout.HelpBox("No cell selected.", MessageType.Info);
            GUILayout.EndVertical();
            return;
        }

        CellData cell = level.Board.GetCell(selected.x, selected.y);

        GUILayout.Space(5);

        EditorGUI.BeginChangeCheck();

        cell.Exists = EditorGUILayout.Toggle("Exists", cell.Exists);

        cell.IsNumberTile = EditorGUILayout.Toggle("Number Tile", cell.IsNumberTile);

        if (cell.IsNumberTile)
        {
            cell.RegionSize = Mathf.Max(1,
                EditorGUILayout.IntField("Region Size", cell.RegionSize));
        }

        EditorGUILayout.LabelField("Cell ID", cell.CellId.ToString());

        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(level);
        }

        GUILayout.EndVertical();
    }
}