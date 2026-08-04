using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [Header("Level")]
    [SerializeField] private LevelData level;

    [Header("Prefabs")]
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private NumberTile numberTilePrefab;

    [Header("Board")]
    [SerializeField] private Transform boardRoot;
    [SerializeField] private Transform tileRoot;
    [SerializeField] private Transform effectRoot;
    [SerializeField] private float tileSpacing = 1f;
    [SerializeField]private RegionManager regionManager;
    private BoardCell[,] boardCells;
    public LevelData Level => level;
    private void Start()
    {
        if (level != null)
            LoadLevel(level);
    }

    public void LoadLevel(LevelData levelData)
    {
        level = levelData;

        ClearBoard();
        CreateBoard();
    }

    private void CreateBoard()
    {
        level.Board.EnsureValid();

        boardCells = new BoardCell[level.Board.Width, level.Board.Height];

        for (int y = 0; y < level.Board.Height; y++)
        {
            for (int x = 0; x < level.Board.Width; x++)
            {
                CellData cell = level.Board.GetCell(x, y);

                if (cell == null || !cell.Exists)
                    continue;

                SpawnCell(cell, x, y);
            }
        }
        BuildBoardSkin();
        CenterBoard();
        regionManager.Build(this);
    }
    public BoardCell GetCell(Vector2Int position)
{
    if (!BoardUtility.IsInsideBoard(position.x, position.y,
        level.Board.Width, level.Board.Height))
        return null;

    return boardCells[position.x, position.y];
}

    private void SpawnCell(CellData data, int x, int y)
{
  
    Vector3 localPosition = new Vector3(
        x * tileSpacing,
        -y * tileSpacing,
        0f);

    Tile tile;

    if (data.IsNumberTile)
    {
        NumberTile numberTile = Instantiate(numberTilePrefab, tileRoot);
        numberTile.transform.localPosition = localPosition;
        numberTile.Initialize(new Vector2Int(x, y));
        numberTile.SetRegionSize(data.RegionSize);

        tile = numberTile;
    }
    else
    {
        tile = Instantiate(tilePrefab, tileRoot);
        tile.transform.localPosition = localPosition;
        tile.Initialize(new Vector2Int(x, y));
    }

    boardCells[x, y] = new BoardCell(
        data,
        tile,
        new Vector2Int(x, y));
}
   private void ClearBoard()
{
    if (tileRoot == null)
        return;

    for (int i = tileRoot.childCount - 1; i >= 0; i--)
    {
        Destroy(tileRoot.GetChild(i).gameObject);
    }

    if (effectRoot != null)
    {
        for (int i = effectRoot.childCount - 1; i >= 0; i--)
        {
            Destroy(effectRoot.GetChild(i).gameObject);
        }
    }
}
private void CenterBoard()
{
    float width = (level.Board.Width - 1) * tileSpacing;
    float height = (level.Board.Height - 1) * tileSpacing;

    boardRoot.localPosition = new Vector3(
        -width * 0.5f,
         height * 0.5f,
         0f);
}
private void BuildBoardSkin()
{
    for (int y = 0; y < level.Board.Height; y++)
    {
        for (int x = 0; x < level.Board.Width; x++)
        {
            BoardCell cell = boardCells[x, y];

            if (cell == null)
                continue;

            UpdateBorders(cell);
        }
    }
}
private void UpdateBorders(BoardCell cell)
{
    TileBorder border = cell.Tile.Border;

    border.HideAll();

    bool up    = HasTile(cell.Coordinate + Vector2Int.up);
    bool down  = HasTile(cell.Coordinate + Vector2Int.down);
    bool left  = HasTile(cell.Coordinate + Vector2Int.left);
    bool right = HasTile(cell.Coordinate + Vector2Int.right);

    border.SetTop(!up);
    border.SetBottom(!down);
    border.SetLeft(!left);
    border.SetRight(!right);

    border.SetTopLeft(!up && !left);
    border.SetTopRight(!up && !right);
    border.SetBottomLeft(!down && !left);
    border.SetBottomRight(!down && !right);
}
private bool HasTile(Vector2Int position)
{
    if (!BoardUtility.IsInsideBoard(
        position.x,
        position.y,
        level.Board.Width,
        level.Board.Height))
        return false;

    return boardCells[position.x, position.y] != null;
}

}