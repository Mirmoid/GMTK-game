using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuView : MonoBehaviour, IMenuView
{
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private Scrollbar _musicSlider;
    [SerializeField] private Scrollbar _sfxSlider;

    public event Action OnPlayClicked;
    public event Action OnSettingsClicked;
    public event Action OnExitClicked;
    public event Action<float> OnMusicVolumeChanged;
    public event Action<float> OnSfxVolumeChanged;
    public event Action OnCloseSettingsRequested;

    public bool IsSettingsPanelActive() => _settingsPanel.activeSelf;

    private void Awake()
    {
        // Настройка кнопок
        // (привяжите эти методы к соответствующим кнопкам в инспекторе)
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && IsSettingsPanelActive())
        {
            ShowSettingsPanel(false);
        }
    }

    public void ShowSettingsPanel(bool show)
    {
        _settingsPanel.SetActive(show);
    }

    public void SetMusicVolume(float volume)
    {
        _musicSlider.value = volume;
    }

    public void SetSfxVolume(float volume)
    {
        _sfxSlider.value = volume;
    }

    // Вызывается при нажатии кнопки Play
    public void PlayButtonClick()
    {
        OnPlayClicked?.Invoke();
    }

    // Вызывается при нажатии кнопки Settings
    public void SettingsButtonClick()
    {
        OnSettingsClicked?.Invoke();
    }

    // Вызывается при нажатии кнопки Exit
    public void ExitButtonClick()
    {
        OnExitClicked?.Invoke();
    }

    // Вызывается при изменении ползунка музыки
    public void OnMusicSliderChanged(float value)
    {
        OnMusicVolumeChanged?.Invoke(value);
    }

    // Вызывается при изменении ползунка SFX
    public void OnSfxSliderChanged(float value)
    {
        OnSfxVolumeChanged?.Invoke(value);
    }
}