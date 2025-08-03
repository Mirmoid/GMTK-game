using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class AltarInteractable : InteractableBase
{
    [Header("Altar Settings")]
    [SerializeField] private AudioClip _interactionSound;
    [SerializeField] private int _mainMenuSceneIndex = 0;
    [SerializeField] private float _loadDelay = 1.5f;

    private AudioSource _audioSource;
    private bool _isActivated = false;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
    }

    public override void Interact()
    {
        if (!_isActivated)
        {
            ActivateAltar();
        }
    }

    private void ActivateAltar()
    {
        _isActivated = true;

        // Проигрываем звук
        if (_interactionSound != null)
        {
            _audioSource.PlayOneShot(_interactionSound);
        }

        // Запускаем переход с задержкой
        Invoke(nameof(LoadMainMenu), _loadDelay);
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene(_mainMenuSceneIndex);
    }

    // Визуальная обратная связь (опционально)
    private void Update()
    {
        if (_isActivated)
        {
            // Можно добавить плавное свечение или другие эффекты
            float emissionValue = Mathf.PingPong(Time.time, 1f);
            GetComponent<Renderer>().material.SetColor("_EmissionColor",
                Color.white * emissionValue);
        }
    }
}