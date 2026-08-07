using System.Collections.Generic;
using UnityEngine;

public class RegionManager : MonoBehaviour
{
    private readonly List<Region> regions = new();
    public IReadOnlyList<Region> Regions => regions;
    private int lastFlowerType = -1;
    [SerializeField] private int flowerTypeCount = 3;
    public void Clear()
{
    regions.Clear();
}
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
                region.FlowerType = GetRandomFlowerType();
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
private int GetRandomFlowerType()
{
    if (lastFlowerType == -1)
    {
        lastFlowerType = Random.Range(0, flowerTypeCount);
        return lastFlowerType;
    }

    int flowerType;

    do
    {
        flowerType = Random.Range(0, flowerTypeCount);
    }
    while (flowerType == lastFlowerType);

    lastFlowerType = flowerType;
    return flowerType;
}
public bool AreAllRegionsCompleted()
{
    foreach (Region region in regions)
    {
        if (!region.IsComplete)
            return false;
    }

    return true;
}
public Region GetRegion(int id)
{
    foreach (Region region in regions)
    {
        if (region.Id == id)
            return region;
    }

    return null;
}

public int RegionCount => regions.Count;

public void ResetAllRegions()
{
    foreach (Region region in regions)
    {
        region.Reset();
        region.AddTile(region.StartTile);
    }
}
}