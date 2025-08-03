using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private float _checkRate = 0.1f;

    private PlayerInput _input;
    private Camera _playerCamera;
    private float _nextCheckTime;
    private IInteractable _currentInteractable;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _playerCamera = Camera.main;
    }

    private void Update()
    {
        if (Time.time >= _nextCheckTime)
        {
            CheckForInteractable();
            _nextCheckTime = Time.time + _checkRate;
        }

        HandleInteractionInput();
    }

    private void CheckForInteractable()
    {
        Ray ray = new Ray(_playerCamera.transform.position, _playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null && Vector3.Distance(transform.position, hit.point) <= interactable.InteractionDistance)
            {
                _currentInteractable = interactable;
                return;
            }
        }

        _currentInteractable = null;
    }

    private void HandleInteractionInput()
    {
        if (_currentInteractable != null && _input.InteractPressed)
        {
            _currentInteractable.Interact();
        }
    }
}