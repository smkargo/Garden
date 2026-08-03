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

    [SerializeField] private float tileSpacing = 1f;

    private BoardCell[,] boardCells;

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

    }

    private void ClearBoard()
    {

    }
}