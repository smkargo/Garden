using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private BoardManager boardManager;
    [SerializeField] private RegionManager regionManager;

    public bool IsGameCompleted { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void InitializeGame()
    {
        regionManager.Build(boardManager);

        MoveManager.Instance.Initialize(regionManager.Regions);

        IsGameCompleted = false;
    }

    public void CheckWin()
    {
        if (IsGameCompleted)
            return;

        if (!regionManager.AreAllRegionsCompleted())
            return;

        IsGameCompleted = true;

        int level = LevelManager.Instance.CurrentLevelIndex;
        int stars = MoveManager.Instance.GetStars();
        int moves = MoveManager.Instance.CurrentMoves;

        SaveManager.Instance.SaveResult(level, stars, moves);

        // Unlock the next level
        SaveManager.Instance.UnlockLevel(level + 1);
        UIManager.Instance.ShowLevelComplete();
            }

    public void ResetGame()
    {
        IsGameCompleted = false;
        UIManager.Instance.HideLevelComplete();
            }
}