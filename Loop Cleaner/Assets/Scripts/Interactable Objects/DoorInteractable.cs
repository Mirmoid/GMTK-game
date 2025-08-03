using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class DoorInteractable : InteractableBase
{
    [Header("Door Settings")]
    [SerializeField] private string _openAnimationName = "Door Openning";
    [SerializeField] private AudioClip _openSound;

    [Header("Activation Settings")]
    [SerializeField] private GameObject _waveManager;
    [SerializeField] private GameObject _rainManager;
    [SerializeField] private AudioSource _mainOST;
    [SerializeField] private bool _activateOnOpen = true;

    private Animator _animator;
    private AudioSource _audioSource;
    private bool _hasBeenOpened = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;

        // Деактивируем объекты при старте, если нужно
        if (_waveManager != null && _activateOnOpen)
            _waveManager.SetActive(false);

        if (_rainManager != null && _activateOnOpen)
            _rainManager.SetActive(false);

        if (_mainOST != null && _activateOnOpen)
            _mainOST.Stop();
    }

    public override void Interact()
    {
        if (!_hasBeenOpened)
        {
            OpenDoor();
            ActivateSystems();
            _hasBeenOpened = true;
        }
    }

    private void OpenDoor()
    {
        _animator.Play(_openAnimationName);

        if (_openSound != null)
        {
            _audioSource.PlayOneShot(_openSound);
        }
    }

    private void ActivateSystems()
    {
        // Активация WaveManager
        if (_waveManager != null)
        {
            _waveManager.SetActive(_activateOnOpen);
        }

        // Активация RainManager
        if (_rainManager != null)
        {
            _rainManager.SetActive(_activateOnOpen);
        }

        // Включение основной музыки
        if (_mainOST != null)
        {
            if (_activateOnOpen)
                _mainOST.Play();
            else
                _mainOST.Stop();
        }
    }
}