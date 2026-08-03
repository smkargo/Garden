using System;

[Serializable]
public class CellData
{
    public int CellId = -1;

    public bool Exists = false;

    public bool IsNumberTile = false;

    public int RegionSize = 0;
}