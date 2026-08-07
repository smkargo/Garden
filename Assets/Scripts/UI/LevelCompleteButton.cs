using UnityEngine;

public class LevelCompleteButton : MonoBehaviour
{
    public void RetryLevel()
    {
        LevelManager.Instance.RestartLevel();
    }

    public void NextLevel()
    {
        LevelManager.Instance.LoadNextLevel();
    }
}