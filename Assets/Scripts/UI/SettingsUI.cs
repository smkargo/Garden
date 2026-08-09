using UnityEngine;
using TMPro;

public class SettingsUI : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private TMP_Text sfxText;
    [SerializeField] private TMP_Text musicText;

    private void OnEnable()
    {
        UpdateUI();
    }

    public void ToggleSFX()
    {
        if (AudioManager.Instance == null)
            return;

        AudioManager.Instance.ToggleSFX();
        UpdateUI();
    }

    public void ToggleMusic()
    {
        if (AudioManager.Instance == null)
            return;

        AudioManager.Instance.ToggleMusic();
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (AudioManager.Instance == null)
            return;

        if (sfxText != null)
        {
            sfxText.text =
                AudioManager.Instance.IsSFXEnabled()
                ? "SFX: ON"
                : "SFX: OFF";
        }

        if (musicText != null)
        {
            musicText.text =
                AudioManager.Instance.IsMusicEnabled()
                ? "Music: ON"
                : "Music: OFF";
        }
    }
}