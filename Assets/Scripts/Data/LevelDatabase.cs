using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDatabase", menuName = "Garden/Level Database")]
public class LevelDatabase : ScriptableObject
{
    public List<LevelData> Levels = new();
}