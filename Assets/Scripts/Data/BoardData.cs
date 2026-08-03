using System;

[Serializable]
public class BoardData
{
    public int Width = 5;
    public int Height = 5;

    public CellData[] Cells;

    public void Initialize()
    {
        Cells = new CellData[Width * Height];

        for (int i = 0; i < Cells.Length; i++)
        {
            Cells[i] = new CellData();
            Cells[i].CellId = i;
        }
    }

   public CellData GetCell(int x, int y)
{
    if (!BoardUtility.IsInsideBoard(x, y, Width, Height))
        return null;

    if (Cells == null)
        return null;

    int index = BoardUtility.GetIndex(x, y, Width);

    if (index < 0 || index >= Cells.Length)
        return null;

    return Cells[index];
}
public void EnsureValid()
{
    int expected = Width * Height;

    if (Cells == null || Cells.Length != expected)
    {
        Initialize();
    }
}
}