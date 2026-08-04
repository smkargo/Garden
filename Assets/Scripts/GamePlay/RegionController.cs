using UnityEngine;

public class RegionController : MonoBehaviour
{
    private Region activeRegion;

    public Region ActiveRegion => activeRegion;
    public bool HasActiveRegion => activeRegion != null;

    public void BeginRegion(Region region)
{
    if (region == null)
        return;

    if (region.CurrentSize > 1)
        ResetRegion(region);

    activeRegion = region;
}
    public void EndRegion()
    {
        activeRegion = null;
    }

  public void TryAddTile(Tile tile)
{
    if (activeRegion == null)
        return;

    if (tile == null)
        return;

    if (activeRegion.IsComplete)
        return;

    // Can't paint another region.
    if (tile.State != TileState.Empty &&
        tile.RegionId != activeRegion.Id)
        return;

    // Can't enter another region's number tile.
    if (tile is NumberTile numberTile)
    {
        if (numberTile != activeRegion.StartTile)
            return;
    }

    // Backtracking.
    if (activeRegion.Tiles.Count >= 2)
    {
        Tile previous = activeRegion.Tiles[activeRegion.Tiles.Count - 2];

        if (tile == previous)
        {
            activeRegion.RemoveLastTile();
            return;
        }
    }

    // Already in the path.
    if (activeRegion.Tiles.Contains(tile))
        return;

    // Must be adjacent.
    Tile last = activeRegion.LastTile;

    if (last != null)
    {
        Vector2Int diff = tile.GridPosition - last.GridPosition;

        int distance = Mathf.Abs(diff.x) + Mathf.Abs(diff.y);

        if (distance != 1)
            return;
    }

    activeRegion.AddTile(tile);
}
public void ResetRegion(Region region)
{
    if (region == null)
        return;

    region.Reset();

    // Keep the number tile filled.
    region.AddTile(region.StartTile);
}
}