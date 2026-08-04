using System.Collections.Generic;
using UnityEngine;

public class RegionManager : MonoBehaviour
{
    private readonly List<Region> regions = new();

    public IReadOnlyList<Region> Regions => regions;

    public void Build(BoardManager board)
    {
        regions.Clear();

        int id = 0;

        for (int y = 0; y < board.Level.Board.Height; y++)
        {
            for (int x = 0; x < board.Level.Board.Width; x++)
            {
                BoardCell cell = board.GetCell(new Vector2Int(x, y));

                if (cell == null)
                    continue;

                if (cell.Tile is not NumberTile numberTile)
                    continue;

                Region region = new Region();

                region.Id = id++;
                region.StartTile = numberTile;
                region.TargetSize = numberTile.TargetSize;

                region.AddTile(numberTile);

                regions.Add(region);
            }
        }
    }
    public Region GetRegion(NumberTile tile)
{
    foreach (Region region in regions)
    {
        if (region.StartTile == tile)
            return region;
    }

    return null;
}
}