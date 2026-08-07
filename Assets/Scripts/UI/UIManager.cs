using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Gameplay")]
    [SerializeField] private LevelCompleteUI levelCompleteUI;

    [Header("Pause")]
    [SerializeField] private GameObject pausePanel;

    [Header("Settings")]
    [SerializeField] private GameObject settingsPanel;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowLevelComplete()
    {
        levelCompleteUI.Show();
    }

    public void HideLevelComplete()
    {
        levelCompleteUI.Hide();
    }

    public void ShowPause()
    {
        if (pausePanel != null)
            pausePanel.SetActive(true);
    }

    public void HidePause()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    public void ShowSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    public void HideSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }
}