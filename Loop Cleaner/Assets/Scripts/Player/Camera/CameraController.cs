using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    [Header("Camera Settings")]
    public Transform playerCameraTransform; // Основная камера игрока
    public Transform dialogueCameraTransform; // Камера для диалогов
    public float transitionSpeed = 2f;

    private Transform _currentTarget;
    private bool _isDialogueCameraActive;

    private void Awake()
    {
        Instance = this;
    }

    public void SwitchToDialogueCamera(Transform target)
    {
        _currentTarget = target;
        _isDialogueCameraActive = true;
        playerCameraTransform.gameObject.SetActive(false);
        dialogueCameraTransform.gameObject.SetActive(true);
    }

    public void SwitchToPlayerCamera()
    {
        _isDialogueCameraActive = false;
        dialogueCameraTransform.gameObject.SetActive(false);
        playerCameraTransform.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (_isDialogueCameraActive && _currentTarget != null)
        {
            dialogueCameraTransform.position = Vector3.Lerp(
                dialogueCameraTransform.position,
                _currentTarget.position,
                Time.deltaTime * transitionSpeed
            );

            dialogueCameraTransform.rotation = Quaternion.Lerp(
                dialogueCameraTransform.rotation,
                _currentTarget.rotation,
                Time.deltaTime * transitionSpeed
            );
        }
    }
}