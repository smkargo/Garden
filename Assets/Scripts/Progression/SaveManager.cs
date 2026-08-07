using System.IO;
using System.Collections.Generic;
using UnityEngine;
public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    private SaveData saveData;

    private string SavePath =>
        Path.Combine(Application.persistentDataPath, "save.json");

    public SaveData Data => saveData;

    private void Awake()
{
    if (Instance != null && Instance != this)
    {
        Destroy(gameObject);
        return;
    }

    Instance = this;
    DontDestroyOnLoad(gameObject);
    Load();   // <-- ADD THIS
}

    public void Save()
    {
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(SavePath, json);
    }

  public void Load()
{
    if (File.Exists(SavePath))
    {
        string json = File.ReadAllText(SavePath);
        saveData = JsonUtility.FromJson<SaveData>(json);
    }

    if (saveData == null)
        saveData = new SaveData();

    if (saveData.Levels == null)
        saveData.Levels = new List<LevelProgress>();

    if (saveData.Levels.Count == 0)
    {
        saveData.Levels.Add(new LevelProgress
        {
            Unlocked = true
        });

        Save();
    }
}
[ContextMenu("Delete Save")]
public void DeleteSave()
{
    if (File.Exists(SavePath))
        File.Delete(SavePath);

    Debug.Log("Save deleted.");
}
    private void EnsureLevelExists(int levelIndex)
    {
        while (saveData.Levels.Count <= levelIndex)
        {
            saveData.Levels.Add(new LevelProgress());
        }
    }

    public bool IsUnlocked(int levelIndex)
    {
        EnsureLevelExists(levelIndex);
        return saveData.Levels[levelIndex].Unlocked;
    }

    public void UnlockLevel(int levelIndex)
    {
        EnsureLevelExists(levelIndex);

        saveData.Levels[levelIndex].Unlocked = true;

        Save();
    }

    public int GetStars(int levelIndex)
    {
        EnsureLevelExists(levelIndex);

        return saveData.Levels[levelIndex].Stars;
    }

    public int GetBestMoves(int levelIndex)
    {
        EnsureLevelExists(levelIndex);

        return saveData.Levels[levelIndex].BestMoves;
    }

    public void SaveResult(int levelIndex, int stars, int moves)
    {
        EnsureLevelExists(levelIndex);

        LevelProgress progress = saveData.Levels[levelIndex];

        progress.Stars = Mathf.Max(progress.Stars, stars);

        if (progress.BestMoves == 0 || moves < progress.BestMoves)
            progress.BestMoves = moves;

        Save();
    }
}