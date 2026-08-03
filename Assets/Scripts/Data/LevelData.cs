using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Garden Fill/Level Data")]
public class LevelData : ScriptableObject
{
    public int Width;
    public int Height;

    public CellData[] Cells;
}