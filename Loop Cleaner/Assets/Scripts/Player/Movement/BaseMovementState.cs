using UnityEngine;

public abstract class BaseMovementState : IMovementState
{
    protected readonly PlayerMovementController Controller;
    protected readonly IStaminaSystem StaminaSystem;
    protected Transform PlayerTransform => Controller.transform;
    protected ICameraController CameraController { get; }

    protected KeyCode JumpKey => KeyCode.Space;
    protected KeyCode DashKey => KeyCode.LeftShift;
    protected KeyCode SlideKey => KeyCode.LeftControl;

    protected BaseMovementState(PlayerMovementController controller, IStaminaSystem staminaSystem)
    {
        Controller = controller;
        StaminaSystem = staminaSystem;
        CameraController = controller.GetComponent<ICameraController>();
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }

    public virtual bool CanTransitionTo(IMovementState nextState) => true;
}