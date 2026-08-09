using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private bool sfxEnabled = true;
    private bool musicEnabled = true;
    private bool muted;
    private const string SFX_KEY = "SFXEnabled";
    private const string MUSIC_KEY = "MusicEnabled";

    [Header("Audio Source")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip grassGrow;
    [SerializeField] private AudioClip flowerBloom;
    [SerializeField] private AudioClip regionComplete;
    [SerializeField] private AudioClip levelComplete;
    [SerializeField] private AudioClip buttonClick;
    [SerializeField] private AudioClip starReveal;

    [Header("Music")]
    [SerializeField] private AudioClip backgroundMusic;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadSettings();
    }

    private void Start()
    {
        SetupMusic();
    }
    public void ToggleMute()
    {
    muted = !muted;

    AudioListener.volume = muted ? 0f : 1f;
    }
    private void LoadSettings()
    {
        sfxEnabled =
            PlayerPrefs.GetInt(SFX_KEY, 1) == 1;

        musicEnabled =
            PlayerPrefs.GetInt(MUSIC_KEY, 1) == 1;
    }
    private void SetupMusic()
    {
        if (musicSource == null)
            return;

        if (backgroundMusic == null)
            return;

        musicSource.clip = backgroundMusic;
        musicSource.loop = true;

        if (musicEnabled)
        {
            musicSource.Play();
        }
    }

    public void ToggleMusic()
    {
        musicEnabled = !musicEnabled;

        PlayerPrefs.SetInt(
            MUSIC_KEY,
            musicEnabled ? 1 : 0
        );

        PlayerPrefs.Save();

        if (musicSource == null)
            return;

        if (musicEnabled)
        {
            if (!musicSource.isPlaying)
                musicSource.Play();
        }
        else
        {
            musicSource.Stop();
        }
    }

    public bool IsMusicEnabled()
    {
        return musicEnabled;
    }

    public void ToggleSFX()
    {
        sfxEnabled = !sfxEnabled;

        PlayerPrefs.SetInt(
            SFX_KEY,
            sfxEnabled ? 1 : 0
        );

        PlayerPrefs.Save();
    }

    public bool IsSFXEnabled()
    {
        return sfxEnabled;
    }

    public void PlayGrass()
    {
        PlaySFX(grassGrow);
    }

    public void PlayFlower()
    {
        PlaySFX(flowerBloom);
    }

    public void PlayRegionComplete()
    {
        PlaySFX(regionComplete);
    }

    public void PlayLevelComplete()
    {
        PlaySFX(levelComplete);
    }

    public void PlayButtonClick()
    {
        PlaySFX(buttonClick);
    }

    public void PlayStarReveal()
    {
        PlaySFX(starReveal);
    }
    private void PlaySFX(AudioClip clip)
    {
        if (!sfxEnabled)
            return;

        if (clip == null)
            return;

        if (sfxSource == null)
            return;

        sfxSource.PlayOneShot(clip);
    }
}