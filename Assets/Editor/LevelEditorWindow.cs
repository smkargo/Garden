using UnityEditor;
using UnityEngine;

public class LevelEditorWindow : EditorWindow
{
    private LevelData currentLevel;
    private GridView gridView = new GridView();
    private InspectorPanel inspector = new InspectorPanel();
    private EditorTool currentTool = EditorTool.Paint;
    

    [MenuItem("Garden Fill/Level Editor")]
    public static void ShowWindow()
    {
        GetWindow<LevelEditorWindow>("Level Editor");
    }

    private void OnGUI()
    {
        DrawToolbar();

        GUILayout.Space(10);

        if (currentLevel == null)
        {
            EditorGUILayout.HelpBox("Create or open a level.", MessageType.Info);
            return;
        }

        GUILayout.BeginHorizontal();

        gridView.Draw(currentLevel);

        GUILayout.Space(20);

        inspector.Draw(currentLevel, gridView.SelectedCell);

        GUILayout.EndHorizontal();
    }

   void DrawToolbar()
{
    // File Toolbar
    GUILayout.BeginHorizontal();

    if (GUILayout.Button("New", GUILayout.Height(30)))
        CreateLevelWindow.Open(this);

    if (GUILayout.Button("Open", GUILayout.Height(30)))
        OpenLevel();

    if (GUILayout.Button("Save", GUILayout.Height(30)))
        SaveLevel();

    GUILayout.EndHorizontal();

    GUILayout.Space(5);

    // Tool Toolbar
    currentTool = (EditorTool)GUILayout.Toolbar(
        (int)currentTool,
        new string[]
        {
            "Paint",
            "Erase",
            "Number",
            "Select"
        });

    GUILayout.Space(10);
}
    public void CreateLevel(int width, int height, string assetPath)
    {
        LevelData level = CreateInstance<LevelData>();

        level.LevelName = System.IO.Path.GetFileNameWithoutExtension(assetPath);

        level.Board.Width = width;
        level.Board.Height = height;
        level.Board.Initialize();

        AssetDatabase.CreateAsset(level, assetPath);
        AssetDatabase.SaveAssets();

        currentLevel = level;
    }

    void OpenLevel()
    {
        string path = EditorUtility.OpenFilePanel("Open Level", Application.dataPath, "asset");

        if (string.IsNullOrEmpty(path))
            return;

        path = "Assets" + path.Substring(Application.dataPath.Length);

        currentLevel = AssetDatabase.LoadAssetAtPath<LevelData>(path);

        if (currentLevel != null)
        {
            currentLevel.Board.EnsureValid();
        }
    }

    void SaveLevel()
    {
        if (currentLevel == null)
            return;

        EditorUtility.SetDirty(currentLevel);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    private void OnEnable()
{
    gridView = new GridView();

    gridView.OnCellClicked += HandleCellClick;
    gridView.OnCellDragged += HandleCellDrag;
}
private void HandleCellClick(Vector2Int position)
{
    EditCell(position);
}

private void HandleCellDrag(Vector2Int position)
{
    if (currentTool == EditorTool.Paint ||
        currentTool == EditorTool.Erase)
    {
        EditCell(position);
    }
}
private void EditCell(Vector2Int pos)
{
    if (currentLevel == null)
        return;

    CellData cell = currentLevel.Board.GetCell(pos.x, pos.y);

    if (cell == null)
        return;

    Undo.RecordObject(currentLevel, "Edit Cell");

    switch (currentTool)
    {
        case EditorTool.Paint:
            cell.Exists = true;
            break;

        case EditorTool.Erase:
            cell.Exists = false;
            cell.IsNumberTile = false;
            cell.RegionSize = 0;
            break;

        case EditorTool.Number:
            if (cell.Exists)
            {
                cell.IsNumberTile = true;

                if (cell.RegionSize < 1)
                    cell.RegionSize = 1;
            }
            break;

        case EditorTool.Select:
            // Nothing to modify.
            break;
    }

    EditorUtility.SetDirty(currentLevel);
    AssetDatabase.SaveAssets();
Repaint();
}
}