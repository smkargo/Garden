using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void Play()
    {GameSession.SelectedLevel = 0;
SceneManager.LoadScene("LevelSelect");
    }

    public void Settings()
    {
        UIManager.Instance.ShowSettings();
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}