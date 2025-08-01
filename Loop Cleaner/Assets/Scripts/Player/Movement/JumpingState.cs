using UnityEngine;

public class JumpingState : BaseMovementState
{
    private const float JumpPower = 150f;
    private const float AirControl = 0.5f;

    public JumpingState(PlayerMovementController controller, IStaminaSystem staminaSystem)
        : base(controller, staminaSystem) { }

    public override void Enter()
    {
        if (!StaminaSystem.TryUseStamina(MovementConstants.StaminaCostJump) || !Controller.IsGrounded)
        {
            Controller.ChangeState<WalkingState>();
            return;
        }

        Controller.Jump(JumpPower);
    }

    public override void Update()
    {
        var input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        Vector3 moveDirection = Vector3.zero;
        if (CameraController != null)
        {
            moveDirection = CameraController.CameraTransform.TransformDirection(input);
            moveDirection.y = 0;
            moveDirection.Normalize();
        }

        Controller.Move(moveDirection, AirControl);

        if (Controller.IsGrounded)
        {
            Controller.ChangeState<WalkingState>();
        }
    }
}