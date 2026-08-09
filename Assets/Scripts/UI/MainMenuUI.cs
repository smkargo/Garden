using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject settingsPanel;

    private void Start()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    public void Play()
    {
        GameSession.SelectedLevel = 0;
        SceneManager.LoadScene("LevelSelect");
    }

    public void Settings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }
}