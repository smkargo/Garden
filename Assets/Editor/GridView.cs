using UnityEditor;
using UnityEngine;
using System;

public class GridView
{
    public Action<Vector2Int> OnCellClicked;
    public Action<Vector2Int> OnCellDragged;
    public float CellSize = 40f;

  public Vector2Int SelectedCell { get; private set; } = new(-1, -1);

    public void Draw(LevelData level)
    {
        level.Board.EnsureValid();

        Rect boardRect = GUILayoutUtility.GetRect(
            level.Board.Width * CellSize,
            level.Board.Height * CellSize);

        Event e = Event.current;

        for (int y = 0; y < level.Board.Height; y++)
        {
            for (int x = 0; x < level.Board.Width; x++)
            {
                CellData cell = level.Board.GetCell(x, y);
                if (cell == null)
                continue;
                Rect rect = new Rect(
                    boardRect.x + x * CellSize,
                    boardRect.y + y * CellSize,
                    CellSize,
                    CellSize);

                DrawCell(rect, cell);

                HandleInput(e, rect, cell, x, y);
            }
        }
    }

    void DrawCell(Rect rect, CellData cell)
    {
        Color color = cell.Exists
            ? new Color(0.55f, 0.35f, 0.18f)
            : new Color(0.25f, 0.25f, 0.25f);

        EditorGUI.DrawRect(rect, color);

        Handles.color = Color.black;
        Handles.DrawWireCube(rect.center, rect.size);

        if (cell.IsNumberTile)
        {
            GUIStyle style = new GUIStyle(EditorStyles.boldLabel);
            style.alignment = TextAnchor.MiddleCenter;
            style.normal.textColor = Color.white;

            GUI.Label(rect, cell.RegionSize.ToString(), style);
        }
    }

    void HandleInput(Event e, Rect rect, CellData cell, int x, int y)
    {
        if (!rect.Contains(e.mousePosition))
            return;

        if (e.type == EventType.MouseDown && e.button == 0)
        {
            SelectedCell = new Vector2Int(x, y);

            OnCellClicked?.Invoke(SelectedCell);
            e.Use();
        }

        if (e.type == EventType.MouseDrag && e.button == 0)
        {
            OnCellDragged?.Invoke(new Vector2Int(x, y));

            e.Use();
        }

        if (SelectedCell.x == x && SelectedCell.y == y)
        {
            Handles.color = Color.yellow;
            Handles.DrawWireCube(rect.center, rect.size);
        }
    }

    
}