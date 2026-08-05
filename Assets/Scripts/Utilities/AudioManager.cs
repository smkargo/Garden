using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Source")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip grassGrow;
    [SerializeField] private AudioClip flowerBloom;
    [SerializeField] private AudioClip regionComplete;
    [SerializeField] private AudioClip levelComplete;
    [SerializeField] private AudioClip buttonClick;
    [SerializeField] private AudioClip backgroundMusic;

private void Start()
{
    if (backgroundMusic != null)
    {
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }
}

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
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

    private void PlaySFX(AudioClip clip)
    {
        if (clip == null)
            return;

        sfxSource.PlayOneShot(clip);
    }
}