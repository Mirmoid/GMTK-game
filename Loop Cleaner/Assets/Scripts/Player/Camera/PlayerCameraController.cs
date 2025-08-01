using UnityEngine;

public class PlayerCameraController : MonoBehaviour, ICameraController
{
    [Header("Settings")]
    [SerializeField] private float _mouseSensitivity = 2f;
    [SerializeField] private float _minVerticalAngle = -80f;
    [SerializeField] private float _maxVerticalAngle = 80f;

    [Header("References")]
    [SerializeField] private Transform _cameraTransform;

    private float _verticalRotation;

    public Transform CameraTransform => _cameraTransform;
    public float MouseSensitivity
    {
        get => _mouseSensitivity;
        set => _mouseSensitivity = Mathf.Max(0.1f, value);
    }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void RotateCamera(Vector2 input)
    {
        transform.Rotate(Vector3.up * input.x * _mouseSensitivity);

        _verticalRotation -= input.y * _mouseSensitivity;
        _verticalRotation = Mathf.Clamp(_verticalRotation, _minVerticalAngle, _maxVerticalAngle);

        _cameraTransform.localEulerAngles = new Vector3(_verticalRotation, 0, 0);
    }
}