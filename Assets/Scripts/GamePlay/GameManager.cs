using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private RegionManager regionManager;
    public bool IsGameCompleted { get; private set; }
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

  public void InitializeGame()
{
    // Build all regions first
    regionManager.Build(boardManager);

    int perfectMoves = 0;

    foreach (Region region in regionManager.Regions)
    {
        perfectMoves += region.TargetSize - 1;
    }

    MoveManager.Instance.Initialize(perfectMoves);
}

    public void CheckWin()
{
     if (IsGameCompleted)
        return;

    foreach (Region region in regionManager.Regions)
    {
        if (!region.IsComplete)
            return;
    }

    IsGameCompleted = true;
    if (LevelCompleteUI.Instance == null)
{
    return;
}
    LevelCompleteUI.Instance.Show();
}
public void ResetGame()
{
    IsGameCompleted = false;

    if (LevelCompleteUI.Instance != null)
        LevelCompleteUI.Instance.Hide();
}
}