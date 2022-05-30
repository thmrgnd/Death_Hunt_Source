using UnityEngine;

public class ThemeMusicManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip normalTheme;
    [SerializeField] float normalThemeVolume;
    [SerializeField] AudioClip endgameTheme;
    [SerializeField] float endgameThemeVolume;
    [SerializeField] AudioClip heartbeatTheme;
    [SerializeField] float heartbeatThemeVolume;
    [SerializeField] AudioClip creditsTheme;
    [SerializeField] float creditsThemeVolume;

    float currentVolume;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = normalTheme;
        audioSource.volume = currentVolume = normalThemeVolume;
        audioSource.Play();
        audioSource.loop = true;
    }

    void Update()
    {

    }
    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void ResumeMusic()
    {
        audioSource.UnPause();
    }

    public void SetLowVolume()
    {
        currentVolume = audioSource.volume;
        audioSource.volume = currentVolume / 3;
    }
    public void SetHighVolume()
    {
        audioSource.volume = currentVolume;

    }


    public void PlayEndgameTheme()
    {
        audioSource.clip = endgameTheme;
        audioSource.volume = endgameThemeVolume;
        audioSource.Play();
        audioSource.loop = true;
    }
    public void PlayHeartbeatTheme()
    {
        audioSource.clip = heartbeatTheme;
        audioSource.volume = heartbeatThemeVolume;
        audioSource.Play();
        audioSource.loop = true;
    }
    public void PlayCreditsTheme()
    {
        audioSource.clip = creditsTheme;
        audioSource.volume = creditsThemeVolume;
        audioSource.Play();
        audioSource.loop = true;
    }
}
