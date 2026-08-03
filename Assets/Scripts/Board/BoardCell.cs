using UnityEngine;

public class BoardCell
{
    public CellData Data;

    public Tile Tile;

    public Vector2Int Coordinate;

    public BoardCell(CellData data, Tile tile, Vector2Int coordinate)
    {
        Data = data;
        Tile = tile;
        Coordinate = coordinate;
    }
}