using UnityEngine;
using UnityEngine.SceneManagement;

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
    public void LevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}