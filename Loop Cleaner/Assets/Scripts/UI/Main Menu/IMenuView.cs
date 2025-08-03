using System;

public interface IMenuView
{
    event Action OnPlayClicked;
    event Action OnSettingsClicked;
    event Action OnExitClicked;
    event Action<float> OnMusicVolumeChanged;
    event Action<float> OnSfxVolumeChanged;
    bool IsSettingsPanelActive();

    void ShowSettingsPanel(bool show);
    void SetMusicVolume(float volume);
    void SetSfxVolume(float volume);
}

// IMenuController.cs
public interface IMenuController
{
    void Initialize();
    void PlayGame();
    void ToggleSettings();
    void ExitGame();
    void SetMusicVolume(float volume);
    void SetSfxVolume(float volume);
}

// ISceneLoader.cs
public interface ISceneLoader
{
    void LoadScene(int sceneIndex);
}

// IAudioManager.cs
public interface IAudioManager
{
    float MusicVolume { get; set; }
    float SfxVolume { get; set; }
    void SaveAudioSettings();
    void LoadAudioSettings();
}