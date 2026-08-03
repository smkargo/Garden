using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Garden Fill/Level")]
public class LevelData : ScriptableObject
{
    public string LevelName = "Level";

    public BoardData Board = new BoardData();

    [TextArea]
    public string Notes;
}