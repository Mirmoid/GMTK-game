using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class CapsuleInteractable : InteractableBase
{
    [Header("Capsule Settings")]
    [SerializeField] private string _openAnimationName = "Capsule Openning";
    [SerializeField] private AudioClip _openSound;

    [Header("Panel Reference")]
    [SerializeField] private GameObject _slidingPanel; // Ссылка на раздвижную панель

    private Animator _animator;
    private AudioSource _audioSource;
    private bool _isOpened = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
    }

    public override void Interact()
    {
        if (!_isOpened)
        {
            OpenCapsule();
        }
    }

    private void OpenCapsule()
    {
        _isOpened = true;

        // Проигрываем анимацию
        if (!string.IsNullOrEmpty(_openAnimationName))
        {
            _animator.Play(_openAnimationName);
        }

        // Проигрываем звук
        if (_openSound != null)
        {
            _audioSource.PlayOneShot(_openSound);
        }
    }

    // Метод для анимационного события (если нужно)
    public void OnCapsuleOpened()
    {
        // Вызывается в конце анимации (нужно настроить в Animation Event)
        Debug.Log("Capsule fully opened");
    }
}