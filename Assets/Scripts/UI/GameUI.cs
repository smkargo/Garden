using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        LevelManager.Instance.RestartLevel();
    }

    public void Levels()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void ToggleSound()
    {
        AudioManager.Instance.ToggleMute();
    }
}