using UnityEngine;

public static class WinChecker
{
    public static bool IsLevelComplete(
        BoardManager board,
        RegionManager regions)
    {
        foreach (Region region in regions.Regions)
        {
            if (!region.IsComplete)
                return false;
        }

        for (int y = 0; y < board.Level.Board.Height; y++)
        {
            for (int x = 0; x < board.Level.Board.Width; x++)
            {
                BoardCell cell = board.GetCell(new Vector2Int(x, y));

                if (cell == null)
                    continue;

                if (cell.Tile.State == TileState.Empty)
                    return false;
            }
        }

        return true;
    }
}