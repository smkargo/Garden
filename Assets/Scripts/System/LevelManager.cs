using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private BoardManager boardManager;
   [SerializeField] private LevelDatabase levelDatabase;

    private int currentLevelIndex;

    public int CurrentLevelIndex => currentLevelIndex;

    private void Start()
{
    LoadLevel(0);
}

    private void Awake()
    {
        Instance = this;
    }

    public void LoadLevel(int index)
{
    if (index < 0 || index >= levelDatabase.Levels.Count)
        return;

    currentLevelIndex = index;

    GameManager.Instance.ResetGame();

    boardManager.LoadLevel(levelDatabase.Levels[index]);
}

    public void RestartLevel()
    {
        LoadLevel(currentLevelIndex);
    }

    public void LoadNextLevel()
    {
        if (currentLevelIndex + 1 < levelDatabase.Levels.Count)
            LoadLevel(currentLevelIndex + 1);
        else
            Debug.Log("No more levels.");
    }
}