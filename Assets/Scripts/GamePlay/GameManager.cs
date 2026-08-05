using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private RegionManager regionManager;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

   public void InitializeGame()
{
    regionManager.Build(boardManager);
}

    public void CheckWin()
    {
        if (!WinChecker.IsLevelComplete(boardManager, regionManager))
            return;

        Debug.Log("LEVEL COMPLETE");
    }
}