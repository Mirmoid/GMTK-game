public class MenuController : IMenuController
{
    private readonly IMenuView _view;
    private readonly ISceneLoader _sceneLoader;
    private readonly IAudioManager _audioManager;

    public MenuController(IMenuView view, ISceneLoader sceneLoader, IAudioManager audioManager)
    {
        _view = view;
        _sceneLoader = sceneLoader;
        _audioManager = audioManager;
    }

    public void Initialize()
    {
        _view.OnPlayClicked += PlayGame;
        _view.OnSettingsClicked += ToggleSettings;
        _view.OnExitClicked += ExitGame;
        _view.OnMusicVolumeChanged += SetMusicVolume;
        _view.OnSfxVolumeChanged += SetSfxVolume;

        // Загружаем настройки звука
        _audioManager.LoadAudioSettings();
        _view.SetMusicVolume(_audioManager.MusicVolume);
        _view.SetSfxVolume(_audioManager.SfxVolume);
    }

    public void PlayGame()
    {
        _sceneLoader.LoadScene(1);
    }

    public void ToggleSettings()
    {
        bool isActive = !_view.IsSettingsPanelActive();
        _view.ShowSettingsPanel(isActive);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void SetMusicVolume(float volume)
    {
        _audioManager.MusicVolume = volume;
        _audioManager.SaveAudioSettings();
    }

    public void SetSfxVolume(float volume)
    {
        _audioManager.SfxVolume = volume;
        _audioManager.SaveAudioSettings();
    }
}