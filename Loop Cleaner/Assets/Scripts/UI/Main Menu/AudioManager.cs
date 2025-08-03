using UnityEngine;

public class AudioManager : IAudioManager
{
    private const string MUSIC_VOLUME_KEY = "MusicVolume";
    private const string SFX_VOLUME_KEY = "SfxVolume";

    public float MusicVolume { get; set; } = 0.5f;
    public float SfxVolume { get; set; } = 0.5f;

    public void SaveAudioSettings()
    {
        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, MusicVolume);
        PlayerPrefs.SetFloat(SFX_VOLUME_KEY, SfxVolume);
        PlayerPrefs.Save();
    }

    public void LoadAudioSettings()
    {
        MusicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 0.5f);
        SfxVolume = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 0.5f);
    }
}