using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class EyeButtonInteractable : InteractableBase
{
    [Header("Eye Settings")]
    [SerializeField] private AudioClip _activationSound;
    [SerializeField] private GameObject _activationLight; // —сылка на Point Light

    [Header("System References")]
    [SerializeField] private GameObject _objectToDisable;
    [SerializeField] private GameObject _waveManager;
    [SerializeField] private GameObject _rainManager;

    [Header("UI Settings")]
    [SerializeField] private Text _counterText;

    private AudioSource _audioSource;
    private bool _isActivated = false;
    private static int _activatedEyesCount = 0;
    private static int _totalEyesCount = 2;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        InitializeEye();
    }

    private void InitializeEye()
    {
        _audioSource.playOnAwake = false;

        // ƒеактивируем свет при старте
        if (_activationLight != null)
        {
            _activationLight.SetActive(false);
        }

        UpdateCounterUI();
    }

    public override void Interact()
    {
        if (!_isActivated)
        {
            ActivateEye();
            CheckBothEyesActivated();
        }
    }

    private void ActivateEye()
    {
        _isActivated = true;
        _activatedEyesCount++;

        // јктивируем свет и проигрываем звук
        if (_activationLight != null)
        {
            _activationLight.SetActive(true);
        }

        if (_activationSound != null)
        {
            _audioSource.PlayOneShot(_activationSound);
        }

        UpdateCounterUI();
    }

    private void UpdateCounterUI()
    {
        if (_counterText != null)
        {
            _counterText.text = $"{_activatedEyesCount}/{_totalEyesCount}";
        }
    }

    private void CheckBothEyesActivated()
    {
        if (_activatedEyesCount >= _totalEyesCount)
        {
            DisableTargetObject();
            DisableWeatherSystems();
        }
    }

    private void DisableTargetObject()
    {
        if (_objectToDisable != null)
        {
            _objectToDisable.SetActive(false);
        }
    }

    private void DisableWeatherSystems()
    {
        if (_waveManager != null)
        {
            _waveManager.SetActive(false);
        }

        if (_rainManager != null)
        {
            _rainManager.SetActive(false);
        }
    }

    public static void ResetEyes()
    {
        _activatedEyesCount = 0;
    }
}