using System;

[Serializable]
public class CellData
{
    public int CellId = -1;

    public bool Exists;

    public bool IsNumberTile;

    public int RegionSize;

    public int RegionId = -1;
}