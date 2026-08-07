using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public List<LevelProgress> Levels = new();

    public float MusicVolume = 1f;
    public float SfxVolume = 1f;
}