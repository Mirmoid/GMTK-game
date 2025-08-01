using UnityEngine;

public interface IMovementController
{
    Vector3 Velocity { get; }
    bool IsGrounded { get; }
    void Move(Vector3 direction, float speedModifier);
    void Jump(float power);
    void Slide(Vector3 direction, float speed);
}