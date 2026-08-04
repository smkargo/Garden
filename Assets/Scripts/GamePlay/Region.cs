using System.Collections.Generic;

public class Region
{
    public int Id;

    public NumberTile StartTile;
    public Tile LastTile =>
    Tiles.Count > 0 ? Tiles[Tiles.Count - 1] : null;
    public int TargetSize;

    public RegionState State = RegionState.Inactive;

    public List<Tile> Tiles = new();

    public int CurrentSize => Tiles.Count;

    public bool IsComplete => CurrentSize == TargetSize;

   public void AddTile(Tile tile)
{
    if (Tiles.Contains(tile))
        return;

    Tiles.Add(tile);

    tile.Fill(Id);

    if (IsComplete)
        Complete();
        GameManager.Instance.CheckWin();
}
    public void RemoveTile(Tile tile)
    {
        if (!Tiles.Remove(tile))
            return;

        tile.ClearFill();
    }

   public void Complete()
{
    State = RegionState.Completed;

    foreach (Tile tile in Tiles)
        tile.Complete();

}
    public void Reset()
    {
        foreach (Tile tile in Tiles)
            tile.ClearFill();

        Tiles.Clear();

        State = RegionState.Inactive;
    }
    public bool CanRemoveLastTile(Tile tile)
{
    if (Tiles.Count <= 1)
        return false;

    return LastTile == tile;
}
public void RemoveLastTile()
{
    if (Tiles.Count <= 1)
        return;

    Tile tile = LastTile;

    tile.ClearFill();

    Tiles.RemoveAt(Tiles.Count - 1);

    State = RegionState.Growing;
}
}