using UnityEngine;

public interface ICameraController
{
    Transform CameraTransform { get; }
    void RotateCamera(Vector2 input);
    float MouseSensitivity { get; set; }
}